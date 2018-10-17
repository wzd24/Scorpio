namespace Scorpio.Modularity
{
    public interface IModuleManager
    {
        void InitializeModules(ApplicationInitializationContext applicationInitializationContext);
        void ShutdownModules(ApplicationShutdownContext applicationShutdownContext);
    }
}