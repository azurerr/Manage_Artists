using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Assignment5.EntityModels
{
    [Table("Genre")]
    public class Genre
    {
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string Name { get; set; }
    }
}