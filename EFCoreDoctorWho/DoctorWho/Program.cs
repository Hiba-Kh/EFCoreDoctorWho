using Dapper;
using DoctorWho.Db;
using DoctorWho.Db.Mappings;
using DoctorWho.Db.Models;
using DoctorWho.Db.Repositories;
using DoctorWho.Domain;
using Microsoft.EntityFrameworkCore;
using Supermarket.API.Domain.Repositories;
using Supermarket.API.Persistence.Repositories;
using System;
using System.Data;
using System.Linq;

namespace DoctorWho
{
    class Program
    {
        private static readonly DoctorWhoCoreDbContext context = new DoctorWhoCoreDbContext(); 
        private static readonly DapperAccess depperAccess = new DapperAccess("server = (localdb)\\MSSQLLocalDB; database = DoctorWhoCore; Trusted_Connection=Yes");
        private static readonly ICompanionRepository companionRepository = new CompanionRepository(context);
        private static readonly IUnitOfWork unitOfWork = new UnitOfWork(context);
        static void Main(string[] args)
        {
            //ExecuteDbFuncs();
            //ExecuteViewEpisodes();
            //ExecuteStoredProcedure();
            ExecuteCompanionCRUD();
            Console.ReadLine();
        }

        private async static void ExecuteCompanionCRUD()
        {
            Companion companionToInsert = new Companion() { CompanionName = "Micheal Alen", WhoPlayed = "Freema Agyeman" };
            await companionRepository.AddAsync(companionToInsert);
            await unitOfWork.CompleteAsync();
        }

        static void ExecuteViewEpisodes()
        {
            var summarise = context.ViewEpisodes;
            foreach (var result in summarise)
                Console.WriteLine($"{result.EpisodeId}, {result.Enemies}");
        }

        static void ExecuteStoredProcedure()
        {
            var result = depperAccess.SpSummariseEpisodes();
            var companionResult = result.EpisodesSummariseCompanions;
            var enemiesResult = result.EpisodesSummariseEnemies;
            Console.WriteLine("Companion Id: Companion Count");
            foreach (var companion in companionResult)
                Console.WriteLine($"{companion.CompanionId}: {companion.Count}");
            Console.WriteLine("Enemy Id: Enemy Count");
            foreach (var enemiy in enemiesResult)
                Console.WriteLine($"{enemiy.EnemyId}: {enemiy.Count}");
        }

        static void ExecuteDbFuncs()
        {
            var episodeID = 1;
            var companionName = from p in context.fnCompanion(episodeID)
                         select p.CompanionName;
            Console.WriteLine(companionName.FirstOrDefault());
            //same for fnEnemies
        }
    }
}
