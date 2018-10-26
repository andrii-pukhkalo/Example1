using System;

namespace Cup.Domain.Core.AggregatesModel.ClubAggregate
{
    public class Club
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
    }
}
