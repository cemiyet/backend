using System;
using Microsoft.AspNetCore.Mvc;

namespace Cemiyet.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        public GenresController() { }

        // POST {{url}}/genres
        [HttpPost]
        public IActionResult Add()
        {
            throw new NotImplementedException("TODO");
        }

        // GET {{url}}/genres?page=<page>
        [HttpGet]
        public IActionResult List([FromQuery] int page = 1)
        {
            throw new NotImplementedException("TODO");
        }

        // GET {{url}}/genres/<id>
        [HttpGet("{id}")]
        public IActionResult Details(Guid id)
        {
            throw new NotImplementedException("TODO");
        }

        // {{url}}/genres/<id>/books?page=<page>
        [HttpGet("{id}/books")]
        public IActionResult ListBooks(Guid id, [FromQuery] int page = 1)
        {
            throw new NotImplementedException("TODO");
        }

        // PUT {{url}}/genres/<id>
        [HttpPut("{id}")]
        public IActionResult Update(Guid id)
        {
            throw new NotImplementedException("TODO");
        }

        // DELETE {{url}}/genres/<id>
        [HttpDelete("{id}")]
        public IActionResult DeleteOne(Guid id)
        {
            throw new NotImplementedException("TODO");
        }

        // DELETE {{url}}/genres?id=<id_1>&id=<id_n>
        [HttpDelete]
        public IActionResult DeleteMany([FromQuery] Guid[] id)
        {
            throw new NotImplementedException("TODO");
        }
    }
}
