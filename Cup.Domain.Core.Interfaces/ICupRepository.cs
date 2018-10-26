using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Cup.Domain.Core.AggregatesModel.CupAggregate;

namespace Cup.Domain.Core.Interfaces
{
    public interface ICupRepository
    {
        Task<AggregatesModel.CupAggregate.Cup> GetByIdAsync(Guid cupId);
        Task<List<AggregatesModel.CupAggregate.Cup>> GetAllActiveAsync();
        Task UpdateParticipantAsync(Guid cupId, Participant participant);
        Task SaveParticipantAsync(Guid cupId, Participant participant);
    }
}
