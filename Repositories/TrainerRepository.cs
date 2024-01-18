using Npgsql;
using Pokedex.Context;
using Pokedex.Models;
using System.Data;
using Dapper;

namespace Pokedex.Repositories
{
    public class TrainerRepository
    {
        private readonly AppDbContext _context;
        private readonly IDbConnection _db;
        private readonly ILogger _logger;

        public TrainerRepository(AppDbContext context, IConfiguration configuration, ILogger<TrainerRepository> logger)
        {
            _context = context;
            _db = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection"));
            _logger = logger;
        }

        public async Task<IEnumerable<Trainer>> GetAll()
        {
            try
            {
                var sql = "SELECT * FROM Trainer ORDER BY id";
                return await _db.QueryAsync<Trainer>(sql);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<Trainer> GetById(int id)
        {
            try
            {
                var sql = @"SELECT * FROM Trainer WHERE id = @Id";
                return await _db.QuerySingleOrDefaultAsync<Trainer>(sql, new { Id = id });
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<Trainer> GetByName(string name)
        {
            try
            {
                var sql = @"SELECT * FROM Trainer WHERE name = @Name";
                return await _db.QuerySingleOrDefaultAsync<Trainer>(sql, new { Name = name });
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<Trainer> Create(Trainer trainer)
        {
            try
            {
                var maxIdSql = @"SELECT COALESCE(MAX(id), 0) FROM Trainer";
                var maxId = await _db.QuerySingleAsync<int>(maxIdSql);
                trainer.id = maxId + 1;

                string a = "https://archives.bulbagarden.net/media/upload/";
                string f = trainer.image;
                string g = trainer.name;
                string link = "";

                Boolean imageFound = false;

                for (int i = 0; i < 9 && !imageFound; i++)
                {
                    for (int j = 0; j < 9 && !imageFound; j++)
                    {
                        link = $"{a}{i}/{i}{j}/{f}{g}.png";
                        if (ImageChecker.checkIfImageExists(link))
                        {
                            Console.WriteLine(link);
                            imageFound = true;
                        }
                    }
                }
                for (int i = 0; i < 9 && !imageFound; i++)
                {
                    for (char j = 'a'; j <= 'z' && !imageFound; j++)
                    {
                        link = $"{a}{i}/{i}{j}/{f}{g}.png";
                        if (ImageChecker.checkIfImageExists(link))
                        {
                            Console.WriteLine(link);
                            imageFound = true;
                        }
                    }
                }
                for (char i = 'a'; i <= 'z' && !imageFound; i++)
                {
                    for (int j = 0; j < 9 && !imageFound; j++)
                    {
                        link = $"{a}{i}/{i}{j}/{f}{g}.png";
                        if (ImageChecker.checkIfImageExists(link))
                        {
                            Console.WriteLine(link);
                            imageFound = true;
                        }
                    }
                }
                for (char i = 'a'; i <= 'z' && !imageFound; i++)
                {
                    for (char j = 'a'; j <= 'z' && !imageFound; j++)
                    {
                        for (int k = 0; k < f.Length; k++)
                        {
                            link = $"{a}{i}/{i}{j}/{f[k]}{g}.png";
                            if (ImageChecker.checkIfImageExists(link))
                            {
                                Console.WriteLine(link);
                                imageFound = true;
                            }
                        }
                    }
                }

                trainer.image = link;

                var sql = @"INSERT INTO Trainer (id, name, regionid, gymid, image, descrption) VALUES (@id, @name, @regionId, @gymId, @image, @descrption)";

                await _db.ExecuteAsync(sql, trainer);
                return trainer;
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<Trainer> Update(int id, Trainer trainer)
        {
            try
            {
                var sql = @"UPDATE Trainer SET name = @name, regionid = @regionId, gymid = @gymId, image = @image, description = @description WHERE id = @id";

                int updatedRows = await _db.ExecuteAsync(sql, trainer);
                if (updatedRows == 0)
                {
                    throw new Exception("Tidak ada trainer ditemukan dengan id " + id);
                }
                return trainer;
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<Trainer> Delete(int id)
        {
            try
            {
                var select = @"SELECT * FROM Trainer WHERE id = @Id";
                Trainer trainer = await _db.QuerySingleOrDefaultAsync<Trainer>(select, new { Id = id });

                if (trainer == null)
                {
                    throw new Exception("Trainer tidak ditemukan dengan id " + id);
                }
                var sql = @"DELETE FROM Trainer WHERE id = @Id";
                var deletedRows = await _db.QuerySingleOrDefaultAsync(sql, new { Id = id });

                return trainer;
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                throw new Exception(ex.Message);
            }
        }
    }
}
