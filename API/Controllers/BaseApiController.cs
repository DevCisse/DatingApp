﻿using API.Helpers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;

namespace API.Controllers
{

    [ServiceFilter(typeof(LogUserActivity))]
    [Route("api/[controller]")]
    [ApiController]
    public class BaseApiController  : ControllerBase
    {


        
    }
}
