using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace FIT.Data
{
    public class Student
    {
        public int Id { get; set; }
        public string Indeks { get; set; }
        public string Lozinka { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Email { get; set; }
        public byte[] Slika { get; set; }
        public DateTime DatumRodjenja { get; set; }
        public bool Aktivan { get; set; }        
        public int SemestarId { get; set; }
        public Gradovi Gradovi { get; set; }
        public int GradoviID { get; set; }
        public string NazivGrada => Gradovi.Naziv.ToString();
        public string NazivDrzave => Gradovi.Drzave.Naziv.ToString();
        [NotMapped]
        public float Prosek { get;set; }
        public override string ToString()
        {
            return $"{Indeks} {Ime} {Prezime}";
        }
    }
}
