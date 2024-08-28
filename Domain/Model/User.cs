using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace coin_api.Domain.Model
{
    [Table("users")]
    public class User
    {
        [Key]
        public int id { get; init; }
        public string email { get; private set; }
        public string password { get; private set; }
    }
}