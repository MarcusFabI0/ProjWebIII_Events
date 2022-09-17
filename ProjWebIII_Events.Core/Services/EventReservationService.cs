using ProjWebIII_Events.Core.Interfaces;
using ProjWebIII_Events.Core.Models;

namespace ProjWebIII_Events.Core.Services
{
    public class EventReservationService : IEventReservationService
    {
        public IEventReservationRepository _eventReservationRepository;

        public EventReservationService(IEventReservationRepository eventReservationRepository)

        {
            _eventReservationRepository = eventReservationRepository;
        }

        public List<EventReservation> GetAllReservations()
        {
            return _eventReservationRepository.GetAllReservationsRep();
        }

        public List<EventReservation> GetReservationByName(string personName)
        {
            return _eventReservationRepository.GetReservationByNameRep(personName);
        }

        public EventReservation GetReservationById(long IdReservation)
        {
            return _eventReservationRepository.GetReservationByIdRep(IdReservation);
        }
        public List<object> GetReservationByNameAndTitle(string personName, string title)
        {
            return _eventReservationRepository.GetReservationByNameAndTitleRep(personName, title);
        }
        public bool InsertReservation(EventReservation eventReservation)
        {
            return _eventReservationRepository.InsertReservationRep(eventReservation);
        }

        public bool UpdateReservationQuantity(long IdReservation, long Quantity)
        {
            return _eventReservationRepository.UpdateReservationRep(IdReservation, Quantity);
        }

        public bool DeleteReservation(long IdEvent)
        {
            return _eventReservationRepository.DeleteReservationEventRep(IdEvent);
        }

        public bool CheckReservation(long IdEvent)
        {
            return _eventReservationRepository.CheckReservationRep(IdEvent);
        }

        
    }
}
