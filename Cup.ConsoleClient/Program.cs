using Cup.Service.Interfaces;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cup.ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            ICupService cupService = ServiceProxy.Create<ICupService>(new Uri("fabric:/CricketManager/Cup.Service"));

            Task<List<Cup.Domain.Core.AggregatesModel.CupAggregate.Cup>> task = cupService.GetActiveCups();

            Console.Write(task.Result);
            Console.ReadLine();
        }
    }
}
