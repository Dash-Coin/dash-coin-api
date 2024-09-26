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

    [HttpGet]
    [Route("despesas")]
    public async Task<IActionResult> ListarDespesas()
    {
        try
        {
            var despesas = await _transactionService.GetTransactionsForType();

            if (despesas == null || !despesas.Any())
            {
                return NotFound("Nenhuma despesa encontrada");
            }

            var totalDespesas = despesas.Sum(d => d.Value);

            return Ok(new
            {
                TotalDespesas = totalDespesas,
                Transacoes = despesas
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao buscar despesas: {ex.Message}");
        }
    }

    [HttpGet]
    [Route("receitas")]
    public async Task<IActionResult> ListarReceitas()
    {
        try
        {
            var receita = await _transactionService.GetTransactionsForType();

            if (receita == null || !receita.Any())
            {
                return NotFound("Nenhuma receita encontrada");
            }

            var totalReceitas = receita.Sum(d => d.Value);

            return Ok(new
            {
                TotalReceitas = totalReceitas,
                Transacoes = receita
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao buscar receitas: {ex.Message}");
        }
    }

    [HttpGet]
    [Route("receitas-mensais")]
    public async Task<IActionResult> ListarReceitasMensais()
    {
        try
        {
            var receitasPorMes = await _transactionService.GetMonthlyIncomes();

            if (receitasPorMes == null || !receitasPorMes.Any())
            {
                return NotFound("Nenhuma receita encontrada");
            }

            return Ok(receitasPorMes);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao buscar receitas: {ex.Message}");
        }
    }

    [HttpGet]
    [Route("despesas-paginas")]
    public async Task<IActionResult> ListarDespesasPaginadas([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        try
        {
            var paginatedDespesas = await _transactionService.GetPaginatedForType(page, pageSize);

            if (!paginatedDespesas.Any())
            {
                return NotFound("Nenhuma despesa encontrada");
            }

            return Ok(paginatedDespesas);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao buscar despesas: {ex.Message}");
        }
    }

    [HttpGet]
    [Route("receitas-paginadas")]
    public async Task<IActionResult> ListarReceitasPaginadas([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        try
        {
            var paginatedReceitas = await _transactionService.GetPaginatedForType(page, pageSize);

            if (!paginatedReceitas.Any())
            {
                return NotFound("Nenhuma receita encontrada");
            }

            return Ok(paginatedReceitas);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao buscar receitas: {ex.Message}");
        }
    }

    [HttpGet]
    [Route("usuario/{userId}/despesas")]
    public async Task<IActionResult> ListarDespesasPorUsuario(int userId)
    {
        try
        {
            var despesas = await _transactionService.GetTransactionsForType(userId, false); // 0 para despesas

            if (!despesas.Any())
            {
                return NotFound("Nenhuma despesa encontrada para este usuário.");
            }

            return Ok(despesas);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao buscar despesas do usuário: {ex.Message}");
        }
    }

    [HttpGet]
    [Route("usuario/{userId}/receitas")]
    public async Task<IActionResult> ListarReceitasPorUsuario(int userId)
    {
        try
        {
            var receitas = await _transactionService.GetTransactionsForType(userId, true); // 1 para receitas

            if (!receitas.Any())
            {
                return NotFound("Nenhuma receita encontrada para este usuário.");
            }

            return Ok(receitas);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao buscar receitas do usuário: {ex.Message}");
        }
    }






}
