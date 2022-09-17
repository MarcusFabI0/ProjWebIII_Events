using ProjWebIII_Events.Core.Models;

namespace ProjWebIII_Events.Core.Interfaces
{
    public interface ICityEventRepository
    {
        List<CityEvent> GetAllEventsRep();
        public CityEvent GetEventByIdRep(long IdEvent);
        public List<CityEvent> GetCityEventsByWordRep(string Title);
        public List<CityEvent> GetCityEventsByLocalAndDateRep(string local, DateTime dateHourEvent);
        public List<CityEvent> GetCityEventsByPriceAndDateRep(decimal minPrice, decimal maxPrice, DateTime date);
        public bool InsertNewEventRep(CityEvent cityEvent);
        public bool UpdateEventRep(long IdEvent, CityEvent cityEvent);
        public bool DeleteEventRep(long IdEvent);
        public bool UpdateEventRep(long IdEvent);
    }
}
