using System;
using Core.Helpers;
using Xunit;

namespace Tests
{
    public class UnitTest1
    {
        [Fact]
        public void StatndartCodeParserShouldReturnTrueForCorrectString()
        {
            Assert.Equal(true, CodeParser.TryParseStandartCode("FIA standard 8858-2010"));
        }
    }
}