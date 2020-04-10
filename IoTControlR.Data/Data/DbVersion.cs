using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IoTControlR.Data
{
    [Table("DbVersion")]
    public class DbVersion
    {
        [Key]
        public string Version { get; set; }
    }
}
