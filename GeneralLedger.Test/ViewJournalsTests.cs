using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using FluentAssertions.Collections;
using NUnit.Framework;

namespace GeneralLedger.Test
{
    public class ViewJournalsTests : IJournalReader
    {
        private Object[] _journals;
        private ViewJournals _viewJournals;

        public List<object> All()
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

        private ViewJournalsResponse Execute()
        {
            return _viewJournals.Execute();
        }

        [SetUp]
        public void SetUp()
        {
            _journals = new Object[] { };
            _viewJournals = new ViewJournals(this);
        }

        [Test]
        public void CanViewEmptyGeneralLedger()
        {
            ExpectNoJournals(Execute());
        }

        [Test]
        public void CanViewASingleJournal()
        {
            _journals = new [] { new Journal
            {
                Description = "Appreciation of bricks and mortar",
                PostingDate = DateTime.Parse("2008/01/01 9:00")
            } };

            var viewJournalsResponse = Execute();
            viewJournalsResponse.Journals.First().Date.Should().Be(DateTime.Parse("2008/01/01 9:00"));
            viewJournalsResponse.Journals.First().Description.Should().Be("Appreciation of bricks and mortar");
            ExpectOneJournal(viewJournalsResponse);
        }
    }
}