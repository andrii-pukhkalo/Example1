using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Cup.Domain.Core.AggregatesModel.ClubAggregate;

namespace Cup.Domain.Core.Interfaces
{
    public interface IClubRepository
    {
        Task<List<Club>> GetSuitableOpponentsForClubAsync(Guid clubId);
        Task<Club> GetByIdAsync(Guid clubId);
        //temp method
        Task<List<Club>> GetAllAsync();
    }
}
