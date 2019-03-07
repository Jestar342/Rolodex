using FluentAssertions;
using Rolodex.Messages.Events;
using Xunit;

namespace Rolodex.Tests.Models.RolodexEntryTests.Creation
{
    public class WhenCreatingARolodexEntry
    {
        readonly CreatingARolodexEntryTestContext _context;

        public WhenCreatingARolodexEntry()
        {
            _context = new CreatingARolodexEntryTestContext();
            _context.CreateNewRolodexEntry();
        }

        [Fact]
        public void ShouldCreateRolodexCreatedEvent()
            => _context.EntryAsAggregateRoot
                       .UnCommittedEvents
                       .Should()
                       .ContainSingle()
                       .And
                       .AllBeOfType<RolodexEntryCreated>();

        [Fact]
        public void ShouldHaveSameEmailAddress()
            => _context.UnCommittedEvent.EmailAddress.Should().Be(CreatingARolodexEntryTestContext.EmailAddress);

        [Fact]
        public void ShouldHaveSameFirstNames()
            => _context.UnCommittedEvent.FirstNames.Should().Be(CreatingARolodexEntryTestContext.FirstNames);

        [Fact]
        public void ShouldHaveSameSurname()
            => _context.UnCommittedEvent.Surname.Should().Be(CreatingARolodexEntryTestContext.Surname);

        [Fact]
        public void ShouldHaveSameCompanyName()
            => _context.UnCommittedEvent.CompanyName.Should().Be(CreatingARolodexEntryTestContext.CompanyName);

        [Fact]
        public void ShouldHaveSameTelephoneNumber()
            => _context.UnCommittedEvent.TelephoneNumber.Should().Be(CreatingARolodexEntryTestContext.TelephoneNumber);

        [Fact]
        public void ShouldHaveSameEntityId()
            => _context.UnCommittedEvent.EntityId.Should().Be(_context.EntityId);

        [Fact]
        public void ShouldHaveSameOccurredOn()
            => _context.UnCommittedEvent.OccurredOn.Should().Be(_context.OccurredOn);
    }
}