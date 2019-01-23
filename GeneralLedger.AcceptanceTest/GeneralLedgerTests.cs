using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace GeneralLedger.AcceptanceTest
{
    public class InMemoryJournalGateway : IJournalGateway
    {
        private readonly List<Journal> _journals = new List<Journal>();
        public List<Journal> All() => _journals;
        public void Save(Journal journal) => _journals.Add(journal);
    }

    public class GeneralLedgerTests
    {
        private ViewJournals _viewJournals;
        private InMemoryJournalGateway _journalGateway;
        private PostJournal _postJournal;
        private ViewJournalsResponse _viewJournalsResponse;

        private void GivenOneJournal(string dateTime, string description)
        {
            _postJournal.Execute(new PostJournalRequest
            {
                PostingDateTime = DateTime.Parse(dateTime),
                Description = description,
                Entries = new PostJournalRequest.Entry[] { }
            });
        }

        private void WhenViewingJournals() => _viewJournalsResponse = _viewJournals.Execute();

        private void ThenJournalsShouldBeEmpty() => _viewJournalsResponse.Journals.Should().BeEmpty();

        private void ThenThereShouldBeOneJournal() => _viewJournalsResponse.Journals.Should().HaveCount(1);

        private void AndThereShouldBeAJournalMatching(
            int journalNumber,
            string expectedDateTime,
            string expectedDescription
        )
        {
            var firstPresentableJournal = _viewJournalsResponse.Journals[journalNumber - 1];
            firstPresentableJournal.PostingDateTime.Should().Be(DateTime.Parse(expectedDateTime));
            firstPresentableJournal.Description.Should().Be(expectedDescription);
        }

        [SetUp]
        public void SetUp()
        {
            _journalGateway = new InMemoryJournalGateway();
            _viewJournals = new ViewJournals(_journalGateway);
            _postJournal = new PostJournal(_journalGateway);
        }

        [Test]
        public void CanViewNoJournals()
        {
            WhenViewingJournals();

            ThenJournalsShouldBeEmpty();
        }

        [Test]
        public void CanViewASingleJournal()
        {
            GivenOneJournal(
                "2019/01/01 17:00",
                "Moving asset valuation."
            );

            WhenViewingJournals();

            ThenThereShouldBeOneJournal();
            AndThereShouldBeAJournalMatching(
                1,
                "2019/01/01 17:00",
                "Moving asset valuation."
            );
        }
    }
}