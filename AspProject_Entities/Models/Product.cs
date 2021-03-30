using AspProject_Entities.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspProject_Entities.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Required field")]
        [Column(TypeName = "varchar(50)")] 
        public string Title { get; set; }
        [Required(ErrorMessage = "Required field")]
        [Column(TypeName = "varchar(500)")]
        public string ShortDescription { get; set; }
        [Required(ErrorMessage = "Required field")]
        [Column(TypeName = "varchar(4000)")]
        public string LongDescription { get; set; }
        [Required(ErrorMessage = "Required field")]
        [Column(TypeName = "smalldatetime")]
        public DateTime Date { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime LastModified { get; set; }// For the nonUsers to know if i should change the state 15 minutes from being touched

        [Required(ErrorMessage = "Required field")]
        [Column(TypeName = "decimal(18,0)")]
        public double Price { get; set; }
        [Column(TypeName = "image")]
        public byte[] Picture1 { get; set; }
        [Column(TypeName = "image")]
        public byte[] Picture2 { get; set; }
        [Column(TypeName = "image")]
        public byte[] Picture3 { get; set; }
        [Required(ErrorMessage = "Required field")]
        public ProductState State { get; set; }
        public User Seler { get; set; }
        public User Buyer { get; set; }
    }
}
