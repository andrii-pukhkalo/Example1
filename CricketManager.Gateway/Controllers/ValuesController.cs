using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using CupService.Interfaces;
using Microsoft.ServiceFabric.Services.Client;

namespace CricketManager.Gateway.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ValuesController : Controller
    {
        // GET api/values
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<Class> votes = new List<Class>();

            votes.Add(new Class(Guid.NewGuid()));
            votes.Add(new Class(Guid.NewGuid()));

            //var proxyLocation = new ServiceUriBuilder("MasterDataMService");
            //var masterDataService = ServiceProxy.Create<IMasterDataMService>(proxyLocation.ToUri());

            //var result = await masterDataService.GetMasterDataByName(interfaceName);

            ICupService service = ServiceProxy
                .Create<ICupService>(new Uri("fabric:/CricketManager/CupService"), 
                                        new ServicePartitionKey(1)); //, new ServicePartitionKey(1)

            try
            {
                List<Cup.Domain.Core.AggregatesModel.CupAggregate.Cup> cups = await service.GetActiveCups();
                return Json(cups);
            } catch (Exception e)
            {

            }


            return Json(votes);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
