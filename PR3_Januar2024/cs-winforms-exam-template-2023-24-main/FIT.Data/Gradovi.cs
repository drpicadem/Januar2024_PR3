using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIT.Data
{
    [Table("Gradovi")]
    public class Gradovi
    {
        public int Id { get; set; }
        public  string Naziv { get; set; }
        public bool Status { get; set; }
        public Drzave Drzave { get; set; }
        public int DrzaveId { get; set; }

        public override string ToString()
        {
            return Naziv; 
        }

    }
}
