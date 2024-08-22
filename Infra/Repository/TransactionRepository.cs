
public class TransactionRepository
{
    private readonly ConnectionContext _context = new ConnectionContext();

    public void Add(Transaction transaction)
    {
        _context.Transactions.Add(transaction);
        _context.SaveChanges();
    }

    public List<Transaction> Get()
    {
        return _context.Transactions.ToList();
    }
}
