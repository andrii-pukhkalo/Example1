using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;

using Cup.Domain.Core.AggregatesModel.ClubAggregate;
using Cup.Domain.Core.Interfaces;

namespace Cup.Infrastructure.Data.Repositories
{
    public class ClubRepository : IClubRepository
    {
        private readonly CupContext _context;

        public ClubRepository()
        {
            _context = new CupContext();
        }

        public async Task<List<Club>> GetAllAsync()
        {
            var builder = new FilterDefinitionBuilder<Club>();
            var filter = builder.Empty;

            return await _context.Clubs.Find(filter).ToListAsync();
            
        }

        public async Task<Club> GetByIdAsync(Guid clubId)
        {
            var builder = new FilterDefinitionBuilder<Club>();
            var filter = Builders<Club>.Filter.Eq("Id", clubId);

            return await _context.Clubs.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<List<Club>> GetSuitableOpponentsForClubAsync(Guid clubId)
        {
            var builder = new FilterDefinitionBuilder<Club>();
            var filter = builder.Empty;

            return await _context.Clubs.Find(filter).ToListAsync();
        }
    }
}
