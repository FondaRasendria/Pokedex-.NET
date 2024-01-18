using Npgsql;
using Pokedex.Context;
using System.Data;
using Dapper;
using Pokedex.Models;

namespace Pokedex.Repositories
{
    public class PokemonRepository
    {
        private readonly AppDbContext _context;
        private readonly IDbConnection _db;
        private readonly ILogger _logger;

        public PokemonRepository(AppDbContext context, IConfiguration configuration, ILogger<PokemonRepository> logger)
        {
            _context = context;
            _db = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection"));
            _logger = logger;
        }

        public async Task<IEnumerable<Pokemon>> GetAll()
        {
            try
            {
                var sql = "SELECT * FROM Pokemon ORDER BY id";
                return await _db.QueryAsync<Pokemon>(sql);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<Pokemon> GetById(int id)
        {
            try
            {
                var sql = @"SELECT * FROM Pokemon WHERE id = @Id";
                return await _db.QuerySingleOrDefaultAsync<Pokemon>(sql, new { Id = id });
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<Pokemon> GetByName(string name)
        {
            try
            {
                var sql = @"SELECT * FROM Pokemon WHERE name = @Name";
                return await _db.QuerySingleOrDefaultAsync<Pokemon>(sql, new { Name = name });
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<Pokemon> Create(Pokemon pokemon)
        {
            try
            {
                var imageLink = "https://assets.pokemon.com/assets/cms2/img/pokedex/full/";
                string newId = pokemon.id.ToString();
                while(newId.Length < 3)
                {
                    newId = "0" + newId;
                }
                imageLink = imageLink + newId + ".png";
                pokemon.image = imageLink;

                var sql = @"INSERT INTO Pokemon (id, name, image) VALUES (@id, @name, @image)";

                await _db.ExecuteAsync(sql, pokemon);
                return pokemon;
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<Pokemon> Update(int id, Pokemon pokemon)
        {
            try
            {
                var sql = @"UPDATE Pokemon SET name = @name, image = @image WHERE id = @id";

                int updatedRows = await _db.ExecuteAsync(sql, pokemon);
                if (updatedRows == 0)
                {
                    throw new Exception("Tidak ada pokemon ditemukan dengan id " + id);
                }
                return pokemon;
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<Pokemon> Delete(int id)
        {
            try
            {
                var select = @"SELECT * FROM Pokemon WHERE id = @Id";
                Pokemon pokemon = await _db.QuerySingleOrDefaultAsync<Pokemon>(select, new { Id = id });

                if (pokemon == null)
                {
                    throw new Exception("Pokemon tidak ditemukan dengan id " + id);
                }
                var sql = @"DELETE FROM Pokemon WHERE id = @Id";
                var deletedRows = await _db.QuerySingleOrDefaultAsync(sql, new { Id = id });

                return deletedRows;
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                throw new Exception(ex.Message);
            }
        }
    }
}
