using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FanFan.Models
{
    public class UserChange
    {
        public string Id { get; set; }
        public IFormFile PhotoUser { get; set; }
    }
}
