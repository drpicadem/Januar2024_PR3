﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIT.Data
{
    [Table("Drzave")]
    public class Drzave
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public bool Status { get; set; }
        public byte[] Zastava { get; set; }
        [NotMapped]
        public int BrojGradova { get; set; }
        public override string ToString()
        {
            return Naziv;
        }
    }
}
