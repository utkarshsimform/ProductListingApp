using ProtoType.Model.DatabaseEntity;
using Microsoft.EntityFrameworkCore;

namespace ProtoType.Model
{
    public class IProtoTypeContext : DbContext
    {
        public IProtoTypeContext(DbContextOptions<IProtoTypeContext> options) : base(options)
        {

        }

        public DbSet<BrandAndProduct> BrandAndProducts { get; set; }
    }
}
