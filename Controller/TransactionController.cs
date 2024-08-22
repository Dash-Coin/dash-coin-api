using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/transaction")]
public class TransactionController : ControllerBase
{
    private readonly TransactionRepository _transactionRepository;

    public TransactionController(TransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository ?? throw new ArgumentNullException(nameof(transactionRepository));
    }

    [HttpGet]
    [Route("all")]
    public IActionResult Get()
    {
        var transactions = _transactionRepository.Get();
        return Ok(transactions);
    }
}