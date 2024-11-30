using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    public class Image
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UrlImage { get; set; }
    }
}
