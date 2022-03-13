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
        private static readonly IEpisodeRepository episodeRepository = new EpisodeRepository(context);
        private static readonly IEnemyRepository enemyRepository = new EnemyRepository(context);
        private static readonly IDoctorRepository doctorRepository = new DoctorRepository(context);
        private static readonly IUnitOfWork unitOfWork = new UnitOfWork(context);
        static void Main(string[] args)
        {
            //ExecuteDbFuncs();
            //ExecuteViewEpisodes();
            //ExecuteStoredProcedure();
            //ExecuteCompanionCRUD();
            //AddEnemyToEpisode();
            //AddCompanionToEpisode();
            //GetAllDoctors();
            //GetEnemyById(2);
            GetCompanionById(3);
            Console.ReadLine();
        }

        private async static void GetCompanionById(int id)
        {
            Companion companion = await companionRepository.FindByIdAsync(id);
            Console.WriteLine(companion.CompanionName);

        }

        private async static void GetEnemyById(int id)
        {
            Enemy enemy = await enemyRepository.FindByIdAsync(id);
            Console.WriteLine(enemy.EnemyName);
        }

        private async static void GetAllDoctors()
        {
            var doctors = await doctorRepository.ListAsync();
            foreach (var doctor in doctors)
            {
                Console.WriteLine(doctor.DoctorName);
            }
        }

        private async static void AddCompanionToEpisode()
        {
            Episode episode = await episodeRepository.FindByIdAsync(5);
            episode.EpisodeCompanions.Add(new EpisodeCompanion { CompanionId = 2 });
            await unitOfWork.CompleteAsync();
        }

        private async static void AddEnemyToEpisode()
        {

            Enemy enemy = new Enemy()
            {
                EnemyName = "Torchwood ",
                Description = "An iconic monster brought about by the shapeshifting aliens, " +
                "the Zygons, the Loch Ness Monster is in fact an augmented cyborg sea monster."
            };
            await enemyRepository.AddAsync(enemy);
            await unitOfWork.CompleteAsync();
            var episodeEnemyJoin = new EpisodeEnemy() { EnemyId = enemy.EnemyId, EpisodeId = 4 };
            context.Add(episodeEnemyJoin);
            await unitOfWork.CompleteAsync();
            //Or
            Episode episode = await episodeRepository.FindByIdAsync(5);
            episode.EpisodeEnemies.Add(new EpisodeEnemy { EnemyId = 6 });
            await unitOfWork.CompleteAsync();

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
