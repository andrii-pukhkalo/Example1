using System;
using System.Collections.Generic;
using System.Fabric;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using CupService.Interfaces;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using Microsoft.ServiceFabric.Services.Client;

namespace StatelessClientService
{
    /// <summary>
    /// An instance of this class is created for each service instance by the Service Fabric runtime.
    /// </summary>
    internal sealed class StatelessClientService : StatelessService
    {
        public StatelessClientService(StatelessServiceContext context)
            : base(context)
        { }

        /// <summary>
        /// Optional override to create listeners (e.g., TCP, HTTP) for this service replica to handle client or user requests.
        /// </summary>
        /// <returns>A collection of listeners.</returns>
        protected override IEnumerable<ServiceInstanceListener> CreateServiceInstanceListeners()
        {
            return new ServiceInstanceListener[0];
        }

        /// <summary>
        /// This is the main entry point for your service instance.
        /// </summary>
        /// <param name="cancellationToken">Canceled when Service Fabric needs to shut down this service instance.</param>
        protected override async Task RunAsync(CancellationToken cancellationToken)
        {

            ICupService service = ServiceProxy
                .Create<ICupService>(new Uri("fabric:/CricketManager/CupService"),
                                        new ServicePartitionKey(1)); //, new ServicePartitionKey(1)

            try
            {
                List<Cup.Domain.Core.AggregatesModel.CupAggregate.Cup> cups = await service.GetActiveCups();
                var a = 1; 
            }
            catch (Exception e)
            {

            }


            // TODO: Replace the following sample code with your own logic 
            //       or remove this RunAsync override if it's not needed in your service.

            //long iterations = 0;

            //while (true)
            //{
            //    cancellationToken.ThrowIfCancellationRequested();

            //    ServiceEventSource.Current.ServiceMessage(this.Context, "Working-{0}", ++iterations);

            //    await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);
            //}
        }
    }
}
