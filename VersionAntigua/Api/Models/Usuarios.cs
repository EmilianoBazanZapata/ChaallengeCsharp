using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Api
{
    public class Usuario
    {
        public int Id{get;set;}
        public string Email{get;set;}
        public string Password{get;set;}
    }
}