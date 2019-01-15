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
            _journals = new [] { new Object() };

            ExpectOneJournal(Execute());
        }
    }
}