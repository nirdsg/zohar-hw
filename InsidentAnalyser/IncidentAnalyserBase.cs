using System;
using System.Collections.Generic;
using System.Linq;

namespace InsidentAnalyser
{
    public class IncidentAnalyserBase
    {
        public List<IncidentDetails> Incidents { get; set; }
        public IncidentAnalyserBase(List<IncidentDetails> incidents)
        {
            Incidents = incidents;
        }
        internal void ValidateRequest(DateTime startDate, DateTime endDate)
        {
            if (startDate > endDate)
                throw new ArgumentException("StartDate must be befor end date");
        }
    }
}