using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Remoting;

using Cup.Domain.Core.AggregatesModel.CupAggregate;

namespace CupService.Interfaces
{
    public interface ICupService : IService

    {
        Task<List<Cup.Domain.Core.AggregatesModel.CupAggregate.Cup>> GetActiveCups();
        Task JoinCup(Guid cupId, Guid clubId);

        Task<Leaderboard> GetLeaderboard(Guid cupId);
        Task UpdateLeaderboard(Guid cupId, Guid clubId);
    }
}
