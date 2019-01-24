using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FluentAssertions;
using GeneralLedger.Boundary;
using GeneralLedger.Domain;
using GeneralLedger.UseCase;
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
        private ViewJournal _viewJournal;

        private PostJournalResponse GivenOneJournal(string dateTime, string description)
        {
            return _postJournal.Execute(new PostJournalRequest
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

        [Test]
        public void CanCreateABalancingJournal()
        {
            var response = GivenOneJournal(
                "2019/01/01 17:00",
                "Correction",
                new[]
                {
                    new PostJournalRequest.Entry
                    {
                        Type = DEBIT,
                        Amount = 10.00
                    },
                    new PostJournalRequest.Entry
                    {
                        Type = CREDIT,
                        Amount = 10.00
                    }
                }
            );

            var journalId = response.Id;

            WhenViewingAJournal(journalId);

            ThenTheJournalShouldContainACreditOf(new decimal(10.00));
            AndShouldContainADebitOf(new decimal(10.00));
        }

        private void AndShouldContainADebitOf(decimal expectedAmount)
        {
            
        }

        private void ThenTheJournalShouldContainACreditOf(decimal expectedAmount)
        {
            
        }

        private void WhenViewingAJournal(string journalId)
        {
            _viewJournalsResponse = _viewJournal.Execute(new ViewJournalRequest
            {
                Id = journalId
            });

        }
    }
}