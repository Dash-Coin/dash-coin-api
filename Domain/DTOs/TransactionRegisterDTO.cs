
namespace coin_api.Domain.DTOs
{
    public class TransactionRegisterDTO
    {
        public string? Description { get; set; } // Descrição da transação

        public bool Type { get; set; } // Usando 'bool' em vez de 'Boolean'

        public DateTime Date { get; set; } // Data da transação

        public double Value { get; set; } // Valor da transação

        public int CategoryId { get; set; }

        public int UserId { get; set; }

    }
}