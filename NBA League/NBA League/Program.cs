using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NBA_League.domain;
using NBA_League.test;
using NBA_League.repository;
using NBA_League.validation;

namespace NBA_League
{
    class Program
    {
        static void Main(string[] args)
        {
            TestDomains tstDomains = new TestDomains();
            TestValidation tstValidation = new TestValidation();
            TeamFileRepository repositoryTeamFile = new TeamFileRepository(new ValidationTeam(), @"E:\NBA League\NBA League\resources\TeamFile.txt");
            PlayerFileRepository playerFileRepository = new PlayerFileRepository(new ValidationPlayer(), repositoryTeamFile, @"E:\NBA League\NBA League\resources\PlayerFile.txt");
            StudentFileRepository studentFileRepository = new StudentFileRepository(new ValidationStudent(), @"E:\NBA League\NBA League\resources\StudentFile.txt");
            ActivePlayerRepository activePlayerRepository = new ActivePlayerRepository(new ValidationActivePlayer(), @"E:\NBA League\NBA League\resources\ActivePlayerFile.txt");
            GameFileRepository gameFileRepository = new GameFileRepository(new ValidationGame(), repositoryTeamFile, @"E:\NBA League\NBA League\resources\GameFile.txt");

            Team teamOne = new Team(1, "TeamOne");
            Team teamTwo = new Team(2, "TeamTwo");

            repositoryTeamFile.Save(teamOne);
            repositoryTeamFile.Save(teamTwo);
            Game game = new Game(1, teamOne, teamTwo, DateTime.Now);
            gameFileRepository.Save(game);

            List<Game> list = gameFileRepository.FindAll().ToList();
            foreach (Game p in list)
                Console.WriteLine(p);
            Console.ReadKey();
        }
    }
}
