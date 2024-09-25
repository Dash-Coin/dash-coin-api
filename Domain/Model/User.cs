using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace coin_api.Domain.Model
{
    [Table("users")]
    public class User
    {
        [Key]
        public int id { get; set; }
        public string email { get; set; } = string.Empty;
        public byte[] senhaHash { get; set; }
        public byte[] senha { get; set; }
        public string? token { get; set; }
        // public string? reseteSenha { get; set; }   
        [Column(TypeName = "date")]
        public DateTime expiraToken { get; set; }

        // Relacionamento um para muitos com TransactionModel
        public ICollection<TransactionModel>? Transactions { get; set; } = new List<TransactionModel>();

        // Relacionamento um para muitos com CategoryModel
        public ICollection<CategoryModel>? Categories { get; set; } = new List<CategoryModel>();

    }
}