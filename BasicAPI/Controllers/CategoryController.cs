using BasicAPI.Interfaces;
using BasicAPI.Model.Entities;
using BasicAPI.Model.Request;
using BasicAPI.Model.Response;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace BasicAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoryController(ICategoryRepository repository) : ControllerBase
    {
        private readonly ICategoryRepository _repository = repository;

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_repository.GetAll());
            }
            catch (Exception e)
            {
                Log.Error(e.Message);

                return StatusCode(500, $"An unexpected error occurred: {e}");
            }
        }

        [HttpGet("{id}")]
        public ActionResult GetById(Guid id)
        {
            try
            {
                Category? category = _repository.GetById(id);

                if (category is null)
                {
                    return NotFound("La Categoria no fue encontrada");
                }

                CategoryResponse response = new()
                {
                    Id = category.Id,
                    Name = category.Name,
                    Description = category.Description,
                    Products = category.Products,
                };

                return Ok(response);
            }
            catch (Exception e)
            {
                Log.Error(e.Message);

                return StatusCode(500, $"An unexpected error occurred: {e}");
            }
        }

        [HttpPost]
        public IActionResult Create(CreateCategoryRequest req)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            Category category = new()
            {
                Name = req.Name,
                Description = req.Description,
            };

            try
            {
                bool result = _repository.Create(category);

                if (!result)
                {
                    return BadRequest("Ocurrio un problema al crear la Categoria");
                }

                return CreatedAtAction(nameof(GetById), new { Id = category.Id }, category);
            }
            catch (Exception e)
            {
                Log.Error(e.Message);

                return StatusCode(500, $"An unexpected error occurred: {e}");
            }
        }
    }
}
