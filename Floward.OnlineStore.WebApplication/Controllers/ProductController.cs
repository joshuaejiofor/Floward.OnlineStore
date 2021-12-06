using AutoMapper.Configuration;
using Floward.OnlineStore.Core.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Floward.OnlineStore.WebApplication.Controllers
{
    public class ProductController : ControllerShared
    {
        public ProductController(IUnitOfWork unitOfWork, ILogger logger, IConfiguration configuration)
            : base(unitOfWork, logger, configuration) { }

        public IActionResult Index()
        {
            return View();
        }
    }
}
