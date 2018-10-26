using System;

namespace Cup.Domain.Core.AggregatesModel.CupAggregate
{
    [Serializable]
    public class ClubPoints
    {
        public int MP { get; private set; }
        public int W { get; private set; }
        public int L { get; private set; }

        public ClubPoints(int mp, int w, int l)
        {
            MP = mp;
            W = w;
            L = l;
        }
    }
}