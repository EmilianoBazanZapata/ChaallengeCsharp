using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Data;
using Microsoft.AspNetCore.Mvc;
namespace Api.Controllers
{
    public class PeliculaController
    {
        private readonly DisneyContext _db;
        public PeliculaController(DisneyContext db)
        {
            _db = db;
        }
    }
}