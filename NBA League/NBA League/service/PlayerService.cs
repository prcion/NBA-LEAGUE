using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NBA_League.domain;
using NBA_League.repository;
using NBA_League.validation;

namespace NBA_League.service
{
    class PlayerService
    {
        private PlayerFileRepository playerRepo;
        private TeamFileRepository teamRepo;
        private ActivePlayerRepository activePlayerRepo;
        private Player player;

        public PlayerService(PlayerFileRepository playerRepo, TeamFileRepository teamRepo, ActivePlayerRepository activePlayerRepo)
        {
            this.playerRepo = playerRepo;
            this.teamRepo = teamRepo;
            this.activePlayerRepo = activePlayerRepo;
        }
        
        public Player SavePlayer(string name, string school, int IDTeam)
        {
            player = default(Player);
            Team team = teamRepo.FindOne(IDTeam);
            if(!(team is default(Team)))
            {
                try
                {
                    player = playerRepo.Save(new Player(getMaxID(), name, school, team));
                }catch(ValidationException ex)
                {
                    Console.WriteLine(ex.Message.ToString());
                }
            }
            return player;
        }

        public Player UpdatePlayer(int ID, string name, string school, int IDTeam)
        {
            player = default(Player);
            Team team = teamRepo.FindOne(IDTeam);
            if (!(team is default(Team)))
            {
                try
                {
                    player = playerRepo.Update(new Player(ID, name, school, team));
                }
                catch (ValidationException ex)
                {
                    Console.WriteLine(ex.Message.ToString());
                }
            }
            return player;
        }

        public Player DeletePlayer(int ID)
        {
            player = default(Player);
            try
            {
                player = playerRepo.Delete(ID);
                if(!(player is default(Player)))
                {
                    List<ActivePlayer> lst = activePlayerRepo.FindAll().ToList();
                    foreach(ActivePlayer a in lst)
                    {
                        string[] str = a.ID.Split('.');
                        int IDPlayer;
                        bool ok = int.TryParse(str[0], out IDPlayer);
                        if (ok)
                        {
                            if(IDPlayer == ID)
                            {
                                activePlayerRepo.Delete(a.ID);
                            }
                        }
                    }
                }
            }
            catch (ValidationException ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
            return player;
        }

        public Player FindOne(int ID)
        {
            return playerRepo.FindOne(ID);
        }

        public IEnumerable<Player> GetAll()
        {
            return playerRepo.FindAll();
        }

        private int getMaxID()
        {
            int Max = 0;
            foreach (Player player in playerRepo.FindAll())
            {
                if (player.ID > Max)
                    Max = player.ID;
            }
            return Max + 1;
        }
    }
}
