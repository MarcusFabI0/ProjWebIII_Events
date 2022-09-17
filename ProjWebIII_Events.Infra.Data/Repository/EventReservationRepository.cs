using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ProjWebIII_Events.Core.Interfaces;
using ProjWebIII_Events.Core.Models;

namespace ProjWebIII_Events.Infra.Data.Repository
{
    public class EventReservationRepository : IEventReservationRepository
    {
        private readonly IConfiguration _configuration;
        public EventReservationRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<EventReservation> GetAllReservationsRep()
        {
            var query = "SELECT * FROM EventReservation";

            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            using var conn = new SqlConnection(connectionString);

            return conn.Query<EventReservation>(query).ToList();
        }

        public EventReservation GetReservationByIdRep(long IdReservation)
        {
            var query = "SELECT * FROM EventReservation WHERE IdReservation = @IdReservation";

            var parameters = new DynamicParameters();
            parameters.Add("IdReservation", IdReservation);

            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            using var conn = new SqlConnection(connectionString);

            return conn.QueryFirst<EventReservation>(query, parameters);
        }

        public List<EventReservation> GetReservationByNameRep(string personName)
        {
            var query = "SELECT * FROM EventReservation WHERE PersonName = @PersonName";

            var parameters = new DynamicParameters();
            parameters.Add("PersonName", personName);

            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            using var conn = new SqlConnection(connectionString);

            return conn.Query<EventReservation>(query, parameters).ToList();
        }

        public List<Object> GetReservationByNameAndTitleRep(string personName, string title)
        {
            var query = @"SELECT * FROM CityEvent AS Event INNER JOIN EventReservation AS Reservation ON Event.title LIKE ('%' + @title + '%') 
AND Reservation.personName = @personName AND Event.idEvent = Reservation.idEvent;";
            DynamicParameters parameters = new(new { personName, title });
            try
            {
                using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                return conn.Query<Object>(query, parameters).ToList();
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error Type {ex.GetType().Name}Message {ex.Message}, Stack Trace{ex.StackTrace}, Target {ex.TargetSite}");
                return null;
            }
        }

        public bool InsertReservationRep(EventReservation eventReservation)
        {
            var query = "INSERT INTO EventReservation VALUES (@PersonName,@Quantity)";

            var parameters = new DynamicParameters(new
            {
                eventReservation.PersonName,
                eventReservation.Quantity,

            });

            try
            {
                var connectionString = _configuration.GetConnectionString("DefaultConnection");
                using var conn = new SqlConnection(connectionString);

                return conn.Execute(query, parameters) == 1;
            }
            catch (ArgumentException ex)
            {

                Console.WriteLine($"Erro ao comunicar com banco (INSERT), mensagem {ex.Message}, stack trace {ex.StackTrace}");

                return false;
            }
        }

        public bool UpdateReservationRep(long IdReservation, long Quantity)
        {
            var query = "UPDATE EventReservation set Quantity = @Quantity WHERE IdReservation = @IdReservation";

            var parameters = new DynamicParameters();

            parameters.Add("IdReservation", IdReservation);
            parameters.Add("Quantity", Quantity);

            try
            {
                using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                return conn.Execute(query, parameters) == 1;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error Type {ex.GetType().Name}Message {ex.Message}, Stack Trace{ex.StackTrace}");
                return false;
            }
        }

        public bool DeleteReservationEventRep(long IdReservation)
        {
            var query = "DELETE FROM EventReservation WHERE IdReservation = @IdReservation";

            var parameters = new DynamicParameters();
            parameters.Add("IdReservation", IdReservation);

            try
            {
                var connectionString = _configuration.GetConnectionString("DefaultConnection");
                using var conn = new SqlConnection(connectionString);

                return conn.Execute(query, parameters) == 1;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Erro ao comunicar com banco (DELETE), mensagem {ex.Message}, stack trace {ex.StackTrace}");

               return false;
            }
        }

        public bool CheckReservationRep(long IdEvent)
        {
            var query = "SELECT * FROM EventReservation WHERE IdEvent = @IdEvent";

            var parameters = new DynamicParameters();
            parameters.Add("IdEvent", IdEvent);

            try
            {
                var connectionString = _configuration.GetConnectionString("DefaultConnection");
                using var conn = new SqlConnection(connectionString);


                if(conn.Query<EventReservation>(query).Count() == 0)
                {

                    return false;
                }


                return true;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Erro ao comunicar com banco (CHECK_RESERVATION), mensagem {ex.Message}, stack trace {ex.StackTrace}");

                return false;
            }
        }


    }
}



