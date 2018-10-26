using Cup.Domain.Core.AggregatesModel.CupAggregate;
using Microsoft.ServiceFabric.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CupService
{
    public class LeaderboardSerializer : IStateSerializer<Leaderboard>
    {
        public Leaderboard Read(BinaryReader binaryReader)
        {


            throw new NotImplementedException();

        }

        public Leaderboard Read(Leaderboard baseValue, BinaryReader binaryReader)
        {
            throw new NotImplementedException();
        }

        public void Write(Leaderboard value, BinaryWriter binaryWriter)
        {
            throw new NotImplementedException();
        }

        public void Write(Leaderboard baseValue, Leaderboard targetValue, BinaryWriter binaryWriter)
        {
            throw new NotImplementedException();
        }
    }
}
