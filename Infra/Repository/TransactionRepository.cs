
public class TransactionRepository
{
    // private readonly ConnectionContext _context = new ConnectionContext();

    private readonly ConnectionContext _context;
    public TransactionRepository(ConnectionContext context)
        {
            _context = context;
        }

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
