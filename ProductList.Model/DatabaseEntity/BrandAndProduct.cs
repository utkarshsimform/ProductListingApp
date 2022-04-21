using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProtoType.Model.DatabaseEntity
{
    [Table("BrandAndProduct")]
    public class BrandAndProduct
    {
        [Key]
        public int Id { get; set; }
        //[Required]
        public int BrandId { get; set; }
        public string BrandName { get; set; }        
        public int? CredentialId { get; set; }                
        public string ProductName { get; set; }
        public string ArrReference { get; set; }
    }
}
