using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace ProtoType.Model.Models
{
    [Table("BrandAndProduct")]
    public class BrandAndProduct
    {
        public int Id { get; set; }        
        public int BrandId { get; set; }        
        public string BrandName { get; set; }
        public int? CredentialId { get; set; }
        public string CredentialName { get; set; }        
        public string ProductName { get; set; }
        public string ArrReference { get; set; }        
    }
}