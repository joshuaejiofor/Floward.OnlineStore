using AutoMapper;
using Floward.OnlineStore.Core.Helpers.Interfaces;
using Floward.OnlineStore.Core.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace Floward.OnlineStore.WebApi.Controllers.Base
{
    public abstract class ApiControllerBase : ControllerBase
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;
        protected readonly ILogger _logger;
        protected readonly IConfiguration _configuration;
        protected readonly IUriHelper _uriHelper;


        public ApiControllerBase(IUnitOfWork unitOfWork, IMapper mapper, ILogger logger, IConfiguration configuration, IUriHelper uriHelper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _configuration = configuration;
            _uriHelper = uriHelper;
        }
    }
}
