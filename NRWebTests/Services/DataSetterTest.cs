using System.Reflection;
using NRWeb.Services;
using NUnit.Framework;

namespace NRWebTests.Services
{
    [TestFixture]
    public class DataSetterTest
    {
        private DataSetter _dataSetter;
        [SetUp]
        public void Init()
        {
            _dataSetter = new DataSetter(null);
            
        }
        [Test]
        public void TestReadExcel()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var excelStream = assembly.GetManifestResourceStream("NRWebTests.Resources.Krokenmila_14052012.xlsx");

            _dataSetter.GetRaceTimesFromExcelFile(excelStream);
            if (excelStream != null) excelStream.Close();
        }
    }
}