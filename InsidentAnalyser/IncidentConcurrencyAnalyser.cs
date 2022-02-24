using System;
using System.Collections.Generic;
using System.Linq;

namespace InsidentAnalyser
{
    public class IncidentConcurrencyAnalyser : IncidentAnalyserBase
    {
        public IncidentConcurrencyAnalyser(List<IncidentDetails> incidents) 
            : base(incidents)
        {

        }

        public int GetMaxConcurrancyForTime(DateTime startDate, DateTime endDate)
        {
            ValidateRequest(startDate, endDate);
            var maxConncurrencies = ActOnDailySplit(Incidents, startDate, endDate);

            return maxConncurrencies;
        }
        private int ActOnDailySplit(List<IncidentDetails> incidentsInRange, DateTime minDay, DateTime maxDay)
        {
            var dayDict = new Dictionary<DateTime, int>();
            var currDate = minDay;
            int valurToCompare = 0;
            while (currDate <= maxDay.Date) // create the dictionary of days for requested period
            {
                var concurrents = incidentsInRange.Count(x => currDate >= x.StartDate && currDate <= x.EndtDate);
                dayDict.Add(currDate, concurrents);

                if (concurrents > valurToCompare)
                {
                    valurToCompare = concurrents;
                }

                currDate = currDate.AddDays(1);
            }
            return valurToCompare;
        }
       
    }
}
