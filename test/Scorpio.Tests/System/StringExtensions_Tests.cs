using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Shouldly;
namespace System
{
    public class StringExtensions_Tests
    {
        [Fact]
        public void EnsureEndsWith()
        {
            "hello word".EnsureEndsWith('d').ShouldBe("hello word");
            "hello word".EnsureEndsWith('!').ShouldBe("hello word!");
        }
        [Fact]
        public void EnsureStartsWith()
        {
            "hello word".EnsureStartsWith('h').ShouldBe("hello word");
            "hello word".EnsureStartsWith('i').ShouldBe("ihello word");
        }
        [Fact]
        public void IsNullOrEmpty()
        {
            string nstr = null;
            nstr.IsNullOrEmpty().ShouldBeTrue();
            "".IsNullOrEmpty().ShouldBeTrue();
            string.Empty.IsNullOrEmpty().ShouldBeTrue();
            " ".IsNullOrEmpty().ShouldBeFalse();
            "hello word".IsNullOrEmpty().ShouldBeFalse();
            "\t".IsNullOrEmpty().ShouldBeFalse();
        }
        [Fact]
        public void IsNullOrWhiteSpace()
        {
            string nstr = null;
            nstr.IsNullOrWhiteSpace().ShouldBeTrue();
            "".IsNullOrWhiteSpace().ShouldBeTrue();
            string.Empty.IsNullOrWhiteSpace().ShouldBeTrue();
            " ".IsNullOrWhiteSpace().ShouldBeTrue();
            "hello word".IsNullOrWhiteSpace().ShouldBeFalse();
            "\t".IsNullOrWhiteSpace().ShouldBeTrue();
            "\n".IsNullOrWhiteSpace().ShouldBeTrue();
            "\r".IsNullOrWhiteSpace().ShouldBeTrue();
        }

        [Fact]
        public void Left()
        {
            var str = "Scorpio project is great.";
            str.Left(7).ShouldBe("Scorpio");
            str.Left(8).ShouldBe("Scorpio ");
            ((Action)(() => str.Left(0))).ShouldThrow<ArgumentException>();
            ((Action)(() => str.Left(40))).ShouldThrow<ArgumentException>();
        }
        [Fact]
        public void Right()
        {
            var str = "Scorpio project is great.";
            str.Right(7).ShouldBe(" great.");
            str.Right(8).ShouldBe("s great.");
            ((Action)(() => str.Right(0))).ShouldThrow<ArgumentException>();
            ((Action)(() => str.Right(40))).ShouldThrow<ArgumentException>();
        }

        [Fact]
        public void NormalizeLineEndings()
        {
            "\r".NormalizeLineEndings().ShouldBe(Environment.NewLine);
            "\r\n".NormalizeLineEndings().ShouldBe(Environment.NewLine);
            "\n\r".NormalizeLineEndings().ShouldBe(Environment.NewLine);
            "\n".NormalizeLineEndings().ShouldBe(Environment.NewLine);
            "Scorpio project is great.\r\n".NormalizeLineEndings().ShouldBe($"Scorpio project is great.{Environment.NewLine}");
            "Scorpio project is great.\n".NormalizeLineEndings().ShouldBe($"Scorpio project is great.{Environment.NewLine}");
            "Scorpio project is great.\r".NormalizeLineEndings().ShouldBe($"Scorpio project is great.{Environment.NewLine}");
            "Scorpio project is great.\n\r".NormalizeLineEndings().ShouldBe($"Scorpio project is great.{Environment.NewLine}");
            "Scorpio project is\r\n great.\r\n".NormalizeLineEndings().ShouldBe($"Scorpio project is{Environment.NewLine} great.{Environment.NewLine}");
            "Scorpio project is\n\r great.\n".NormalizeLineEndings().ShouldBe($"Scorpio project is{Environment.NewLine} great.{Environment.NewLine}");
            "Scorpio project is\n great.\r".NormalizeLineEndings().ShouldBe($"Scorpio project is{Environment.NewLine} great.{Environment.NewLine}");
            "Scorpio project is\r great.\n\r".NormalizeLineEndings().ShouldBe($"Scorpio project is{Environment.NewLine} great.{Environment.NewLine}");
        }

        [Fact]
        public void NthIndexOf()
        {
            var str = "Scorpio project is great.";
            str.NthIndexOf('p', 1).ShouldBe(4);
            str.NthIndexOf('p', 2).ShouldBe(8);
            str.NthIndexOf('w', 2).ShouldBe(-1);
            str.NthIndexOf('P', 2).ShouldBe(-1);
            str.NthIndexOf('s', 2).ShouldBe(-1);
            str.NthIndexOf('s', 1).ShouldBe(17);
            str.NthIndexOf('s', 1, StringComparison.Ordinal).ShouldBe(17);
            str.NthIndexOf('s', 1, StringComparison.OrdinalIgnoreCase).ShouldBe(0);
            str.NthIndexOf('S', 1, StringComparison.Ordinal).ShouldBe(0);
            str.NthIndexOf('S', 1, StringComparison.OrdinalIgnoreCase).ShouldBe(0);
            str.NthIndexOf('w', 1, StringComparison.OrdinalIgnoreCase).ShouldBe(-1);
        }

