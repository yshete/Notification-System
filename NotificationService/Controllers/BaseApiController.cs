using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace NotificationService.Controllers
{
    public class BaseApiController : ControllerBase
    {
        protected readonly ILogger<BaseApiController> logger;
        public BaseApiController(ILogger<BaseApiController> logger)
        {
            this.logger = logger;
        }

    }
}