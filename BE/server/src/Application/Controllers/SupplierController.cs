using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using NShop.src.Application.DTOs.Product;
using NShop.src.Application.DTOs.Supplier;
using NShop.src.Application.Service;

namespace NShop.src.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly ILogger<SupplierController> _logger;
        private readonly ISupplierService supplierService;

        public SupplierController(ILogger<SupplierController> logger, ISupplierService service)
        {

            _logger = logger;
            supplierService = service;

        }




        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            _logger.LogInformation("Get user by id request received");

            return await supplierService.HandleGetById(id);
        }


        [HttpPost("")]
        public async Task<IActionResult> GetCreate([FromBody] SupplierCreateDto dto)
        {
            _logger.LogInformation("Create supplier request received");

            return await supplierService.HandleCreate(dto);
        }

        [HttpDelete("")]

        public async Task<IActionResult> GetDelete(Guid id)
        {

            _logger.LogInformation("deleted ");
            return await supplierService.HandleDelete(id);


        }

        [HttpPut("")]

        public async Task<IActionResult> GetUpdae(Guid id, [FromBody] SupplierUpdateDto dto)
        {

            _logger.LogInformation("Update supplier request received");
            return await supplierService.HandleUpdate(id, dto);






        }
    }
}
