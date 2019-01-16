using System;
using FluentAssertions;
using NUnit.Framework;

namespace GeneralLedger.Test
{
    public class PostJournalTests : IJournalWriter
    {
        private Journal _savedJournal;

        public void Save(Journal journal)
        {
            _savedJournal = journal;
        }

        [Test]
        [TestCase(
            "2019/06/02 10:23", "Depreciation of heavy machinery",
            "2019/06/02 10:23", "Depreciation of heavy machinery"
        )]
        [TestCase(
            "2019/02/28 10:10", "Reorganising revenue",
            "2019/02/28 10:10", "Reorganising revenue"
        )]
        public void CanPostJournal(
            string date, string description,
            string expectedDate, string expectedDescription
        )
        {
            var postJournal = new PostJournal(this);
            postJournal.Execute(new PostJournalRequest
            {
                Date = DateTime.Parse(date),
                Description = description
            });

            _savedJournal.PostingDate.Should().Be(DateTime.Parse(expectedDate));
            _savedJournal.Description.Should().Be(expectedDescription);
        }
    }
}