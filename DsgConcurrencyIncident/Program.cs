using System;
using System.Collections.Generic;
using System.Linq;

namespace DsgConcurrencyIncidentFinder
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hallow and welcome to this awesome Concurrency Incident Finder" +
                "Please input a list of incidents and thier start and end dates. Use the following format:" +
                "{insidantName} {startDate} {endDate} set the date as mm/dd/yyy to complete the intesr pass 'D'");

            string insicant = "";

            var startDate = new DateTime(2022, 2, 10);
            var endDate = new DateTime(2022, 2, 14);

            var incidents = new List<IncidentDetails>();

            while (true)
            {
                insicant = Console.ReadLine();
                if (insicant == "D" || insicant == "d")
                {
                    break;
                }
                var arr = insicant.Split(' ');
                if (arr.Length == 3 && DateTime.TryParse(arr[1], out var startDateIn) && DateTime.TryParse(arr[2], out var endDateIn))
                {
                    incidents.Add(new IncidentDetails
                    {
                        Description = arr[0],
                        StartDate = startDateIn,
                        EndtDate = endDateIn
                    });
                    continue;
                }
                Console.WriteLine("Invalid format. Please use the following format:" +
                "{insidantName} {startDate} {endDate} set the date as mm/dd/yyy to process the data pass 'D'");
            }

            Console.WriteLine("Please insert the time pirios in the following format {startDate} {endDate} the date as dd/mm/yyy to process the data pass 'P'");

            var date = Console.ReadLine();

            var arrDate = date.Split(' ');
            if (arrDate.Length == 2 && DateTime.TryParse(arrDate[0], out var startDatePireod) && DateTime.TryParse(arrDate[0], out var endDatePireod))
            {
                startDate = startDatePireod;
                endDate = endDatePireod;
            }
            else
            {
                Console.WriteLine("Invalid format. Please use the following format:" +
                "{startDate} {endDate}");
            }

            var incidentsInRange = incidents.Where(
                x => x.StartDate >= startDate
                || x.EndtDate <= startDate).ToList();

            GetLongerInsidents(endDate, incidents, incidentsInRange);

            var dayDict = new Dictionary<DateTime, int>(); // to count mun of insidents per dat

            // take date range from existing insidets in that range
            DateTime minDay = incidentsInRange.Min(x => x.StartDate).Date, maxDay = incidentsInRange.Max(x => x.EndtDate).Date;
            DateTime currDate = minDay;


            (DateTime dateOfMaxCon, int maxCon) maxCuncurent;
            maxCuncurent.dateOfMaxCon = minDay;
            maxCuncurent.maxCon = 0;

            while (currDate <= maxDay.Date) // create the dictionary of days for requested period
            {
                var concurrents = incidentsInRange.Count(x => minDay >= x.StartDate && minDay <= x.StartDate);
                dayDict.Add(currDate, concurrents);

                if (concurrents > maxCuncurent.maxCon) // fined max cuncorency
                {
                    maxCuncurent.dateOfMaxCon = currDate;
                    maxCuncurent.maxCon = concurrents;
                }

                currDate = currDate.AddDays(1);
            }

            var action = "";
            switch (action)
            {
                case Actions.GetMaxConcurrency:
                    var maxCon= GetMaxConcurrency(dayDict);

            }

            var avgCon = GetAvgConcurrency(dayDict);
            
            Console.WriteLine($"The max cuncirent events in the provided time is {maxCuncurent.maxCon}");
            Console.ReadLine();
        }

        private static double GetAvgConcurrency(Dictionary<DateTime, int> dayDict)
        {
            return dayDict.Average(x => x.Value); 
        }

        private static int GetMaxConcurrency(Dictionary<DateTime, int> dayDict)
        {
            return dayDict.Max(x => x.Value);
        }

        private static void GetLongerInsidents(DateTime endDate, List<IncidentDetails> incidents, List<IncidentDetails> incidentsInRange)
        {
            var incidentsLongerThenRange = incidents.Where(x => x.EndtDate > endDate)
                            .Select(x => new IncidentDetails { Description = x.Description, StartDate = x.StartDate, EndtDate = endDate }).ToList();

            if (incidentsLongerThenRange.Any())
            {
                foreach (var item in incidentsLongerThenRange)
                {
                    incidentsInRange.First(x => x.Description == item.Description).EndtDate = item.EndtDate;
                }
            }
        }
    }
}

namespace DsgConcurrencyIncident
{
    enum Actions
    {
    }
}