        [Fact]
        public void RemovePostFix()
        {
            var str = "Scorpio project is great.";
            str.RemovePostFix("great.").ShouldBe("Scorpio project is ");
            str.RemovePostFix("is ", "great.").ShouldBe("Scorpio project is ");
            str.RemovePostFix("is", "great").ShouldBe("Scorpio project is great.");
            str.RemovePostFix("Great.").ShouldBe("Scorpio project is great.");
            str.RemovePostFix(StringComparison.Ordinal, "Great.").ShouldBe("Scorpio project is great.");
            str.RemovePostFix(StringComparison.OrdinalIgnoreCase, "Great.").ShouldBe("Scorpio project is ");
            string.Empty.RemovePostFix("is").ShouldBe(null);
            ((string)null).RemovePostFix("is").ShouldBe(null);
            str.RemovePostFix(StringComparison.Ordinal).ShouldBe("Scorpio project is great.");
            str.RemovePostFix().ShouldBe("Scorpio project is great.");

        }
        [Fact]
        public void RemovePreFix()
        {
            var str = "Scorpio project is great.";
            str.RemovePreFix("Scorpio").ShouldBe(" project is great.");
            str.RemovePreFix("is ", "Scorpio").ShouldBe(" project is great.");
            str.RemovePreFix("is", "scorpio").ShouldBe("Scorpio project is great.");
            str.RemovePreFix("Great.").ShouldBe("Scorpio project is great.");
            str.RemovePreFix(StringComparison.Ordinal, "scorpio").ShouldBe("Scorpio project is great.");
            str.RemovePreFix(StringComparison.OrdinalIgnoreCase, "scorpio").ShouldBe(" project is great.");
            string.Empty.RemovePreFix("is").ShouldBe(null);
            ((string)null).RemovePreFix("is").ShouldBe(null);
            str.RemovePreFix(StringComparison.Ordinal).ShouldBe("Scorpio project is great.");
            str.RemovePreFix().ShouldBe("Scorpio project is great.");
        }

        [Fact]
        public void Split()
        {
            var str = "Scorpio project is great.";
            StringExtensions.Split(str, " ").Length.ShouldBe(4);
            StringExtensions.Split(str, "S").Length.ShouldBe(2);
            StringExtensions.Split(str, "S")[0].IsNullOrEmpty().ShouldBeTrue();
            StringExtensions.Split(str, "S", StringSplitOptions.RemoveEmptyEntries).Length.ShouldBe(1);
        }
        [Fact]
        public void SplitToLines()
        {
            var str = "Scorpio\r\nproject\r\nis\r\ngreat.\r\n";
            str.SplitToLines().Length.ShouldBe(5);
            str.SplitToLines( StringSplitOptions.RemoveEmptyEntries).Length.ShouldBe(4);
        }

        [Theory]
        [InlineData("Scorpio", "scorpio")]
        [InlineData("scorpio", "scorpio")]
        [InlineData("1Scorpio", "1Scorpio")]
        [InlineData(" Scorpio", " Scorpio")]
        [InlineData("S", "s")]
        [InlineData("", "")]
        public void ToCamelCase(string value,string expected)
        {
            value.ToCamelCase().ShouldBe(expected);
        }

        [Theory]
        [InlineData("RemoveEmptyEntries", "Remove empty entries")]
        [InlineData("removeEmptyEntries", "remove empty entries")]
        [InlineData("Remove EmptyEntries", "Remove Empty entries")]
        [InlineData("Remove emptyEntries", "Remove empty entries")]
        [InlineData("ThisIsSampleSentence", "This is sample sentence")]
        [InlineData("  ", "  ")]
        public void ToSentenceCase(string value,string expected)
        {
            value.ToSentenceCase().ShouldBe(expected);
            value.ToSentenceCase(true).ShouldBe(expected);
        }

        [Fact]
        public void ToEnum()
        {
            "Monday".ToEnum<DayOfWeek>().ShouldBe(DayOfWeek.Monday);
            ((Action)(() => "monday".ToEnum<DayOfWeek>())).ShouldThrow<ArgumentException>();
            "monday".ToEnum<DayOfWeek>(true).ShouldBe(DayOfWeek.Monday);
            ((Action)(() => "monday".ToEnum<DayOfWeek>(false))).ShouldThrow<ArgumentException>();
        }

        [Fact]
        public void ToMd5()
        {
            "Scorpio".ToMd5().ShouldBe("7c70e2cb2b4a13c4590f6b15c30385fd");
        }

        [Theory]
        [InlineData("scorpio", "Scorpio")]
        [InlineData("Scorpio", "Scorpio")]
        [InlineData("1Scorpio", "1Scorpio")]
        [InlineData(" Scorpio", " Scorpio")]
        [InlineData("s", "S")]
        [InlineData("", "")]
        public void ToPascalCase(string value, string expected)
        {
            value.ToPascalCase().ShouldBe(expected);
        }
    }
}
