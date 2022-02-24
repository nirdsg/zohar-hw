using Microsoft.VisualStudio.TestTools.UnitTesting;
using InsidentAnalyser;
using System.Collections.Generic;
using System;

namespace InsidentAnalyser.Test
{
    [TestClass]
    public class InsidentConcurrencyAnalyserTest
    {
        static IncidentConcurrencyAnalyser IncidentConcurrencyAnalyser;

        [ClassInitialize]
        public static void ClassInitialize(TestContext tc)
        {
            IncidentConcurrencyAnalyser = new IncidentConcurrencyAnalyser(
               new List<IncidentDetails> {
               new IncidentDetails{ Description = "ins1", StartDate = new DateTime(2021, 2, 15), EndtDate = new DateTime(2021, 2, 20) },
               new IncidentDetails{ Description = "ins2", StartDate = new DateTime(2021, 1, 20), EndtDate = new DateTime(2021, 3, 20) },
               new IncidentDetails{ Description = "ins3", StartDate = new DateTime(2021, 1, 20), EndtDate = new DateTime(2021, 2, 15) },
               new IncidentDetails{ Description = "ins4", StartDate = new DateTime(2022, 1, 15), EndtDate = new DateTime(2022, 1, 20) }
               });
        }

        [TestMethod]
        public void SingleIncident()
        {
            var startDate = new DateTime(2022, 1, 14);
            var endDate = new DateTime(2022, 1, 21);

            Assert.AreEqual(1, IncidentConcurrencyAnalyser.GetMaxConcurrancyForTime(startDate, endDate));
        }

        [TestMethod]
        public void MultipleIncident()
        {
            var startDate = new DateTime(2021, 1, 20);
            var endDate = new DateTime(2021, 1, 25);

            Assert.AreEqual(2, IncidentConcurrencyAnalyser.GetMaxConcurrancyForTime(startDate, endDate));
        }

        [TestMethod]
        public void ExactRagneOfIncident()
        {
            var startDate = new DateTime(2022, 1, 15);
            var endDate = new DateTime(2022, 1, 20);

            Assert.AreEqual(1, IncidentConcurrencyAnalyser.GetMaxConcurrancyForTime(startDate, endDate));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InvalidRequest()
        {
            // startDate > endDate
            var startDate = new DateTime(2022, 1, 20);
            var endDate = new DateTime(2021, 1, 15);

            Assert.AreEqual(1, IncidentConcurrencyAnalyser.GetMaxConcurrancyForTime(startDate, endDate));
        }

        [TestMethod]
        public void DuplicateIncidentName()
        {
            var startDate = new DateTime(2021, 1, 20);
            var endDate = new DateTime(2021, 1, 25);

            var incidentConcurrencyAnalyser = new IncidentConcurrencyAnalyser(
               new List<IncidentDetails> {
               new IncidentDetails{ Description = "ins2", StartDate = new DateTime(2021, 1, 20), EndtDate = new DateTime(2021, 3, 20) },
               new IncidentDetails{ Description = "ins2", StartDate = new DateTime(2021, 1, 20), EndtDate = new DateTime(2021, 2, 15) },
               });

            Assert.AreEqual(2, incidentConcurrencyAnalyser.GetMaxConcurrancyForTime(startDate, endDate));
        }
    }
}
