using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


[Table("transaction")]
public class Transaction
{
    [Key]
    public int idTransaction { get; set; }
    public string? Description { get; set; }

    public Boolean type { get; set; }

    public DateTime date { get; set; }

    public string? Category { get; set; }

    public double Value { get; set; }
}
