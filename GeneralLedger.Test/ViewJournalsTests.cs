using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace GeneralLedger.Test
{
    public class ViewJournalsTests : IJournalReader
    {
        private Journal[] _journals;
        private ViewJournals _viewJournals;

        public List<Journal> All()
        {
            return _journals.ToList();
        }

        private static void ExpectNoJournals(ViewJournalsResponse response)
        {
            response.Journals.Should().BeEmpty();
        }

        private static void ExpectOneJournal(ViewJournalsResponse response)
        {
            response.Journals.Should().HaveCount(1);
        }

        private static void ExpectPresentableJournalToHave(string expectedDescription, string expectedDateTime,
            ViewJournalsResponse.PresentableJournal firstPresentableJournal)
        {
            firstPresentableJournal.PostingDateTime.Should().Be(DateTime.Parse(expectedDateTime));
            firstPresentableJournal.Description.Should().Be(expectedDescription);
        }

        private ViewJournalsResponse Execute()
        {
            return _viewJournals.Execute();
        }

        [SetUp]
        public void SetUp()
        {
            _journals = new Journal[] { };
            _viewJournals = new ViewJournals(this);
        }

        [Test]
        public void CanViewEmptyGeneralLedger()
        {
            ExpectNoJournals(Execute());
        }

        [Test]
        [TestCase(
            "Appreciation of bricks and mortar", "2008/01/01 9:00",
            "Appreciation of bricks and mortar", "2008/01/01 9:00"
        )]
        [TestCase(
            "Correcting bookkeeping error", "2011/01/01 17:00",
            "Correcting bookkeeping error", "2011/01/01 17:00"
        )]
        public void CanViewASingleJournal(
            string description,
            string dateTime,
            string expectedDescription,
            string expectedDateTime
        )
        {
            _journals = new[]
            {
                new Journal
                {
                    Description = description,
                    PostingDate = DateTime.Parse(dateTime)
                }
            };

            var viewJournalsResponse = Execute();

            var firstPresentableJournal = viewJournalsResponse.Journals.First();
            
            ExpectPresentableJournalToHave(expectedDescription, expectedDateTime, firstPresentableJournal);
            ExpectOneJournal(viewJournalsResponse);
        }
    }
}