using System;
using System.Collections.Generic;
using System.Fabric;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Data.Collections;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using Cup.Domain.Core.Interfaces;
using Cup.Domain.Core.AggregatesModel.CupAggregate;
using Cup.Domain.Core.AggregatesModel.ClubAggregate;
using Microsoft.ServiceFabric.Data;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using CupService.Interfaces;
using Microsoft.ServiceFabric.Services.Remoting.Runtime;

namespace CupService
{
    /// <summary>
    /// An instance of this class is created for each service replica by the Service Fabric runtime.
    /// </summary>
    internal sealed class CupService : StatefulService, ICupService
    {
        private ICupRepository _cupRepository;
        private IClubRepository _clubRepository;

        public CupService(StatefulServiceContext context, ICupRepository cupRepository, IClubRepository clubRepository)
            : base(context)
        {
            //if (!StateManager.TryAddStateSerializer(new LeaderboardSerializer()))
            //{
            //    throw new InvalidOperationException("Failed to set Leaderboard custom serializer");
            //}

            _cupRepository = cupRepository;
            _clubRepository = clubRepository;
        }

        public async Task<List<Cup.Domain.Core.AggregatesModel.CupAggregate.Cup>> GetActiveCups()
        {
            var cups = await _cupRepository.GetAllActiveAsync();

            return cups;
        }

        public async Task JoinCup(Guid cupId, Guid clubId)
        {
            var cup = await _cupRepository.GetByIdAsync(cupId);

            Participant participant = cup.RegisterParticipant(clubId);

            await _cupRepository.SaveParticipantAsync(cupId, participant);

            //organize match (was impl)
            // register match (was impl)
        }

        public async Task<Leaderboard> GetLeaderboard(Guid cupId)
        {
            // need to implement cache
            var cachedLeaderboards = await StateManager.GetOrAddAsync<IReliableDictionary<Guid, byte[]>>("cachedLeaderboards");

            using (var transaction = StateManager.CreateTransaction())
            {
                var result = await cachedLeaderboards.TryGetValueAsync(transaction, cupId);

                //check

                ////serialize
                //var cup = await _cupRepository.GetByIdAsync(cupId);

                //BinaryFormatter bf = new BinaryFormatter();
                //MemoryStream ms = new MemoryStream();
                //bf.Serialize(ms, cup.Leaderboard);

                //var arr = ms.ToArray();

                ////deserialize

                //MemoryStream memStream = new MemoryStream();
                //BinaryFormatter binForm = new BinaryFormatter();
                //memStream.Write(arr, 0, arr.Length);
                //memStream.Seek(0, SeekOrigin.Begin);
                //Leaderboard leaderboard = (Leaderboard)binForm.Deserialize(memStream);

                if (result.HasValue)
                {
                    var arr = result.Value;

                    MemoryStream memStream = new MemoryStream();
                    BinaryFormatter binForm = new BinaryFormatter();
                    memStream.Write(arr, 0, arr.Length);
                    memStream.Seek(0, SeekOrigin.Begin);
                    Leaderboard leaderboard = (Leaderboard)binForm.Deserialize(memStream);
                    return leaderboard;
                }
                else
                {
                    var cup = await _cupRepository.GetByIdAsync(cupId);

                    BinaryFormatter bf = new BinaryFormatter();
                    MemoryStream ms = new MemoryStream();
                    bf.Serialize(ms, cup.Leaderboard);

                    var arr = ms.ToArray();

                    await cachedLeaderboards.AddAsync(transaction, cupId, arr);

                    await transaction.CommitAsync();
                    return cup.Leaderboard;
                }
            }
        }

        public async Task UpdateLeaderboard(Guid cupId, Guid clubId)
        {
            var cup = await _cupRepository.GetByIdAsync(cupId);

            // !!! here we have the match results (score, mp, w,l)

            Participant participant = cup.ProcessMatchResults(clubId, 1, 1, 0, 15);

            await _cupRepository.UpdateParticipantAsync(cupId, participant);
        }

        /// <summary>
        /// Optional override to create listeners (e.g., HTTP, Service Remoting, WCF, etc.) for this service replica to handle client or user requests.
        /// </summary>
        /// <remarks>
        /// For more information on service communication, see https://aka.ms/servicefabricservicecommunication
        /// </remarks>
        /// <returns>A collection of listeners.</returns>
        protected override IEnumerable<ServiceReplicaListener> CreateServiceReplicaListeners()
        {
    
            return new[] {
                new ServiceReplicaListener(context => this.CreateServiceRemotingListener(context))
            };
        }

        /// <summary>
        /// This is the main entry point for your service replica.
        /// This method executes when this replica of your service becomes primary and has write status.
        /// </summary>
        /// <param name="cancellationToken">Canceled when Service Fabric needs to shut down this service replica.</param>
        protected override async Task RunAsync(CancellationToken cancellationToken)
        {
            // GetActiveCups works fine
            //List<Cup.Domain.Core.AggregatesModel.CupAggregate.Cup> cups = await GetActiveCups();

            //var cup = cups[0];
            //var clubs = await _clubRepository.GetAllAsync();

            // JoinCup works fine
            //await JoinCup(cup.Id, clubs[0].Id);

            //var cachedLeaderboards = await StateManager.GetOrAddAsync<IReliableDictionary<Guid, byte[]>>("cachedLeaderboards");

            //await cachedLeaderboards.ClearAsync();

            // GetLeaderboard works fine !!! need to implement cache via state manager
            //Leaderboard leaderboard = await GetLeaderboard(cup.Id);

            
            
        }

        
    }
}
