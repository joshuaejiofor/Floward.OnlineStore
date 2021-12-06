using AutoMapper;
using Floward.OnlineStore.ApplicationCore.Dto;
using Floward.OnlineStore.Core.Exceptions;
using Floward.OnlineStore.Core.Helpers;
using Floward.OnlineStore.Core.Helpers.Interfaces;
using Floward.OnlineStore.Core.Requests.Filters;
using Floward.OnlineStore.Core.Responses;
using Floward.OnlineStore.Core.UnitOfWork;
using Floward.OnlineStore.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Serilog;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Floward.OnlineStore.WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/product")]
    [Produces("application/json")]    
    public class ProductApiController : ApiControllerBase
    {
        public ProductApiController(IUnitOfWork unitOfWork, IMapper mapper, ILogger logger, IConfiguration configuration,
                IUriHelper uriHelper)
            : base(unitOfWork, mapper, logger, configuration, uriHelper)
        {
        }
        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResponse<IEnumerable<ProductDto>>))]
        public IActionResult Get([FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);

            var result = _unitOfWork.ProductRepository.Pagenate(filter, out int totalRecords, c => true);

            var dtoList = _mapper.Map<List<ProductDto>>(result);

            var pagedReponse = PaginationHelper.CreatePagedReponse(dtoList, validFilter, totalRecords, _uriHelper, route);
            return Ok(pagedReponse);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, Type = typeof(Response<ProductDto>))]
        public async Task<IActionResult> Get(int Id)
        {
            var product = await _unitOfWork.ProductRepository.GetAsync(Id);
            var result = _mapper.Map<ProductDto>(product);

            return Ok(new Response<ProductDto>(result));
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<ProductDto>))]
        [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(Response<ProductDto>))]
        public async Task<IActionResult> Post([FromBody] ProductDto productDto)
        {
            var duplicates = await _unitOfWork.ProductRepository.FirstOrDefaultAsync(c => c.Name == productDto.Name && c.ProductType == c.ProductType);
            if (duplicates != null)
                return StatusCode(StatusCodes.Status409Conflict, new Response<ProductDto>(productDto, "Duplicate record", 409, false, 1));

            await _unitOfWork.ProductRepository.AddAsync(productDto);
            await _unitOfWork.CompleteAsync();

            return Ok(new Response<ProductDto>(productDto, 1));
        }

        
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<ProductDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response<ProductDto>))]
        public async Task<IActionResult> Put([FromBody] ProductDto productDto)
        {
            try
            {
                var existingProduct = await _unitOfWork.ProductRepository.FirstOrDefaultAsync(c => c.Id == productDto.Id && c.IsActive);

                if (existingProduct == null)
                    throw new NotFoundException($"No valid product found with Id {existingProduct.Id}");

                _unitOfWork.ProductRepository.Update(productDto);
                await _unitOfWork.CompleteAsync();

                return Ok(new Response<ProductDto>(productDto, 1));
            }
            catch (NotFoundException ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new Response<ProductDto>(productDto, ex.Message, 404, false, 1));
            }
        }
        
        [HttpDelete("{id}")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, Type = typeof(ProductDto))]
        public async Task<IActionResult> Delete(int Id)
        {
            var productToDelete = await _unitOfWork.ProductRepository.FirstOrDefaultAsync(c => c.Id == Id && c.IsActive);

            productToDelete.IsActive = false;
            await _unitOfWork.CompleteAsync();

            var productDto = _mapper.Map<ProductDto>(productToDelete);

            return Ok(productDto);
        }        
    }
}
