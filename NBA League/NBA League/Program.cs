using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NBA_League.domain;
using NBA_League.test;
using NBA_League.repository;
using NBA_League.validation;
using NBA_League.service;

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

            TeamService teamService = new TeamService(repositoryTeamFile, playerFileRepository, gameFileRepository);
            PlayerService playerService = new PlayerService(playerFileRepository, repositoryTeamFile, activePlayerRepository);
            ActivePlayerService activePlayerService = new ActivePlayerService(activePlayerRepository, playerFileRepository, gameFileRepository);
            GameService gameService = new GameService(gameFileRepository, repositoryTeamFile, activePlayerRepository);
            
            gameService.SaveGame(2, 3, DateTime.Now);
            gameService.DeleteGame(2);
            Player p = playerService.SavePlayer("Ion2", "school", 2);
            Player p1 = playerService.UpdatePlayer(1, "ion", "ion", 1);
            activePlayerService.SaveActivePlayer(2, 1, 2, "Participant");
            gameService.DeleteGame(1);
            Console.WriteLine(p);
            Console.WriteLine(p1);
            //teamService.DeleteTeam(1);
            Console.ReadKey();
        }
    }
}
