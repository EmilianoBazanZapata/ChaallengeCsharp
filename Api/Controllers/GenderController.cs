using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Resultado;

namespace Api.Controllers
{
    public class GenderController:ControllerBase
    {
        private readonly IConfiguration _configuration;

        public GenderController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
    }
}