using AutoMapper;
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
    public class ProductController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _repository;

        public ProductController(IMapper mapper, IProductRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        [HttpGet] // http://localhost:7081/product/
        public IActionResult GetAll()
        {
            try
            {
                // Llamamos al metodo GetAll de nuestro repositorio que nos trae TODOS nuestros productos y los almacenamos en una coleccion
                IEnumerable<Product> products = _repository.GetAll();

                // Validamos que la coleccion contenga al menos 1 producto
                if (!products.Any())
                {
                    return NotFound("No se encontraron productos");
                }

                // Usamos AutoMapper para armar un objecto de respuesta custom con los datos de nuestros productos
                // IEnumerable<ProductResponse> response = _mapper.Map<IEnumerable<ProductResponse>>(products);

                // Creamos una lista de un objeto de respuesta custom para luego agregar uno a uno nuestros productos
                List<ProductResponse> response = new();

                // Iteramos nuestra coleccion de productos para agregar cada uno de sus elementos (producto) a nuestra nueva lista de respuesta custom
                foreach (var item in products)
                {
                    response.Add(new ProductResponse
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Price = item.Price,
                        Category = item.Category,
                    });
                }

                // Finalmente retornamos nuestro objeto custom que contiene nuestros productos
                return Ok(response);
            }
            catch (Exception e)
            {
                Log.Error(e.Message); // Si algo falla, esta linea dejaW un mensaje en consola del error ocurrido

                throw new Exception($"An unexpected error occurred: {e.Message}");
            }
        }

        [HttpGet("{id}")] // http://localhost:7081/product/{id}
        public IActionResult GetOne(Guid id)
        {
            // Creamos una instancia de nuestro objeto de respuesta custom
            ProductResponse response = new();

            try
            {
                // Llamamos al metodo GetById de nuestro repositorio que busca un producto coincida con el Id que le pasamos
                Product? product = _repository.GetById(id);

                // Verificamos que si el producto es NULL (significa que no se encontro en nuestra DB) y manejamos el error
                if (product == null)
                {
                    return NotFound($"El producto Id: {id} no fue encontrado");
                }

                // Igualamos cada una de las propiedades de nuestro objeto de respuesta con las de nuestro producto
                response.Id = product.Id;
                response.Name = product.Name;
                response.Price = product.Price;
                response.Category = product.Category;

                // Finalmente retornamos nuestro objeto custom con los valores de nuestro producto
                return Ok(response);
            }
            catch (Exception e)
            {
                Log.Error(e.Message); // Si algo falla, esta linea deja un mensaje en consola del error ocurrido

                throw new Exception($"An unexpected error occurred: {e.Message}");
            }
        }

        [HttpPost] // http://localhost:7081/product/
        public IActionResult Insert(CreateProductRequest req)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                // Creamos nuestro producto y cada una de las propiedades le damos el valor que fue enviado en la request
                Product product = new()
                {
                    Id = Guid.NewGuid(),
                    Name = req.Name,            // Otorgado por el usuario
                    Price = req.Price,          // Otorgado por el usuario
                    CreatedAt = DateTime.Now,
                    CategoryId = req.CategoryId // Id de la Categoria a la que pertenece el Producto
                };

                // Hacemos el llamado al metodo Create de nuestro repositorio y le pasamos nuestro objeto producto
                bool result = _repository.Create(product);

                // Verificamos el resultado, si es false significa que ocurrio un problema y debemos manejarlo
                if (!result)
                {
                    return BadRequest("Ocurrio un problema al crear el producto");
                }

                // Finalmente en este punto, si el estado de la operacion es el correcto podemos buscar y retornar nuestro producto
                return CreatedAtAction(nameof(GetOne), new { Id = product.Id }, product);
            }
            catch (Exception e)
            {
                Log.Error(e.Message); // Si algo falla, esta linea deja un mensaje en consola del error ocurrido

                throw new Exception($"An unexpected error occurred: {e.Message}");
            }
        }

        [HttpPut("{id}")] // http://localhost:7081/product/{id}
        public IActionResult Update(Guid id, [FromBody] UpdateProductRequest req)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Creamos una instancia de nuestro objeto de respuesta custom
            ProductResponse response = new();

            try
            {
                // Buscamos en nuestra DB un producto que coincida con el Id recibido y lo almacenamos en una variable
                Product? product = _repository.GetById(id);

                // Verificamos que si el producto es NULL (significa que no se encontro en nuestra DB) y manejamos el error
                if (product == null)
                {
                    return NotFound($"El producto con Id: {id} no fue encontrado");
                }

                // En este punto el producto fue encontrado y debemos actualizar sus propiedades con los nuevos valores
                product.Name = req.Name;
                product.Price = req.Price;

                // Hacemos el llamado al metodo Update de nuestro repositorio y le pasamos el producto con los nuevos valores
                bool result = _repository.Update(product);

                // Verificamos el resultado, si es false significa que ocurrio un problema y debemos manejarlo
                if (!result)
                {
                    return BadRequest("Ocurrio un problema al actualizar el producto");
                }

                // Igualamos cada una de las propiedades de nuestro objeto de respuesta con las de nuestro producto
                response.Id = product.Id;
                response.Name = product.Name;
                response.Price = product.Price;

                // Finalmente retornamos nuestro objeto custom con los valores de nuestro producto
                return Ok(response);
            }
            catch (Exception e)
            {
                Log.Error(e.Message); // Si algo falla, esta linea deja un mensaje en consola del error ocurrido

                throw new Exception($"An unexpected error occurred: {e.Message}");
            }
        }

        [HttpDelete("{id}")] // http://localhost:7081/product/{id}
        public IActionResult Delete(Guid id)
        {
            try
            {
                // Buscamos en nuestra DB un producto que coincida con el Id recibido y lo almacenamos en una variable
                Product? product = _repository.GetById(id);

                // Verificamos que si el producto es NULL (significa que no se encontro en nuestra DB) y manejamos el error
                if (product == null)
                {
                    return NotFound($"El producto con Id: {id} no fue encontrado");
                }

                // En este punto el producto fue encontrado y podemos eliminarlo llamando al metodo Delete de nuestro repositorio
                bool result = _repository.Delete(product);

                // Verificamos el resultado, si es false significa que ocurrio un problema y debemos manejarlo
                if (!result)
                {
                    return BadRequest("Ocurrio un problema al eliminar el producto");
                }

                // Finalmente retornamos true indicando el exito de la operacion
                return Ok(true);
            }
            catch (Exception e)
            {
                Log.Error(e.Message); // Si algo falla, esta linea deja un mensaje en consola del error ocurrido

                throw new Exception($"An unexpected error occurred: {e.Message}");
            }
        }
    }
}
