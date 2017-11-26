using LibApp.Services;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LibApp.Controllers
{
    public class HomeController : ApiController
    {
        //Container container = new Container(x => x.For<ILibraryService>().Use<LibraryService>());
        //var instance = container.GetInstance<LibraryService>();

        private readonly ILibraryService _libraryService;
        public HomeController(ILibraryService libraryService)
        {
            _libraryService = libraryService;
        }

        //public HomeController() : this(new LibraryService())
        //{
        //}



        public IEnumerable<string> Get()
        {
            //return new string[] { "value1", "value2" };
            return new[] { _libraryService.GetById(1) };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
