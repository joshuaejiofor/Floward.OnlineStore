using AutoMapper.Configuration;
using Floward.OnlineStore.Core.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Floward.OnlineStore.WebApplication.Controllers
{
    public class ControllerShared : Controller
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly ILogger _logger;
        protected readonly IConfiguration _configuration;
        public ControllerShared(IUnitOfWork unitOfWork, ILogger logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _unitOfWork = unitOfWork;
        }
    }
}
