using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Data;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class GeneroController
    {
        private readonly DisneyContext _db;
        public GeneroController(DisneyContext db)
        {
            _db = db;
        }
    }
}