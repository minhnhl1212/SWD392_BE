namespace NShop.src.Application.Controllers;
using Microsoft.AspNetCore.Mvc;
using NShop.src.Application.DTOs.Product;
using NShop.src.Application.Service;


[ApiController]
[Route("/api/v1/products")]

public class ProductController : ControllerBase
{

    private readonly ILogger<ProductController> _logger;
    private readonly IProductService _service;


    public ProductController(ILogger<ProductController> logger, IProductService service)
    {
        _logger = logger;
        _service = service;
    }


    [HttpGet("")]
    public async Task<IActionResult> Get([FromQuery] ProductQuery query)
    {
        _logger.LogInformation("Get all Product request received");

        return await _service.HandleGetAllAsync(query);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByID(Guid id)
    {
        _logger.LogInformation("get Product by id Request received");

        return await _service.HandleGetByIdAsync(id);
    }


    [HttpPost("")]
    public async Task<IActionResult> GetCreate([FromBody] ProductCreateDto dto)
    {
        _logger.LogInformation("Create product request received");

        return await _service.HandleCreateAsync(dto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] ProductUpdateDto dto)
    {
        _logger.LogInformation("request received");

        // Chuyển đổi từ ProductUpdateDto sang ProductCreateDto (nếu cần)
 
        return await _service.HandleUpdateAsync(id, dto);


    }

    [HttpDelete("")]
    public async Task<IActionResult> Delete(Guid id)
    {
        _logger.LogInformation("Delete recieved");
        return await _service.HandleDeleteAsync(id);
    }

}

