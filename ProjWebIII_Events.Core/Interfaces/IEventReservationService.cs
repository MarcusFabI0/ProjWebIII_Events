using ProjWebIII_Events.Core.Models;

namespace ProjWebIII_Events.Core.Interfaces
{
    public interface IEventReservationService
    {
        List<EventReservation> GetAllReservations();
        List<EventReservation> GetReservationByName(string personName);
        EventReservation GetReservationById(long IdReservation);
        List<Object> GetReservationByNameAndTitle(string personName, string title);
        bool InsertReservation(EventReservation eventReservation);
        bool UpdateReservationQuantity(long IdReservation, long Quantity);
        bool DeleteReservation(long IdEvent);
        bool CheckReservation(long IdEvent);
    }
}
