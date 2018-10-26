using System;
using System.Collections.Generic;
using System.Linq;

namespace Cup.Domain.Core.AggregatesModel.CupAggregate
{
    [Serializable]
    public class Leaderboard
    {
        public List<Participant> Participants { get; private set; }

        internal Participant AddParticipant(Guid clubId)
        {
            Participant participant = new Participant(clubId);

            Participants.Add(participant);

            return participant;
        }

        public List<Participant> OrderParticipants()
        {
            return Participants.OrderByDescending(item => item.Score).ToList();
        }
    }
}