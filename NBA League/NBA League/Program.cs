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
using NBA_League.controller;
using NBA_League.console;

namespace NBA_League
{
    class Program
    {
        public static Student toStudent(string line)
        {
            string[] values = line.Split(';');
            if (values.Length == 3)
            {
                int id;
                bool ok = int.TryParse(values[0], out id);
                if (ok)
                {
                    return  new Student(id, values[1], values[2]);
                }
            }
            return default(Student);
        }
        static void Main(string[] args)
        {
            convertFromString<Student> t = toStudent;

            TestDomains tstDomains = new TestDomains();
            TestValidation tstValidation = new TestValidation();
            TeamFileRepository repositoryTeamFile = new TeamFileRepository(new ValidationTeam(), @"E:\facultate\NBA League\NBA League\resources\TeamFile.txt");
         
            PlayerFileRepository playerFileRepository = new PlayerFileRepository(new ValidationPlayer(), repositoryTeamFile, @"E:\facultate\NBA League\NBA League\resources\PlayerFile.txt");
            StudentFileRepository studentFileRepository = new StudentFileRepository(new ValidationStudent(), @"E:\facultate\NBA League\NBA League\resources\StudentFile.txt");
            FileRepository<int, Student> studentml = new FileRepository< int, Student> (new ValidationStudent(), @"E:\facultate\NBA League\NBA League\resources\StudentFile.txt", t);
            
            ActivePlayerRepository activePlayerRepository = new ActivePlayerRepository(new ValidationActivePlayer(), @"E:\facultate\NBA League\NBA League\resources\ActivePlayerFile.txt");
            GameFileRepository gameFileRepository = new GameFileRepository(new ValidationGame(), repositoryTeamFile, @"E:\facultate\NBA League\NBA League\resources\GameFile.txt");

            TeamService teamService = new TeamService(repositoryTeamFile, playerFileRepository, gameFileRepository);
            StudentService studentService = new StudentService(studentFileRepository);
            PlayerService playerService = new PlayerService(playerFileRepository, repositoryTeamFile, activePlayerRepository);
            ActivePlayerService activePlayerService = new ActivePlayerService(activePlayerRepository, playerFileRepository, gameFileRepository);
            GameService gameService = new GameService(gameFileRepository, repositoryTeamFile, activePlayerRepository);
            activePlayerService.SaveActivePlayer(2, 4, 32.3, "Rezerva");
            Controller controller = new Controller(teamService, studentService, playerService, gameService, activePlayerService);

            UI ui = new UI(controller);
            ui.run();
            //controller.raportFour(4);
            //DateTime date1 = new DateTime(2019, 12,
              //                   25, 16, 0, 0);
            //DateTime date2 = new DateTime(2019, 12,
              //                   25, 16, 55, 0);
            //controller.raportThree(date1, date2);
            controller.raportTwo(2, 2);
            //foreach (ActivePlayer a in activePlayerService.GetAll())
              //  Console.WriteLine(a);
            //controller.raportTwo(100, 3);
            //teamService.DeleteTeam(1);
            Console.ReadKey();
        }
    }
}
