#tool "nuget:?package=xunit.runner.console&version=2.3.1"
///////////////////////////////////////////////////////////////////////////////
// ARGUMENTS
///////////////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

var branch = Argument("branch", EnvironmentVariable("APPVEYOR_REPO_BRANCH"));
var isRelease = EnvironmentVariable("APPVEYOR_REPO_TAG") == "true";

var projectName="Scorpio";
var solution=$"./{projectName}.sln";
var nupkgPath = "./artifacts/";
var nupkgRegex = $"{nupkgPath}**/*.nupkg";
var nugetApiKey = "33b30e22-01aa-4b75-80e9-3e73cfa4c1b8";

var nugetQueryUrl="https://www.myget.org/F/scorpio/api/v3/index.json";
var nugetPushUrl = "https://www.myget.org/F/scorpio/api/v2/package";

var NUGET_PUSH_SETTINGS = new DotNetCoreNuGetPushSettings
                         {
						   Source = nugetPushUrl,
						   ApiKey = nugetApiKey
                         };
var NUGET_PACK_SETTINGS = new DotNetCorePackSettings
						{
						   Configuration = "Release",
						   OutputDirectory = nupkgPath
						};
///////////////////////////////////////////////////////////////////////////////
// TASKS
///////////////////////////////////////////////////////////////////////////////
Task("Clean")
	.Does(() =>
{
	Information("Begin clean solution");
	CleanDirectories("./src/**/bin");
	CleanDirectories("./src/**/obj");
	CleanDirectories("./src/**/build");
	CleanDirectories("./test/**/bin");
	CleanDirectories("./test/**/obj");
	CleanDirectories("./artifacts");
});
Task("Build").IsDependentOn("Clean")
	.Does(() =>
{
	var setting=new DotNetCoreBuildSettings{Configuration=configuration,MSBuildSettings=new DotNetCoreMSBuildSettings().WithProperty("SourceLinkCreate","true")};
	
	DotNetCoreBuild( solution,setting);
	
});
Task("Test")
	.IsDependentOn("Build")
	.Does(() =>
{
		DotNetCoreTest(solution,new DotNetCoreTestSettings{Configuration=configuration});
});

Task("Package")
    .IsDependentOn("Test")
    .WithCriteria(() => branch == "master" && isRelease)
    .Does(()=>
    {
			DotNetCorePack(solution, NUGET_PACK_SETTINGS);
    });

	Task("Publish")
    .IsDependentOn("Package")
    .WithCriteria(() => branch == "master" && isRelease)
    .Does(()=>
    {
    	var packages=GetFiles(nupkgRegex);
		foreach (var item in packages)
		{
		DotNetCoreNuGetPush(item.FullPath, NUGET_PUSH_SETTINGS);
		}
    });

Task("Default").IsDependentOn("Build").IsDependentOn("Test").IsDependentOn("Package").IsDependentOn("Publish");

RunTarget(target);