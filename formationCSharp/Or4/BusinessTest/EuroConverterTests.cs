using Microsoft.VisualStudio.TestTools.UnitTesting;
using Or.Business;
using System.Globalization;

namespace Or4
{
    [TestClass]
    public class EuroConverterTests
    {
        [TestMethod]
        public void TestConvertDecimalValues()
        {
            var converter = new EuroConverter();
            Assert.IsNotNull(converter);
            decimal val = 3.4M;
            var converted = converter.Convert(val, typeof(decimal), null, new CultureInfo(""));
            string resultat = "3,40 €";
            Assert.AreEqual(resultat, (string)converted);
        }

        [TestMethod]
        public void TestConvertAlwaysUseFrenchCulture()
        {
            var converter = new EuroConverter();
            Assert.IsNotNull(converter);
            decimal val = 3.4M;
            var converted = converter.Convert(val, typeof(decimal), null, new CultureInfo("en-US"));
            string resultat = "3,40 €";
            Assert.AreEqual(resultat, (string)converted);
        }

        [TestMethod]
        /// <summary>
        /// testing that the convert method return the same object in case the object type was not decimal
        /// </summary>
        public void TestConvertFloatFail()
        {
            var converter = new EuroConverter();
            Assert.IsNotNull(converter);
            float val = 3.4f;
            var converted = converter.Convert(val, typeof(decimal), null, new CultureInfo("en-US"));
            Assert.AreEqual(converted, val);
        }
    }
}
