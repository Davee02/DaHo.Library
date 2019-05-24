using System;
using System.Security;
using NUnit.Framework;

namespace DaHo.Library.Utilities.Test
{
    [TestFixture]
    class StringUtilities_Tests
    {
        [Test]
        [TestCase("hallo", "ll")]
        [TestCase("HALLO", "ll")]
        [TestCase("hallo", "LL")]
        [TestCase("HaLlO", "hAl")]
        public void ContainsIgnoreCase_When_Contains_Return_True(string source, string value)
        {
            Assert.IsTrue(StringUtilities.Contains(source, value, StringComparison.OrdinalIgnoreCase));
        }

        [Test]
        [TestCase("hallo", "halloli")]
        [TestCase("HALLO", "lloo")]
        [TestCase("hallo", "ihall")]
        [TestCase("HaLlO", "Halo")]
        public void ContainsIgnoreCase_When_Not_Contains_Return_False(string source, string value)
        {
            Assert.IsFalse(StringUtilities.Contains(source, value, StringComparison.OrdinalIgnoreCase));
        }

        [Test]
        public void ContainsIgnoreCase_When_Value_Is_Null_Return_False()
        {
            string source = "Hallo";
            string value = null;

            Assert.IsFalse(StringUtilities.Contains(source, value, StringComparison.OrdinalIgnoreCase));
        }

        [Test]
        public void ContainsIgnoreCase_When_Value_Is_Empty_Return_False()
        {
            string source = "Hallo";
            string value = null;

            Assert.IsFalse(StringUtilities.Contains(source, value, StringComparison.OrdinalIgnoreCase));
        }

        [Test]
        [TestCase("This is a test string")]
        [TestCase("öä$ü¨+*ç%&/()=°)132*/->EFDQfw")]
        public void ConvertToString_Returns_Valid_String(string stringValue)
        {
            var secureString = new SecureString();
            foreach (var c in stringValue)
            {
                secureString.AppendChar(c);
            }

            Assert.AreEqual(stringValue, secureString.ConvertToString());
        }
    }
}
