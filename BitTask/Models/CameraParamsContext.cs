using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace BitTask.Models
{
    public class CameraParamsContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionStringBuilder = 
                new SqliteConnectionStringBuilder { DataSource = "CameraParams.db" };
            var connectionString = connectionStringBuilder.ToString();
            var connection = new SqliteConnection(connectionString);

            optionsBuilder.UseSqlite(connection);
        }

        public DbSet<CameraParams> CameraParamsList { get; set; }
    }
}