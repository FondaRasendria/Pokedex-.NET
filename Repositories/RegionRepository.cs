using Npgsql;
using Pokedex.Context;
using Pokedex.Models;
using System.Data;
using Dapper;

namespace Pokedex.Repositories
{
    public class RegionRepository
    {
        private readonly AppDbContext _context;
        private readonly IDbConnection _db;
        private readonly ILogger<RegionRepository> _logger;

        public RegionRepository(AppDbContext context, IConfiguration configuration, ILogger<RegionRepository> logger)
        {
            _context = context;
            _db = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection"));
            _logger = logger;
        }

        public async Task<IEnumerable<Region>> GetAll()
        {
            try
            {
                var sql = "SELECT * FROM Region ORDER BY id";
                return await _db.QueryAsync<Region>(sql);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<Region> GetById(int id)
        {
            try
            {
                var sql = @"SELECT * FROM Region WHERE id = @Id";
                return await _db.QuerySingleOrDefaultAsync<Region>(sql, new { Id = id });
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<Region> GetByName(string name)
        {
            try
            {
                var sql = @"SELECT * FROM Region WHERE name = @Name";
                return await _db.QuerySingleOrDefaultAsync<Region>(sql, new { Name = name });
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<Region> Create(Region region)
        {
            try
            {
                var maxIdSql = @"SELECT COALESCE(MAX(id), 0) FROM Region";
                var maxId = await _db.QuerySingleAsync<int>(maxIdSql);
                region.id = maxId + 1;

                var sql = @"INSERT INTO Region (id, name, image) VALUES (@id, @name, @image)";

                await _db.ExecuteAsync(sql, region);
                return region;
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<Region> Update(int id, Region region)
        {
            try
            {
                var sql = @"UPDATE Region SET name = @name, image = @image WHERE id = @id";

                int updatedRows = await _db.ExecuteAsync(sql, region);
                if (updatedRows == 0)
                {
                    throw new Exception("Tidak ada region ditemukan dengan id " + id);
                }
                return region;
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<Region> Delete(int id)
        {
            try
            {
                var select = @"SELECT * FROM Region WHERE id = @Id";
                Region region = await _db.QuerySingleOrDefaultAsync<Region>(select, new { Id = id });

                if (region == null)
                {
                    throw new Exception("Region tidak ditemukan dengan id " + id);
                }
                var sql = @"DELETE FROM Region WHERE id = @Id";
                var deletedRows = await _db.QuerySingleOrDefaultAsync(sql, new { Id = id });

                return region;
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                throw new Exception(ex.Message);
            }
        }
    }
}
