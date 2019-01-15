using System.Collections.Generic;
using FluentAssertions;
using FluentAssertions.Primitives;
using NUnit.Framework;

namespace GeneralLedger.Test
{
    public class PostJournalTests : IJournalWriter
    {
        private bool _called;

        public void Save()
        {
            _called = true;
        }

        private void ExpectSaveToHaveBeenCalled()
        {
            _called.Should().BeTrue();
        }

        [Test]
        public void CanPostJournal()
        {
            var postJournal = new PostJournal(this);
            postJournal.Execute(new PostJournalRequest());

            ExpectSaveToHaveBeenCalled();
        }
    }
}