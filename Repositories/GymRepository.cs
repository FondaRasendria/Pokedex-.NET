using Npgsql;
using Pokedex.Context;
using Pokedex.Models;
using System.Data;
using Dapper;

namespace Pokedex.Repositories
{
    public class GymRepository
    {
        private readonly AppDbContext _context;
        private readonly IDbConnection _db;
        private readonly ILogger<GymRepository> _logger;

        public GymRepository(AppDbContext context, IConfiguration configuration, ILogger<GymRepository> logger)
        {
            _context = context;
            _db = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection"));
            _logger = logger;
        }

        public async Task<IEnumerable<Gym>> GetAll()
        {
            try
            {
                var sql = "SELECT * FROM Gym ORDER BY id";
                return await _db.QueryAsync<Gym>(sql);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<Gym> GetById(int id)
        {
            try
            {
                var sql = @"SELECT * FROM Gym WHERE id = @Id";
                return await _db.QuerySingleOrDefaultAsync<Gym>(sql, new {Id = id});
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<Gym> GetByName(string name)
        {
            try
            {
                var sql = @"SELECT * FROM Gym WHERE name = @Name";
                return await _db.QuerySingleOrDefaultAsync<Gym>(sql, new { Name = name});
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<Gym> Create(Gym gym)
        {
            try
            {
                var maxIdSql = @"SELECT COALESCE(MAX(id), 0) FROM Gym";
                var maxId = await _db.QuerySingleAsync<int>(maxIdSql);
                gym.id = maxId + 1;

                var sql = @"INSERT INTO Gym (id, name) VALUES (@id, @name)";

                await _db.ExecuteAsync(sql, gym);
                return gym;
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<Gym> Update(int id, Gym gym)
        {
            try
            {
                var sql = @"UPDATE Gym SET name = @name WHERE id = @id";

                int updatedRows = await _db.ExecuteAsync(sql, gym);
                if(updatedRows == 0)
                {
                    throw new Exception("Tidak ada gym ditemukan dengan id " + id);
                }
                return gym;
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<Gym> Delete(int id)
        {
            try
            {
                var select = @"SELECT * FROM Gym WHERE id = @Id";
                Gym gym = await _db.QuerySingleOrDefaultAsync<Gym>(select, new { Id = id });

                if(gym == null)
                {
                    throw new Exception("Gym tidak ditemukan dengan id " + id);
                }
                var sql = @"DELETE FROM Gym WHERE id = @Id";
                var deletedRows = await _db.QuerySingleOrDefaultAsync(sql, new { Id = id });

                return gym;
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                throw new Exception(ex.Message);
            }
        }
    }
}
