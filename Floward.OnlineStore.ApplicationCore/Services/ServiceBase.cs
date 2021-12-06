using AutoMapper;
using Floward.OnlineStore.Core.UnitOfWork;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace Floward.OnlineStore.ApplicationCore.Services
{
    public abstract class ServiceBase
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;
        protected readonly ILogger _logger;
        protected readonly IConfiguration _configuration;

        public ServiceBase(IUnitOfWork unitOfWork, IMapper mapper, ILogger logger, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _configuration = configuration;
        }
    }
}
