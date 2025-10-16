using Project.Domain.Entities.Event;
using Project.Services.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Project.Domain.Entities.Users;
using Project.Domain.Enums; 

namespace Project.Tests
{
    public class EventServiceTests
    {
        [Fact]
        public void CreateEvent_ShouldAddEventToList()
        {
            var service = new EventService();
            var organizer = new User("organizer1", "organizer1@example.com", "EMP001", UserRole.ADMIN);
            var metadata = new EventMetadata(organizer);
            var schedule = new EventSchedule(DateTime.UtcNow, DateTime.UtcNow.AddHours(2));

            var ev = service.CreateEvent("Event 1", "Description", metadata, schedule);

            Assert.NotNull(ev);
            Assert.Single(service.GetAllEvents());
            Assert.Equal("Event 1", ev.Title);
        }

        [Fact]
        public void RegisterParticipant_ShouldAddParticipant()
        {
            var service = new EventService();
            var organizer = new User("organizer1", "organizer1@example.com", "EMP001", UserRole.ADMIN);
            var attendee = new User("attendee1", "attendee1@example.com", "EMP002", UserRole.ADMIN);
            var metadata = new EventMetadata(organizer);
            var schedule = new EventSchedule(DateTime.UtcNow, DateTime.UtcNow.AddHours(2));
            var ev = service.CreateEvent("Event 1", "Description", metadata, schedule);

            var participant = new EventParticipant(ev.Id, attendee.Id);
            service.RegisterParticipant(ev.Id, participant);

            Assert.Single(ev.Participants);
            Assert.Contains(participant, ev.Participants);
        }

        [Fact]
        public void RemoveParticipant_ShouldRemoveParticipant()
        {
            var service = new EventService();
            var organizer = new User("organizer1", "organizer1@example.com", "EMP001", UserRole.ADMIN);
            var attendee = new User("attendee1", "attendee1@example.com", "EMP002", UserRole.EMPLOYEE);
            var metadata = new EventMetadata(organizer);
            var schedule = new EventSchedule(DateTime.UtcNow, DateTime.UtcNow.AddHours(2));
            var ev = service.CreateEvent("Event 1", "Description", metadata, schedule);

            var participant = new EventParticipant(ev.Id, attendee.Id);
            service.RegisterParticipant(ev.Id, participant);
            service.RemoveParticipant(ev.Id, participant.UserId);

            Assert.Empty(ev.Participants);
        }

        [Fact]
        public void CancelEvent_ShouldMarkEventCancelled()
        {
            var service = new EventService();
            var organizer = new User("organizer1", "organizer1@example.com", "EMP001", UserRole.ADMIN);
            var metadata = new EventMetadata(organizer);
            var schedule = new EventSchedule(DateTime.UtcNow, DateTime.UtcNow.AddHours(2));
            var ev = service.CreateEvent("Event 1", "Description", metadata, schedule);

            service.CancelEvent(ev.Id);

            Assert.True(ev.IsCancelled);
        }

        [Fact]
        public void GetEventById_ShouldReturnCorrectEvent()
        {
            var service = new EventService();
            var organizer = new User("organizer1", "organizer1@example.com", "EMP001", UserRole.ADMIN);
            var metadata = new EventMetadata(organizer);
            var schedule = new EventSchedule(DateTime.UtcNow, DateTime.UtcNow.AddHours(2));
            var ev = service.CreateEvent("Event 1", "Description", metadata, schedule);

            var result = service.GetEventById(ev.Id);

            Assert.NotNull(result);
            Assert.Equal(ev.Id, result.Id);
        }

        [Fact]
        public void GetAllEvents_ShouldReturnAllEvents()
        {
            var service = new EventService();
            var organizer = new User("organizer1", "organizer1@example.com", "EMP001", UserRole.ADMIN);
            var metadata = new EventMetadata(organizer);
            var schedule = new EventSchedule(DateTime.UtcNow, DateTime.UtcNow.AddHours(2));
            var ev1 = service.CreateEvent("Event 1", "Description", metadata, schedule);
            var ev2 = service.CreateEvent("Event 2", "Description", metadata, schedule);

            var allEvents = service.GetAllEvents().ToList();

            Assert.Equal(2, allEvents.Count);
            Assert.Contains(ev1, allEvents);
            Assert.Contains(ev2, allEvents);
        }

        [Fact]
        public void RegisterParticipant_ForNonExistentEvent_ShouldThrow()
        {
            var service = new EventService();
            var participant = new EventParticipant(Guid.NewGuid(), Guid.NewGuid());

            var exception = Assert.Throws<InvalidOperationException>(() =>
                service.RegisterParticipant(Guid.NewGuid(), participant));

            Assert.Equal("Event not found.", exception.Message);
        }
    }
}
