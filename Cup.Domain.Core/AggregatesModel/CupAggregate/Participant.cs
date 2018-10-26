using System;

namespace Cup.Domain.Core.AggregatesModel.CupAggregate
{
    [Serializable]
    public class Participant
    {
        public Guid ClubId { get; private set; }
        public int Score { get; private set; }
        public ClubPoints ClubPoints { get; private set; }

        internal Participant(Guid clubId)
        {
            ClubId = clubId;
            Score = 0;
            ClubPoints = new ClubPoints(0, 0, 0);
        }

        internal void AddScore(int score)
        {
            Score += score;
        }

        internal void AddClubPoints(ClubPoints clubPoints)
        {
            ClubPoints = clubPoints;
        }
    }
}