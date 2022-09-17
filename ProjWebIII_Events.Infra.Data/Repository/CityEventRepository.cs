using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ProjWebIII_Events.Core.Interfaces;
using ProjWebIII_Events.Core.Models;

namespace ProjWebIII_Events.Infra.Data.Repository
{
    public class CityEventRepository : ICityEventRepository
    {
        private readonly IConfiguration _configuration;
        public CityEventRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public List<CityEvent> GetAllEventsRep()
        {
            var query = "SELECT * FROM CityEvent";

            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            using var conn = new SqlConnection(connectionString);

            return conn.Query<CityEvent>(query).ToList();
        }

        public CityEvent GetEventByIdRep(long IdEvent)
        {
            var query = "SELECT * FROM CityEvent WHERE IdEvent = @IdEvent";

            var parameters = new DynamicParameters();
            parameters.Add("IdEvent", IdEvent);

            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            using var conn = new SqlConnection(connectionString);

            return conn.QueryFirst<CityEvent>(query, parameters);
        }

        public List<CityEvent> GetCityEventsByWordRep(string Title)
        {
            var query = "SELECT * from CityEvent WHERE Title LIKE ('%' + @title + '%')";

            var parameters = new DynamicParameters();
            parameters.Add("Title", Title);
            try
            {
                using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                return conn.Query<CityEvent>(query, parameters).ToList();
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error Type {ex.GetType().Name}Message {ex.Message}, Stack Trace{ex.StackTrace}");
                return null;
            }
        }

        public List<CityEvent> GetCityEventsByLocalAndDateRep(string local, DateTime dateHourEvent)
        {
            var query = "SELECT * from CityEvent WHERE Local = @Local AND CAST(DateHourEvent as DATE) = CAST(@dateHourEvent as DATE)";

            var parameters = new DynamicParameters();
            parameters.Add("Local", local);
            parameters.Add("DateHourEvent", dateHourEvent);

            try
            {
                using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                return conn.Query<CityEvent>(query, parameters).ToList();
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error Type {ex.GetType().Name}Message {ex.Message}, Stack Trace{ex.StackTrace}");
                return null;
            }
        }

        public bool InsertNewEventRep(CityEvent cityEvent)
        {
            var query = "INSERT INTO CityEvent VALUES (@Title, @Description, @DateHourEvent, @Local, @Address, @Price)";

            var parameters = new DynamicParameters(new
            {
                cityEvent.Title,
                cityEvent.Description,
                cityEvent.DateHourEvent,
                cityEvent.Local,
                cityEvent.Address,
                cityEvent.Price
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

        public bool UpdateEventRep(long IdEvent, CityEvent cityEvent)
        {
            var query = @"UPDATE CityEvent SET Title = @Title, 
Description = @Description, DateHourEvent = @DateHourEvent,
Local = @Local, Address = @Address, Price = @Price ";

            var parameters = new DynamicParameters(new
            {
                cityEvent.Title,
                cityEvent.Description,
                cityEvent.DateHourEvent,
                cityEvent.Local,
                cityEvent.Address,
                cityEvent.Price
            });

            try
            {
                var connectionString = _configuration.GetConnectionString("DefaultConnection");
                using var conn = new SqlConnection(connectionString);

                return conn.Execute(query, parameters) == 1;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Erro ao comunicar com banco (UPDATE), mensagem {ex.Message}, stack trace {ex.StackTrace}");

                return false;
            }
        }

        public bool DeleteEventRep(long IdEvent)
        {
            var query = "DELETE FROM CityEvent WHERE IdEvent = @IdEvent";

            var parameters = new DynamicParameters();
            parameters.Add("IdEvent", IdEvent);

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


        public bool UpdateEventRep(long IdEvent)
        {
            var query = "UPDATE CityEvent SET Status = 0 WHERE IdEvent = @IdEvent";
            var parameters = new DynamicParameters();
            parameters.Add("IdEvent", IdEvent);

            try
            {
                var connectionString = _configuration.GetConnectionString("DefaultConnection");
                using var conn = new SqlConnection(connectionString);

                return conn.Execute(query, parameters) == 1;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Erro ao comunicar com banco (UPDATE_STATUS), mensagem {ex.Message}, stack trace {ex.StackTrace}");

                return false;
            }
        }

        public List<CityEvent> GetCityEventsByPriceAndDateRep(decimal minPrice, decimal maxPrice, DateTime dateHourEvent)
        {
            var query = "SELECT * from CityEvent WHERE Price >= @minPrice AND Price <= @maxPrice AND CAST(DateHourEvent as DATE) = CAST(@dateHourEvent as DATE)";
            
            var parameters = new DynamicParameters();
            parameters.Add("minPrice", minPrice);
            parameters.Add("maxPrice", maxPrice);
            parameters.Add("DateHourEvent", dateHourEvent);
            try
            {
                using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                return conn.Query<CityEvent>(query, parameters).ToList();
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error Type {ex.GetType().Name}Message {ex.Message}, Stack Trace{ex.StackTrace}");
                return null;
            }
        }
    }
}
