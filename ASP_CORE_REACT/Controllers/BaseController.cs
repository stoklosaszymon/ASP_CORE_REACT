using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASP_CORE_REACT.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASP_CORE_REACT.Controllers
{
    public class BaseController : Controller
    {
            public BaseController()
            {
                Database = new BloggingDBContext();
            }

            protected BloggingDBContext Database { get; set; }

            protected override void Dispose(bool disposing)
            {
                Database.Dispose();
                base.Dispose(disposing);
            }
        }
    }