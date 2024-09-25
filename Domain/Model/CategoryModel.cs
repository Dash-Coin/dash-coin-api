using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace coin_api.Domain.Model
{
    [Table("category")]
    public class CategoryModel
    {
        [Key]
        public int IdCategory { get; set; } // A chave primária é um int

        public string? Category { get; set; } // O nome da categoria

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; } = null!;

        // Propriedade de navegação para TransactionModel
        public ICollection<TransactionModel> Transactions { get; set; } = new List<TransactionModel>();

    }
}
