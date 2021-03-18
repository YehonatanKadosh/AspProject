using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AspProject_Entities.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Required field")]
        [Column(TypeName = "varchar(50)")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Required field")]
        [Column(TypeName = "varchar(50)")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Required field")]
        [Column(TypeName = "smalldatetime")]
        public DateTime BirthDate { get; set; }
        [Required(ErrorMessage = "Required field")]
        [Column(TypeName = "varchar(50)")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Required field")]
        [Column(TypeName = "varchar(50)")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Required field")]
        [Column(TypeName = "varchar(50)")]
        public string Password { get; set; }
    }
}
