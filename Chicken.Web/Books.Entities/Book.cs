using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Books.Entities
{
    public class Book
    {
        public int Id { get; set; }

        //[Required]
        //[StringLength(255)]

        public string Title { get; set; }

        public Genre Category { get; set; }

        public string Breast { get; set; }

        public string Wing { get; set; }



    }
}

