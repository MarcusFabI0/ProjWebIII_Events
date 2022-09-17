using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjWebIII_Events.Core.Interfaces;
using ProjWebIII_Events.Core.Models;
using ProjWebIII_Events.Filters;

namespace ProjWebIII_Events.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [TypeFilter(typeof(LogResourceFilter))]
    
    public class CityEventController : ControllerBase
    {
        public List<CityEvent> cityEventList = new()
        {
        };

        public ICityEventService _cityEventService;
        public CityEventController(ICityEventService cityEventService)
        {
            _cityEventService = cityEventService;
        }

        #region GetAllEvents
        [HttpGet("/events/search/all_events")]//consulta todos o eventos.
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<List<CityEvent>> GetAllEvents()
        {
            var search = _cityEventService.GetAllEvents();

            return Ok(search);
        }
        #endregion

        #region GetEventById
        [HttpGet("/events/search/event_{IdEvent}")]// consulta os eventos com base no ID informado.
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public ActionResult<CityEvent> GetEventbyId(long IdEvent)
        {
            var search = _cityEventService.GetEventById(IdEvent);

            if (search == null)
            {
                return NotFound();
            }

            return Ok(search);
        }
        #endregion

        #region GetCityEventsByWord
        [HttpGet("/events/search/{title}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<CityEvent>> GetCityEventsByWord(string Title)
        {
            var search = _cityEventService.GetCityEventsByWord(Title);

            if (search == null)
            {
                return NoContent();
            }

            return Ok(search);
        }
        #endregion

        #region GetCityEventsByLocalAndDate
        [HttpGet("/events/search/{local}_{dateHourEvent}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<CityEvent>> GetCityEventsByLocalAndDate(string local, DateTime dateHourEvent)
        {
            var search = _cityEventService.GetCityEventsByLocalAndDate(local, dateHourEvent);

            if (search == null)
            {
                return NoContent();
            }

            return Ok(search);
        }
        #endregion

        #region GetCityEventsByPriceAndDate
        [HttpGet("/events/search/{minPrice}_{maxPrice}_{dateHourEvent}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<CityEvent> GetCityEventsByPriceAndDate(decimal minPrice, decimal maxPrice, DateTime dateHourEvent)
        {
            var cityEvents = _cityEventService.GetCityEventsByPriceAndDate(minPrice, maxPrice, dateHourEvent);

            if (cityEvents == null)
            {
                return NoContent();
            }

            return Ok(cityEvents);
        }
        #endregion

        #region InsertNewEvent
        [HttpPost("/events/newevent")]//insere um novo evento
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [TypeFilter(typeof(LogActionFilter))]
        [Authorize(Roles = ("admin"))]
        public IActionResult InsertNewEvent(CityEvent cityEvent)
        {
            var newEvent = _cityEventService.InsertNewEvent(cityEvent);
            if (!newEvent)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            return CreatedAtAction(nameof(InsertNewEvent), cityEvent);
        }
        #endregion

        #region UpdateEvent
        [HttpPut("/events/update")]//atualiza o evento
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ServiceFilter(typeof(EventExistsById))]
        [Authorize(Roles = ("admin"))]
        public IActionResult UpdateEvent(long IdEvent, CityEvent cityEvent)
        {
            if (!_cityEventService.UpdateEvent(IdEvent, cityEvent))
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            return NoContent();

        }
        #endregion

        #region DeleteEvent
        [HttpDelete("/events/delete")]//deleta o evento
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ServiceFilter(typeof(EventExistsById))]
        [Authorize(Roles = ("admin"))]
        public ActionResult<List<CityEvent>> DeleteEvent(long IdEvent)
        {
            var delete = _cityEventService.DeleteEvent(IdEvent);
            if (!delete)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            return Ok(delete);
        }
        #endregion

    }
}



