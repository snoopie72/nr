using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NRWeb.Commons;
using NUnit.Framework;

namespace NRWebTests.Common
{
    [TestFixture]
    public class ExcelResultsReaderTest
    {
        [Test]
        public void TestReadKrokenMilaResult()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var excelStream = assembly.GetManifestResourceStream("NRWebTests.Resources.Krokenmila_14052012.xlsx");
            var excelReader = new ExcelReader();
            var results = excelReader.LoadFromExcel(excelStream);
            if (excelStream != null) excelStream.Close();
            Assert.IsNotNull(results);
        }
    }
}
