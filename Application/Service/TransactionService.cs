using Microsoft.EntityFrameworkCore;

namespace coin_api.Application.Service
{
    public class TransactionSerivce
    {
        private readonly ConnectionContext _context;

        public TransactionSerivce(ConnectionContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TransactionModel>> GetAllTransactions()
        {
            return await _context.Transactions.ToListAsync();
        }

        public async Task CreateTransactions(TransactionModel transactionModel)
        {
            _context.Transactions.Add(transactionModel);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteTransaction(int transactionId)
        {
            var transaction = await _context.Transactions.FindAsync(transactionId);
            if (transaction != null)
            {
                _context.Transactions.Remove(transaction);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateTransaction(TransactionModel transactionModel)
        {
            var transaction = await _context.Transactions.FindAsync(transactionModel.IdTransaction);
            if (transaction != null)
            {
                // Atualiza os campos conforme necessário
                transaction.Description = transactionModel.Description;
                transaction.Date = transactionModel.Date;
                transaction.Value = transactionModel.Value;

                _context.Transactions.Update(transaction);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<TransactionModel>> GetTransactionsForType()
        {
            return await _context.Transactions
                                 .Where(t => t.Type)
                                 .ToListAsync();
        }
        public async Task<IEnumerable<object>> GetMonthlyIncomes()
        {
            // Filtra as receitas (Type == true) e agrupa por mês e ano
            return await _context.Transactions
                                 .Where(t => t.Type == true)
                                 .GroupBy(t => new { t.Date.Year, t.Date.Month })
                                 .Select(g => new
                                 {
                                     Ano = g.Key.Year,
                                     Mes = g.Key.Month,
                                     TotalReceitas = g.Sum(t => t.Value),
                                     Transacoes = g.ToList()
                                 })
                                 .OrderBy(g => g.Ano).ThenBy(g => g.Mes) // Ordena por ano e mês
                                 .ToListAsync();
        }

        public async Task<IEnumerable<TransactionModel>> GetPaginatedForType(int userId, int page, int pageSize)
        {
            return await _context.Transactions
                                 .Where(t => t.UserId == userId && t.Type == false) // Filtra apenas despesas
                                 .OrderBy(t => t.Date) // Ordena por data ou qualquer outro campo
                                 .Skip((page - 1) * pageSize) // Pula as páginas anteriores
                                 .Take(pageSize) // Limita a quantidade de itens por página
                                 .ToListAsync();
                                //  .Where(t => t.userId == userId && t => t.Type) // Filtra apenas despesas
        }

        public async Task<IEnumerable<TransactionModel>> GetTransactionsForType(int userId, bool type)
        {
            return await _context.Transactions
                                 .Where(t => t.UserId == userId && t.Type == type)
                                 .ToListAsync();
        }





    }
}