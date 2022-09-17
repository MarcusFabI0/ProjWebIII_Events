using ProjWebIII_Events.Core.Models;

namespace ProjWebIII_Events.Core.Interfaces
{
    public interface ICityEventService
    {
        List<CityEvent> GetAllEvents();
        public CityEvent GetEventById(long IdEvent);
        List<CityEvent> GetCityEventsByWord(string Title);
        public List<CityEvent> GetCityEventsByLocalAndDate(string local, DateTime dateHourEvent);
        public List<CityEvent> GetCityEventsByPriceAndDate(decimal minPrice, decimal maxPrice, DateTime dateHourEvent);
        public bool InsertNewEvent(CityEvent cityEvent);
        public bool UpdateEvent(long IdEvent, CityEvent cityEvent);
        public bool DeleteEvent(long IdEvent);

       
    }
}
