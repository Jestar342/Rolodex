using FluentAssertions;
using Xunit;

namespace Rolodex.Tests.Models.RolodexEntryTests.Creation {
    public class WhenReplayingARolodexEntryCreatedEvent
    {
        readonly CreatingARolodexEntryTestContext _context;

        public WhenReplayingARolodexEntryCreatedEvent()
        {
            _context = new CreatingARolodexEntryTestContext();
            _context.ReplayEvent();
        }

        [Fact]
        public void ShouldApplyEmailAddress()
            => _context.Entry.EmailAddress.Should().Be(CreatingARolodexEntryTestContext.EmailAddress);

        [Fact]
        public void ShouldApplyCompanyName()
            => _context.Entry.CompanyName.Should().Be(CreatingARolodexEntryTestContext.CompanyName);

        [Fact]
        public void ShouldApplyTelephoneNumber()
            => _context.Entry.TelephoneNumber.Should().Be(CreatingARolodexEntryTestContext.TelephoneNumber);

        [Fact]
        public void ShouldApplyFirstNames()
            => _context.Entry.FirstNames.Should().Be(CreatingARolodexEntryTestContext.FirstNames);

        [Fact]
        public void ShouldApplySurname()
            => _context.Entry.Surname.Should().Be(CreatingARolodexEntryTestContext.Surname);

        [Fact]
        public void ShouldApplyEntityId()
            => _context.Entry.EntityId.Should().Be(_context.EntityId);
    }
}