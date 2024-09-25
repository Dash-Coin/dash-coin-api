
public class TransactionRepository
{
    // private readonly ConnectionContext _context = new ConnectionContext();

    private readonly ConnectionContext _context;
    public TransactionRepository(ConnectionContext context)
    {
        _context = context;
    }

    public void Add(TransactionModel transaction)
    {
        _context.Transactions.Add(transaction);
        _context.SaveChanges();
    }

    public List<TransactionModel> Get()
    {
        return _context.Transactions.ToList();
    }
}
