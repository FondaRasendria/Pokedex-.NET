using Dapper;
using Npgsql;
using Pokedex.Context;
using Pokedex.Models;
using System.Data;

namespace Pokedex.Repositories
{
    public class TypeRepository
    {
        private readonly AppDbContext _context;
        private readonly IDbConnection _db;
        private readonly ILogger _logger;

        public TypeRepository (AppDbContext context, IConfiguration configuration, ILogger<TypeRepository> logger)
        {
            _context = context;
            _db = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection"));
            _logger = logger;
        }

        public async Task<IEnumerable<Models.Type>> GetAll()
        {
            try
            {
                var sql = "SELECT * FROM Type ORDER BY id";
                return await _db.QueryAsync<Models.Type>(sql);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<Models.Type> GetById(int id)
        {
            try
            {
                var sql = @"SELECT * FROM Type WHERE id = @Id";
                return await _db.QuerySingleOrDefaultAsync<Models.Type>(sql, new { Id = id });
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<Models.Type> GetByName(string name)
        {
            try
            {
                var sql = @"SELECT * FROM Type WHERE name = @Name";
                return await _db.QuerySingleOrDefaultAsync<Models.Type>(sql, new { Name = name });
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<Models.Type> Create(Models.Type type)
        {
            try
            {
                var maxIdSql = @"SELECT COALESCE(MAX(id), 0) FROM Type";
                var maxId = await _db.QuerySingleAsync<int>(maxIdSql);
                type.id = maxId + 1;

                var sql = @"INSERT INTO Type (id, name) VALUES (@id, @name)";

                await _db.ExecuteAsync(sql, type);
                return type;
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<Models.Type> Update(int id, Models.Type type)
        {
            try
            {
                var sql = @"UPDATE Type SET name = @name WHERE id = @id";

                int updatedRows = await _db.ExecuteAsync(sql, type);
                if (updatedRows == 0)
                {
                    throw new Exception("Tidak ada type ditemukan dengan id " + id);
                }
                return type;
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<Models.Type> Delete(int id)
        {
            try
            {
                var select = @"SELECT * FROM Type WHERE id = @Id";
                Models.Type type = await _db.QuerySingleOrDefaultAsync<Models.Type>(select, new { Id = id });

                if (type == null)
                {
                    throw new Exception("Type tidak ditemukan dengan id " + id);
                }
                var sql = @"DELETE FROM Type WHERE id = @Id";
                var deletedRows = await _db.QuerySingleOrDefaultAsync(sql, new { Id = id });

                return type;
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                throw new Exception(ex.Message);
            }
        }
    }
}
