using Npgsql;
using Pokedex.Context;
using Pokedex.Models;
using System.Data;
using Dapper;

namespace Pokedex.Repositories
{
    public class PokemonTypeRepository
    {
        private readonly AppDbContext _context;
        private readonly IDbConnection _db;
        private readonly ILogger _logger;

        public PokemonTypeRepository(AppDbContext context, IConfiguration configuration, ILogger<PokemonTypeRepository> logger)
        {
            _context = context;
            _db = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection"));
            _logger = logger;
        }

        public async Task<IEnumerable<PokemonType>> GetAll()
        {
            try
            {
                var sql = "SELECT * FROM PokemonType ORDER BY id";
                return await _db.QueryAsync<PokemonType>(sql);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<PokemonType> GetById(int id)
        {
            try
            {
                var sql = @"SELECT * FROM PokemonType WHERE id = @Id";
                return await _db.QuerySingleOrDefaultAsync<PokemonType>(sql, new { Id = id });
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<PokemonType>> GetByPokemonId(int id)
        {
            try
            {
                var sql = @"SELECT * FROM PokemonType WHERE pokemonId = @Id";
                return await _db.QueryAsync<PokemonType>(sql, new { Id = id });
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<PokemonType> Create(PokemonType pokemontype)
        {
            try
            {
                var maxIdSql = @"SELECT COALESCE(MAX(id), 0) FROM PokemonType";
                var maxId = await _db.QuerySingleAsync<int>(maxIdSql);
                pokemontype.id = maxId + 1;

                var sql = @"INSERT INTO PokemonType (id, pokemonid, typeid) VALUES (@id, @pokemonId, @typeId)";

                await _db.ExecuteAsync(sql, pokemontype);
                return pokemontype;
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<PokemonType> Update(int id, PokemonType pokemontype)
        {
            try
            {
                var sql = @"UPDATE PokemonType SET pokemonid = @pokemonId, typeid = @typeId WHERE id = @id";

                int updatedRows = await _db.ExecuteAsync(sql, pokemontype);
                if (updatedRows == 0)
                {
                    throw new Exception("Tidak ada pokemontype ditemukan dengan id " + id);
                }
                return pokemontype;
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<PokemonType> Delete(int id)
        {
            try
            {
                var select = @"SELECT * FROM PokemonType WHERE id = @Id";
                PokemonType pokemontype = await _db.QuerySingleOrDefaultAsync<PokemonType>(select, new { Id = id });

                if (pokemontype == null)
                {
                    throw new Exception("PokemonType tidak ditemukan dengan id " + id);
                }
                var sql = @"DELETE FROM PokemonType WHERE id = @Id";
                var deletedRows = await _db.QuerySingleOrDefaultAsync(sql, new { Id = id });

                return pokemontype;
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                throw new Exception(ex.Message);
            }
        }
    }
}
