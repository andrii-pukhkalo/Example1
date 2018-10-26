using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.ServiceFabric.Services.Remoting;

namespace Cup.Service.Interfaces
{
    public interface ICupService : IService
    {
        Task<List<Domain.Core.AggregatesModel.CupAggregate.Cup>> GetActiveCups();
        //Task JoinСup(Guid cupId, Guid clubId);

        //Task<Leaderboard> GetLeaderboard(Guid cupId);
        //Task UpdateLeaderboard(Guid cupId, Guid clubId);
    }
}
