using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NBA_League.service;
using NBA_League.domain;

namespace NBA_League.controller
{
    class Controller
    {
        private TeamService teamService;
        private StudentService studentService;
        private PlayerService playerService;
        private GameService gameService;
        private ActivePlayerService activePlayerService;

        public Controller(TeamService teamService, StudentService studentService, PlayerService playerService,
                          GameService gameService, ActivePlayerService activePlayerService)
        {
            this.teamService = teamService;
            this.studentService = studentService;
            this.playerService = playerService;
            this.gameService = gameService;
            this.activePlayerService = activePlayerService;
        }
        
        public List<Player> raportOne(int IDTeam)
        {
            var lst = from s in playerService.GetAll()
                               where s.team.ID == IDTeam
                               select s;
            List<Player> list = lst.ToList();
            return list;
        }

        public List<Player> raportTwo(int IDGame, int IDTeam)
        {
            List<Player> list = new List<Player>();
            foreach (ActivePlayer a in activePlayerService.GetAll())
            {
                string[] s = a.ID.Split('.');
                int id;
                int idPlayer;
                bool ok = int.TryParse(s[1], out id);
                if (ok) {
                    ok = int.TryParse(s[0], out idPlayer);
                    if (ok)
                    {
                        if (id == IDGame)
                        {
                            var lst = from p in playerService.GetAll()
                                      where p.ID == idPlayer && p.team.ID == IDTeam
                                      select p;
                            foreach(var i in lst)
                                list.Add(i);

                        }
                    }
                }
            }
            return list;
        }

        public List<Game> raportThree(DateTime dateTimeStart, DateTime dateTimeEnd)
        {
            List<Game> list = new List<Game>();
            var a = from m in gameService.GetAll()
                    where DateTime.Compare(dateTimeStart, m.date) <= 0 && DateTime.Compare(dateTimeEnd, m.date) >= 0
                    select m;
            list = a.ToList();
            return list;
        }

        public double raportFour(int IDGame)
        {
            List<ActivePlayer> list = new List<ActivePlayer>();
            foreach(ActivePlayer a in activePlayerService.GetAll())
            {
                string[] s = a.ID.Split('.');
                int id;
                bool ok = int.TryParse(s[1], out id);
                if (ok)
                {
                    if (id == IDGame)
                     list.Add(a);
                }
            }

            double sum = (from s in list select s.numberOfPoints).Sum();
            return sum;
        }
    }
}
