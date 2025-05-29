using MediatR;
using Microsoft.AspNetCore.Mvc;
using StargateAPI.Business.Commands;
using StargateAPI.Business.Queries;
using StargateAPI.Shared;

namespace StargateAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AstronautDutiesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AstronautDutiesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetAstronautDutiesByName(string name)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name))
                {
                    throw new ArgumentNullException(nameof(name));
                }

                var result = await _mediator.Send(new GetAstronautDutiesByName()
                {
                    Name = name
                });

                return this.GetResponse(result);
            }
            catch (Exception ex)
            {
                return this.GetResponse(new BaseResponse()
                {
                    Message = ex.Message,
                    Success = false,
                    ResponseCode = (int)ex.GetResponseCode()
                });
            }            
        }

        [HttpPost("")]
        public async Task<IActionResult> CreateAstronautDuty([FromBody] CreateAstronautDuty request)
        {
                var result = await _mediator.Send(request);
                return this.GetResponse(result);           
        }
    }
}