using Microsoft.EntityFrameworkCore;

namespace BitTask.Models
{
    public class CameraParamsContext : DbContext
    {
        public CameraParamsContext(DbContextOptions<CameraParamsContext> options)
            : base(options)
        {
        }

        public DbSet<CameraParams> CameraParamsList { get; set; }
    }
}