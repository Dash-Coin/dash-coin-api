using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace coin_api.Domain.Model
{
    [Table("users")]
    public class User
    {
        public int id { get; set; }
        public string email { get; set; } = string.Empty;
        public byte[] senhaHash { get; set; }
        public byte[] senha { get; set; }
        public string? verificaToken { get; set; }
        public string? reseteSenha { get; set; }   
        public DateTime? expiraToken { get; set; }  
    }
}