using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjWebIII_Events.Core.Interfaces;
using ProjWebIII_Events.Core.Models;
using ProjWebIII_Events.Filters;

namespace ProjWebIII_Events.Controllers
{
    [Route("api/[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ApiController]
    public class EventReservationController : ControllerBase
    {
        public List<EventReservation> eventReservationList = new()
        {
        };

        public IEventReservationService _eventReservationService;
        public EventReservationController(IEventReservationService eventReservationService)
        {
            _eventReservationService = eventReservationService;
        }

        #region GetAllReservations
        [HttpGet("/reservations/search/all_reservations")]//consulta todas as reservas.
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<EventReservation>> GetAllReservations()
        {
            var search = _eventReservationService.GetAllReservations();
            return Ok(search);
        }
        #endregion

        #region GetReservationByName
        [HttpGet("/reservations/search/reservation_{personName}")]// consulta as reservas com base no nome informado.
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<EventReservation>> GetReservationByName(string personName)
        {
            var search = _eventReservationService.GetReservationByName(personName);
            if (search == null)
            {
                return NotFound();
            }
            return Ok(search);
        }
        #endregion

        #region GetReservationByNameAndTitle
        [HttpGet("/reservations/search/reservation_{personName}_{title}")]
        [Authorize("admin, cliente")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = ("admin, cliente"))]
        public ActionResult<List<Object>> GetReservationByNameAndTitle(string personName, string title)
        {
            var search = _eventReservationService.GetReservationByNameAndTitle(personName, title);
            if (search == null)
            {
                return NotFound();
            }
            return Ok(search);
        }
        #endregion

        #region GetReservationById
        [HttpGet("/reservations/search/reservation_{IdReservation}")]// consulta as reservas com base no ID informado.
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ServiceFilter(typeof(EventExistsById))]
        public ActionResult<EventReservation> GetReservationById(long IdReservation)
        {
            var search = _eventReservationService.GetReservationById(IdReservation);
            if (search == null)
            {
                return NotFound();
            }
            return Ok(search);
        }
        #endregion

        #region InsertReservation
        [HttpPost("/reservations/newreservation")]//insere uma nova reserva.
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = ("admin, cliente"))]

        public IActionResult InsertReservation(EventReservation eventReservation)
        {
            var newReservation = _eventReservationService.InsertReservation(eventReservation);
            if (!newReservation)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            return CreatedAtAction(nameof(InsertReservation), eventReservation);
        }
        #endregion

        #region UpdateReservationQuantity
        [HttpPut("/reservations/update")]// atualiza a quantidade de reservas
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ServiceFilter(typeof(EventExistsById))]
        [Authorize(Roles = ("admin"))]
        public IActionResult UpdateReservationQuantity(long IdReservation, long Quantity)
        {
            var update = _eventReservationService.UpdateReservationQuantity(IdReservation, Quantity);
            if (!update)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            return Ok();

        }
        #endregion

        #region DeleteReservation
        [HttpDelete("/reservations/delete")]// deleta reserva com base no id informado.
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ServiceFilter(typeof(EventExistsById))]
        [Authorize(Roles = ("admin"))]

        public ActionResult<List<EventReservation>> DeleteReservation(long IdReservation)
        {
            if (!_eventReservationService.DeleteReservation(IdReservation))
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            return Ok(eventReservationList);
        }
        #endregion
    }
}

