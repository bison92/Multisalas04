using System;
namespace WebCine.Controllers
{
    interface IValuesController
    {
        void Delete(int id);
        System.Collections.Generic.IEnumerable<string> Get();
        string Get(int id);
        void Post(string value);
        void Put(int id, string value);
    }
}
