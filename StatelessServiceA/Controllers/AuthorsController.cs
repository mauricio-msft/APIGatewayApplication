using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StatelessServiceA.Model;

namespace StatelessServiceA.Controllers
{
    [ApiController]
    [Route("api/authors")]
    public class AuthorsController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<Author>> Get()
        {
            return new List<Author>() {
                new Author() { Id = 1, Name = "Marcel Proust"},
                new Author() { Id = 2, Name = "James Joyce" }
            };
        }
    }
}
