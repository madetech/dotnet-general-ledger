using System.Collections.Generic;
using FluentAssertions;
using FluentAssertions.Collections;
using NUnit.Framework;

namespace GeneralLedger.AcceptanceTest
{
    public class JournalGateway : IJournalGateway
    {
        private readonly List<object> _journals = new List<object>();

        public List<object> All()
        {
            return _journals;
        }

        public void Save()
        {
            _journals.Add(new object());
        }
    }

    public class GeneralLedgerTests
    {
        private ViewJournals _viewJournals;
        private JournalGateway _journalGateway;
        private PostJournal _postJournal;
        private ViewJournalsResponse _viewJournalsResponse;

        private void GivenOneJournal()
        {
            _postJournal.Execute(new PostJournalRequest
            {
                Components = new[]
                {
                    new PostJournalRequest.Component()
                }
            });
        }

        private void WhenViewingJournals()
        {
            _viewJournalsResponse = _viewJournals.Execute();
        }

        private void ThenJournalsShouldBeEmpty()
        {
            _viewJournalsResponse.Journals.Should().BeEmpty();
        }

        private void ThenThereShouldBeOneJournal()
        {
            _viewJournalsResponse.Journals.Should().HaveCount(1);
        }

        [SetUp]
        public void SetUp()
        {
            _journalGateway = new JournalGateway();
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
            GivenOneJournal();
            
            WhenViewingJournals();

            ThenThereShouldBeOneJournal();
        }
    }
}