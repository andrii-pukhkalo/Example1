using System;

namespace Cup.Domain.Core.AggregatesModel.CupAggregate
{
    public class Cup
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public DateRange DateRange { get; private set; }
        public bool IsRepetitive { get; private set; }
        public Leaderboard Leaderboard { get; private set; }
        public PayoutSpecification PayoutSpecification { get; private set; }

        // domain behavior

        public Participant ProcessMatchResults(Guid clubId, int mp, int w, int l, int score)
        {
            Participant participant = Leaderboard.Participants.Find(item => item.ClubId == clubId);

            ClubPoints clubPoints = new ClubPoints(
                participant.ClubPoints.MP + mp,
                participant.ClubPoints.W + w,
                participant.ClubPoints.L + l);

            participant.AddScore(score);
            participant.AddClubPoints(clubPoints);

            return participant;
        }

        public int CalculateReward(int rank)
        {
            PayoutRule payoutRule = PayoutSpecification.PayoutRules.Find(item => item.Rank == rank);

            return payoutRule.Reward;
        }

        public Participant RegisterParticipant(Guid clubId)
        {
            return Leaderboard.AddParticipant(clubId);
        }
    }
}
