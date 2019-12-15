using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NBA_League.repository;
using NBA_League.domain;
using NBA_League.validation;


namespace NBA_League.service
{
    class TeamService
    {
        private TeamFileRepository repo;
        private PlayerFileRepository playerRepo;
        private GameFileRepository gameRepo;
        private Team team;

        public TeamService(TeamFileRepository repo, PlayerFileRepository playerRepo, GameFileRepository gameRepo)
        {
            this.repo = repo;
            this.playerRepo = playerRepo;
            this.gameRepo = gameRepo;
            this.team = default(Team);
        }

        public Team SaveTeam(string name)
        {
            team = default(Team);
            try
            {
                team =  repo.Save(new Team(getMaxID(), name));
            }catch(ValidationException e)
            {
                Console.WriteLine(e.Message.ToString());
            }
            return team;
        }

        public Team UpdateTeam(int ID, string name)
        {
            team = default(Team);
            try
            {
                team = repo.Update(new Team(ID, name));
            }
            catch (ValidationException e)
            {
                Console.WriteLine(e.Message.ToString());
            }
            return team;
        }

        public Team DeleteTeam(int ID)
        {
            team = default(Team);
            try
            {
                team = repo.Delete(ID);
                if (!(team is (default(Team))))
                {
                    List<Player> lst = playerRepo.FindAll().ToList();
                    foreach (Player p in lst)
                    {
                        if (p.team.ID == ID)
                        {
                            playerRepo.Delete(p.ID);
                        }
                    }

                    List<Game> lstGame = gameRepo.FindAll().ToList();
                    foreach(Game g in lstGame)
                    {
                        if(g.teamOne.ID == ID || g.teamTwo.ID == ID)
                        {
                            gameRepo.Delete(g.ID);
                        }
                    }
                }
            }
            catch (ValidationException e)
            {
                Console.WriteLine(e.Message.ToString());
            }
            return team;
        }

        public Team FindOne(int ID)
        {
            return repo.FindOne(ID);
        }


        public IEnumerable<Team> GetAll()
        {
            return repo.FindAll();
        }

        private int getMaxID()
        {
            int Max = 0;
            foreach(Team team in repo.FindAll())
            {
                if (team.ID > Max)
                    Max = team.ID;
            }
            return Max + 1;
        }
    }
}
