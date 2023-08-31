
using CodeChallenge.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeChallenge.Controllers;


[ApiController]
[Route("[controller]")]
[Authorize]
public class ProductsController : ControllerBase
{

    private UnitOfWork _unitOfWork;
    private readonly ILogger<ProductsController> _logger;

    public ProductsController(UnitOfWork unitOfWork, ILogger<ProductsController> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    [HttpGet(Name = "GetProduct")]
    public ActionResult GetProduct(int id)
    {
        try
        {
            var product = _unitOfWork.ProductRepository.GetById(id);

            if (product == null)
            {
                return NotFound(); 
            }

            return Ok(product); 
        }
        catch (Exception ex)
        {
            _logger.Log(LogLevel.Error, ex.Message);
            return NoContent();

        }
    }
}

