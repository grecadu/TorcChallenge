
using CodeChallenge.DTOs;
using CodeChallenge.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace CodeChallenge.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize] 

    public class OrdersController : ControllerBase
    {
        private UnitOfWork _unitOfWork;
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(UnitOfWork unitOfWork, ILogger<OrdersController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        [HttpPost]
        public ActionResult PostOrder(OrderDto orderDto)
        {
            try
            {
                _unitOfWork.CreateOrderWithTotalCost(orderDto.CustomerId, orderDto.ProductId, orderDto.Quantity);
                _unitOfWork.Save();
                _logger.Log(LogLevel.Information, "order created");
                return Ok(); 
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error,ex.Message);
                return NoContent();
            }
        }

        [HttpGet(Name = "GetOrder")]
        public ActionResult GetOrder(int id)
        {
            try
            {
                var order = _unitOfWork.OrderRepository.GetById(id);

                if (order == null)
                {
                    return NotFound(); 
                }

                return Ok(order);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                return NoContent();
            }
        }
    }
}
