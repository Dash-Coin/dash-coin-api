using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using coin_api.Domain.Model;

[Table("transaction")]
public class TransactionModel
{
    [Key]
    public int IdTransaction { get; set; } // Seguindo a convenção de nomes

    public string? Description { get; set; } // Descrição da transação

    public bool Type { get; set; } // Usando 'bool' em vez de 'Boolean'

    public DateTime Date { get; set; } // Data da transação

    public double Value { get; set; } // Valor da transação

    public int CategoryId { get; set; }
    [ForeignKey("CategoryId")]
    public CategoryModel Category { get; set; } = null!;
    public int UserId { get; set; }
    [ForeignKey("UserId")]
    public User User { get; set; } = null!;
}
