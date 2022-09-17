using ProjWebIII_Events.Core.Interfaces;
using ProjWebIII_Events.Core.Models;

namespace ProjWebIII_Events.Core.Services
{
    public class CityEventService : ICityEventService
    {

        public ICityEventRepository _cityEventRepository;
        public IEventReservationService _eventReservationService;
        public CityEventService(ICityEventRepository cityEventRepository, IEventReservationService eventReservationService)
        {
            _cityEventRepository = cityEventRepository;
            _eventReservationService = eventReservationService;
        }

        public List<CityEvent> GetAllEvents()
        {
            return _cityEventRepository.GetAllEventsRep();
        }
        public CityEvent GetEventById(long IdEvent)
        {
            return _cityEventRepository.GetEventByIdRep(IdEvent);
        }

        public List<CityEvent> GetCityEventsByWord(string Title)
        {
            return _cityEventRepository.GetCityEventsByWordRep(Title);
        }

        public List<CityEvent> GetCityEventsByLocalAndDate(string local, DateTime dateHourEvent)
        {
            return _cityEventRepository.GetCityEventsByLocalAndDateRep(local, dateHourEvent);
        }
        public List<CityEvent> GetCityEventsByPriceAndDate(decimal minPrice, decimal maxPrice, DateTime dateHourEvent)
        {
            return _cityEventRepository.GetCityEventsByPriceAndDateRep(minPrice, maxPrice, dateHourEvent);
        }
        public bool InsertNewEvent(CityEvent cityEvent)
        {
            return _cityEventRepository.InsertNewEventRep(cityEvent);
        }

        public bool UpdateEvent(long IdEvent, CityEvent cityEvent)
        {
            return _cityEventRepository.UpdateEventRep(IdEvent, cityEvent);
        }

        public bool DeleteEvent(long IdEvent)
        {
            if (_eventReservationService.CheckReservation(IdEvent))
            {
                return _cityEventRepository.UpdateEventRep(IdEvent);
            }
            return _cityEventRepository.DeleteEventRep(IdEvent);
        }

        
    }
}

