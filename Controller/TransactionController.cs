using coin_api.Application.Service;
using coin_api.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/transaction")]
public class TransactionController : ControllerBase
{
    private readonly TransactionSerivce _transactionService;

    public TransactionController(TransactionSerivce transactionSerivce)
    {
        _transactionService = transactionSerivce;
    }

    [HttpGet]
    [Route("listar")]
    public async Task<IActionResult> Listar()
    {
        try
        {
            var transactions = await _transactionService.GetAllTransactions();

            if (transactions == null || !transactions.Any())
            {
                return NotFound("Nenhuma transação cadastrada");
            }
            return Ok(transactions);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao buscar transações: {ex.Message}");
        }
    }

    [HttpPost]
    [Route("registrar")]
    public async Task<IActionResult> Registrar(TransactionRegisterDTO registerDTO)
    {
        try
        {
            if (registerDTO == null)
            {
                return BadRequest("Dados da transação inválidos.");
            }

            var transactionModel = new TransactionModel
            {
                Description = registerDTO.Description,
                Date = registerDTO.Date,
                Value = registerDTO.Value,
                Type = registerDTO.Type,
                CategoryId = registerDTO.CategoryId,
                UserId = registerDTO.UserId
            };

            await _transactionService.CreateTransactions(transactionModel);

            return Ok("Transação registrada com sucesso.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao registrar transação: {ex.Message}");
        }
    }

    [HttpPut]
    [Route("atualizar/{id}")]
    public async Task<IActionResult> Atualizar(int id, TransactionRegisterDTO updateDTO)
    {
        try
        {
            var transaction = new TransactionModel
            {
                IdTransaction = id,
                Description = updateDTO.Description,
                Date = updateDTO.Date,
                Value = updateDTO.Value,
                Type = updateDTO.Type,
                CategoryId = updateDTO.CategoryId,
                UserId = updateDTO.UserId
            };

            await _transactionService.UpdateTransaction(transaction);

            return Ok("Transação atualizada com sucesso.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao atualizar transação: {ex.Message}");
        }
    }

    [HttpDelete]
    [Route("deletar/{id}")]
    public async Task<IActionResult> Deletar(int id)
    {
        try
        {
            await _transactionService.DeleteTransaction(id);

            return Ok("Transação deletada com sucesso.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao deletar transação: {ex.Message}");
        }
    }
}
