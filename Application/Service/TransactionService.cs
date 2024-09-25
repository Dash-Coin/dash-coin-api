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

    }
}