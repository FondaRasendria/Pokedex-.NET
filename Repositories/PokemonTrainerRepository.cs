using Npgsql;
using Pokedex.Context;
using Pokedex.Models;
using System.Data;
using Dapper;

namespace Pokedex.Repositories
{
    public class PokemonTrainerRepository
    {
        private readonly AppDbContext _context;
        private readonly IDbConnection _db;
        private readonly ILogger _logger;

        public PokemonTrainerRepository(AppDbContext context, IConfiguration configuration, ILogger<PokemonTrainerRepository> logger)
        {
            _context = context;
            _db = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection"));
            _logger = logger;
        }

        public async Task<IEnumerable<PokemonTrainer>> GetAll()
        {
            try
            {
                var sql = "SELECT * FROM PokemonTrainer ORDER BY id";
                return await _db.QueryAsync<PokemonTrainer>(sql);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<PokemonTrainer> GetById(int id)
        {
            try
            {
                var sql = @"SELECT * FROM PokemonTrainer WHERE id = @Id";
                return await _db.QuerySingleOrDefaultAsync<PokemonTrainer>(sql, new { Id = id });
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<PokemonTrainer>> GetByTrainerId(int id)
        {
            try
            {
                var sql = @"SELECT * FROM PokemonTrainer WHERE trainerid = @Id";
                return await _db.QueryAsync<PokemonTrainer>(sql, new { Id = id });
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<PokemonTrainer> Create(PokemonTrainer pokemontrainer)
        {
            try
            {
                var maxIdSql = @"SELECT COALESCE(MAX(id), 0) FROM PokemonTrainer";
                var maxId = await _db.QuerySingleAsync<int>(maxIdSql);
                pokemontrainer.id = maxId + 1;

                var sql = @"INSERT INTO PokemonTrainer (id, pokemonid, trainerid) VALUES (@id, @pokemonId, @trainerId)";

                await _db.ExecuteAsync(sql, pokemontrainer);
                return pokemontrainer;
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<PokemonTrainer> Update(int id, PokemonTrainer pokemontrainer)
        {
            try
            {
                var sql = @"UPDATE PokemonTrainer SET pokemonid = @pokemonId, trainerid = @trainerId WHERE id = @id";

                int updatedRows = await _db.ExecuteAsync(sql, pokemontrainer);
                if (updatedRows == 0)
                {
                    throw new Exception("Tidak ada pokemontrainer ditemukan dengan id " + id);
                }
                return pokemontrainer;
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<PokemonTrainer> Delete(int id)
        {
            try
            {
                var select = @"SELECT * FROM PokemonTrainer WHERE id = @Id";
                PokemonTrainer pokemontrainer = await _db.QuerySingleOrDefaultAsync<PokemonTrainer>(select, new { Id = id });

                if (pokemontrainer == null)
                {
                    throw new Exception("PokemonTrainer tidak ditemukan dengan id " + id);
                }
                var sql = @"DELETE FROM PokemonTrainer WHERE id = @Id";
                var deletedRows = await _db.QuerySingleOrDefaultAsync(sql, new { Id = id });

                return pokemontrainer;
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                throw new Exception(ex.Message);
            }
        }
    }
}
