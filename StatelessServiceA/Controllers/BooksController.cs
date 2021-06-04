using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StatelessServiceA.Model;

namespace StatelessServiceA.Controllers
{
    [ApiController]
    [Route("api/books")]
    public class BooksController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<Book>> Get()
        {
            return new List<Book>() {
                new Book() { Id = 1, Name = "In Search of Lost Time"},
                new Book() { Id = 2, Name = "Ulysses" }
            };
        }
    }
}
