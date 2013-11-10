using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NRWeb.Commons;
using NRWeb.Model;
using NRWeb.Services.Helpers;

namespace NRWeb.Services
{
    public class DataSetter:IDataSetter
    {

        private readonly IUserService _userService;

        public DataSetter(IUserService userService)
        {
            _userService = userService;
        }

        public ICollection<RaceTime> GetRaceTimesFromExcelFile(Stream excelStream)
        {
            var excelReader = new ExcelReader();
            var results = excelReader.LoadFromExcel(excelStream);

            var enumerable = results as IList<Dictionary<string, object>> ?? results.ToList();
            
            var raceTimes = GetInternalRaceTimes(enumerable);

            var modelRaceTimes = (from racetime in raceTimes let username = racetime.Name let user = _userService.GetUserByName(username) where user != null select new RaceTime {Time = racetime.Time, User = user}).ToList();
            return modelRaceTimes;
        }

        public ICollection<User> GetUserdataFromGoogleDoc(string googleDoc)
        {
            return null;
        }

        private static IEnumerable<InternalRaceTime> GetInternalRaceTimes(IEnumerable<Dictionary<string, object>> results)
        {
            return (from result in results
                let hours = Convert.ToInt32(result["timer"])
                let minutes = Convert.ToInt32(result["minutter"])
                let seconds = Convert.ToInt32(result["sekunder"])
                select new InternalRaceTime
                {
                    Name = Convert.ToString(result["navn"]), Time = new TimeSpan(hours, minutes, seconds)
                }).ToList();
        }
    }
}