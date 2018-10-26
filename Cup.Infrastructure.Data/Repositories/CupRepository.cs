using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;

using Cup.Domain.Core.AggregatesModel.CupAggregate;
using Cup.Domain.Core.Interfaces;

namespace Cup.Infrastructure.Data.Repositories
{
    public class CupRepository : ICupRepository
    {
        private readonly CupContext _context;

        public CupRepository()
        {
            _context = new CupContext();
        }

        public async Task<Domain.Core.AggregatesModel.CupAggregate.Cup> GetByIdAsync(Guid cupId)
        {
            var builder = new FilterDefinitionBuilder<Domain.Core.AggregatesModel.CupAggregate.Cup>();
            var filter = Builders<Domain.Core.AggregatesModel.CupAggregate.Cup>.Filter.Eq("Id", cupId);

            return await _context.Cups.Find(filter).FirstOrDefaultAsync();

        }

        public async Task<List<Domain.Core.AggregatesModel.CupAggregate.Cup>> GetAllActiveAsync()
        {
            var builder = new FilterDefinitionBuilder<Domain.Core.AggregatesModel.CupAggregate.Cup>();
            var filter = builder.Empty;

            return await _context.Cups.Find(filter).ToListAsync();
        }

        public async Task UpdateParticipantAsync(Guid cupId, Participant participant)
        {
            var filter = Builders<Domain.Core.AggregatesModel.CupAggregate.Cup>
                .Filter
                .Where(cup => cup.Id == cupId && cup.Leaderboard.Participants.Any(item => item.ClubId == participant.ClubId));

            var update = Builders<Domain.Core.AggregatesModel.CupAggregate.Cup>
                .Update.Set(x => x.Leaderboard.Participants[-1].Score, participant.Score)
                       .Set(x => x.Leaderboard.Participants[-1].ClubPoints.MP, participant.ClubPoints.MP)
                       .Set(x => x.Leaderboard.Participants[-1].ClubPoints.W, participant.ClubPoints.W)
                       .Set(x => x.Leaderboard.Participants[-1].ClubPoints.L, participant.ClubPoints.L);

            await _context.Cups.UpdateOneAsync(filter, update);
        }

        public async Task SaveParticipantAsync(Guid cupId, Participant participant)
        {
            var filter = Builders<Domain.Core.AggregatesModel.CupAggregate.Cup>
                .Filter.Where(cup => cup.Id == cupId);

            var update = Builders<Domain.Core.AggregatesModel.CupAggregate.Cup>
                .Update.AddToSet(cup => cup.Leaderboard.Participants, participant);

            await _context.Cups.UpdateOneAsync(filter, update);
        }
    }
}
