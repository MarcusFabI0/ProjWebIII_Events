using ProjWebIII_Events.Core.Models;

namespace ProjWebIII_Events.Core.Interfaces
{
    public interface IEventReservationRepository
    {
        List<EventReservation> GetAllReservationsRep();
        List<EventReservation> GetReservationByNameRep(string personName);
        EventReservation GetReservationByIdRep(long IdReservation);
        public List<Object> GetReservationByNameAndTitleRep(string personName, string title);
        bool InsertReservationRep(EventReservation eventReservation);
        bool UpdateReservationRep(long IdReservation, long Quantity);
        bool DeleteReservationEventRep(long IdEvent);
        bool CheckReservationRep(long IdEvent);
    }
}
