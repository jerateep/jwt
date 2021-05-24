using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using testrm.Models;

namespace testrm.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly Data.DB_BackOfficeContext _ent;
        public HomeController(Data.DB_BackOfficeContext ent)
        {
            _ent = ent;
        }
        [HttpGet]
        public LinkDate GetStr()
        {
            LinkDate Data = new LinkDate();
            Data.found = 3;
            Data.Link = new List<Link>{
                new Link {name = "google", _Links = "google.com"},
                new Link {name = "gmail", _Links = "gmail.com"},
                new Link {name = "hotmail", _Links = "hotmail.com"},
            };
            return Data;
        }

        [Authorize]
        [HttpGet]
        public List<Product> Getproduct()
        {
            var data = _ent.Product.ToList();
            return data;
        }
    }

    public class LinkDate
    {
        public int found { get; set; }
        public List<Link> Link { get; set; } 
    }

    public class Link
    {
        public string name { get; set; }
        public string _Links { get; set; }
    }
}