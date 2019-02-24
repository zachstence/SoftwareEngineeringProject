using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//This is a test
namespace ClassLibrary1
{
    public class Book
    {
       public int Id { get; set; }

       [Required]
       [StringLength(255)]
       public string Title { get; set; }

       public Pieces Category { get; set; }

    }
}
