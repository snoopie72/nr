using System.Collections;
using System.Collections.Generic;
using System.IO;
using NRWeb.Model;

namespace NRWeb.Services
{
   public interface IDataSetter
   {
       ICollection<RaceTime> GetRaceTimesFromExcelFile(Stream excelStream);
       ICollection<User> GetUserdataFromGoogleDoc(string googleDoc);
   }
}
