﻿using Cine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebCine.Controllers
{
    //[Authorize]
    public class ValuesController : ApiController, WebCine.Controllers.IValuesController
    {

        public ValueService ValueService {get; set;}

        public ValuesController(ValueService valueService)
        {
            this.ValueService = valueService;
        }


        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return ValueService.Read(id);
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
