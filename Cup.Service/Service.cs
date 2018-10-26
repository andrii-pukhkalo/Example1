using System;
using System.Collections.Generic;
using System.Fabric;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Data.Collections;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using Cup.Service.Interfaces;
using Cup.Domain.Core.Interfaces;

namespace Cup.Service
{
    /// <summary>
    /// An instance of this class is created for each service replica by the Service Fabric runtime.
    /// </summary>
    internal sealed class Service : StatefulService, ICupService
    {
        private ICupRepository _cupRepository;
        private IClubRepository _clubRepository;

        public Service(StatefulServiceContext context,
            ICupRepository cupRepository, IClubRepository clubRepository)
            : base(context)
        {
            _cupRepository = cupRepository;
            _clubRepository = clubRepository;
        }

        public async Task<List<Domain.Core.AggregatesModel.CupAggregate.Cup>> GetActiveCups()
        {
            return await _cupRepository.GetAllActiveAsync();
        }

        protected override async Task RunAsync(CancellationToken cancellationToken)
        {
            List<Domain.Core.AggregatesModel.CupAggregate.Cup> cups = await GetActiveCups();
        }
    }
}
