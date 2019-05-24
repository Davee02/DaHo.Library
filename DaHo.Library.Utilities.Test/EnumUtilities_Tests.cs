using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace DaHo.Library.Utilities.Test
{
    [TestFixture]
    class EnumUtilities_Tests
    {
        [Test]
        public void GetAttributeFromEnum_Get_Correct_Attribute()
        {
            var testEnumValue1 = TestEnum.Value1;
            var testEnumValue2 = TestEnum.Value2;
            var testEnumValue3 = TestEnum.Value3;

            var value1Attribute = testEnumValue1.GetAttributeFromEnum<System.ComponentModel.DescriptionAttribute>();
            var value2Attribute = testEnumValue2.GetAttributeFromEnum<System.ComponentModel.AmbientValueAttribute>();
            var value3Attribute = testEnumValue3.GetAttributeFromEnum<System.ComponentModel.CategoryAttribute>();

            Assert.NotNull(value1Attribute);
            Assert.NotNull(value2Attribute);
            Assert.NotNull(value3Attribute);

            Assert.AreEqual("Value1Description", value1Attribute.Description);
            Assert.AreEqual("Value2AmbientValue", value2Attribute.Value);
            Assert.AreEqual("Value3Category", value3Attribute.Category);
        }

        [Test]
        public void GetAttributeFromEnum_Get_Incorrect_Attribute()
        {
            var testEnumValue1 = TestEnum.Value1;
            var testEnumValue2 = TestEnum.Value2;
            var testEnumValue3 = TestEnum.Value3;

            var value1Attribute = testEnumValue1.GetAttributeFromEnum<System.ComponentModel.AmbientValueAttribute>();
            var value2Attribute = testEnumValue2.GetAttributeFromEnum<System.ComponentModel.CategoryAttribute>();
            var value3Attribute = testEnumValue3.GetAttributeFromEnum<System.ComponentModel.AmbientValueAttribute>();

            Assert.Null(value1Attribute);
            Assert.Null(value2Attribute);
            Assert.Null(value3Attribute);
        }

        [Test]
        public void GetValueFromDescription_Get_Correct_Enum_Value()
        {
            var value1 = EnumUtilities.GetEnumValueFromDescription<TestEnum>("Value1Description");
            var value2 = EnumUtilities.GetEnumValueFromDescription<TestEnum>("Value2Description");
            var value3 = EnumUtilities.GetEnumValueFromDescription<TestEnum>("Value3Description");

            Assert.AreEqual(TestEnum.Value1, value1);
            Assert.AreEqual(TestEnum.Value2, value2);
            Assert.AreEqual(TestEnum.Value3, value3);
        }


        [Test]
        public void GetValueFromDescription_Returns_Default_When_Description_Not_Found()
        {
            var value = EnumUtilities.GetEnumValueFromDescription<TestEnum>("does not exists");
            var defaultValue = default(TestEnum);

            Assert.AreEqual(defaultValue, value);
        }

        private enum TestEnum
        {
            [System.ComponentModel.Description("Value1Description")]
            Value1 = 0,
            [System.ComponentModel.Description("Value2Description")]
            [System.ComponentModel.AmbientValue("Value2AmbientValue")]
            Value2 = 1,
            [System.ComponentModel.Description("Value3Description")]
            [System.ComponentModel.Category("Value3Category")]
            Value3 = 2
        }
    }
}
