using coin_api.Application.Service;
using coin_api.Domain.DTOs;
using coin_api.Domain.Model;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/category")]
public class CategoryController : ControllerBase
{
    private readonly CategoryService _categoryService;

    public CategoryController(CategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    [Route("listar")]
    public async Task<IActionResult> Listar()
    {
        try
        {
            var categories = await _categoryService.GetAllCategories();

            if (categories == null || !categories.Any())
            {
                return NotFound("Nenhuma categoria cadastrada");
            }
            return Ok(categories);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao buscar categorias: {ex.Message}");
        }
    }

    [HttpPost]
    [Route("registrar")]
    public async Task<IActionResult> Registrar(CategoryRegisterDTO registerDTO)
    {
        try
        {
            if (registerDTO == null)
            {
                return BadRequest("Dados da categoria inválidos.");
            }

            if (string.IsNullOrWhiteSpace(registerDTO.Category))
            {
                return BadRequest("A Categoria precisa de um título.");
            }

            var categoryModel = new CategoryModel
            {
                Category = registerDTO.Category,
                UserId = registerDTO.UserId
            };

            await _categoryService.CreateCategory(categoryModel);

            return Ok("Categoria registrada com sucesso.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao registrar categoria: {ex.Message}");
        }
    }

    [HttpPut]
    [Route("atualizar/{id}")]
    public async Task<IActionResult> Atualizar(int id, CategoryRegisterDTO updateDTO)
    {
        try
        {
            var category = new CategoryModel
            {
                IdCategory = id,
                Category = updateDTO.Category,
                UserId = updateDTO.UserId
            };

            await _categoryService.UpdateCategory(category);

            return Ok("Categoria atualizada com sucesso.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao atualizar categoria: {ex.Message}");
        }
    }

    [HttpDelete]
    [Route("deletar/{id}")]
    public async Task<IActionResult> Deletar(int id)
    {
        try
        {
            await _categoryService.DeleteCategory(id);

            return Ok("Categoria deletada com sucesso.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao deletar categoria: {ex.Message}");
        }
    }
}
