using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace zpnet.Models
{
    [Table("elementy")]
    public class element
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string? nazwa { get; set; }


      
        public string? opis { get; set; }
  
        [DataType(DataType.Date)]
        [Display(Name = "data stworzenia")]
        public DateTime? Data_stworzenia { get; set; }

        public int? miejsceId { get; set; }
        [ForeignKey("miejsceId")]
        public miejsce? miejsce { get; set; }

        public ICollection<kategoria>? kategorie { get; set; }

        public string getMax(string wartosc, int dlugosc = 20)
        {
            if (wartosc.Length > 20)
            {
                return wartosc.Substring(0, dlugosc) + "...";
            }
            else
            {
                return wartosc;
            }
        }
    }
}
