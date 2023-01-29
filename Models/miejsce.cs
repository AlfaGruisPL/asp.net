using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace zpnet.Models
{
    [Table("miejsca")]
    public class miejsce
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string  nazwa { get; set; }
        public ICollection<element>? elementy { get; set; }
    }

}
