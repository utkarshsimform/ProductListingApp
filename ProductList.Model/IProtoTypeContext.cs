using ProtoType.Model.DatabaseEntity;
using Microsoft.EntityFrameworkCore;

namespace ProtoType.Model
{
    public class IProtoTypeContext : DbContext
    {
        public IProtoTypeContext(DbContextOptions<IProtoTypeContext> options) : base(options)
        {

        }

        //public DbSet<Credential> Credentials { get; set; }
        public DbSet<BrandAndProduct> BrandAndProducts { get; set; }
        //public DbSet<BrandAndExample> BrandAndExamples { get; set; }
        //public DbSet<HoldingTypeMapping> HoldingTypeMappings { get; set; }
        //public DbSet<DatabaseEntity.ProtoType> ProtoTypes { get; set; }
        //public DbSet<Job> Jobs { get; set; }
    }
}
