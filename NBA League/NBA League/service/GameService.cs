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
    class GameService
    {
        private GameFileRepository gameRepo;
        private TeamFileRepository teamRepo;
        private ActivePlayerRepository activePlayerRepo;
        private Game game;

        public GameService(GameFileRepository gameRepo, TeamFileRepository teamRepo, ActivePlayerRepository activePlayerRepo)
        {
            this.gameRepo = gameRepo;
            this.teamRepo = teamRepo;
            this.activePlayerRepo = activePlayerRepo;
            this.game = default(Game);
        }

        public Game SaveGame(int IDOne, int IDTwo, DateTime date)
        {
            game = default(Game);
            Team teamOne = teamRepo.FindOne(IDOne);
            Team teamTwo = teamRepo.FindOne(IDTwo);
            if(!(teamOne is default(Team)) && !(teamTwo is default(Team)))
            {
                try
                {
                    game = gameRepo.Save(new Game(getMaxID(), teamOne, teamTwo, date));
                }catch(ValidationException ex)
                {
                    Console.WriteLine(ex.Message.ToString());
                }
            }
            return game;
        }

        public Game SaveGame(int ID, int IDOne, int IDTwo, DateTime date)
        {
            game = default(Game);
            Team teamOne = teamRepo.FindOne(IDOne);
            Team teamTwo = teamRepo.FindOne(IDTwo);
            if (!(teamOne is default(Team)) && !(teamTwo is default(Team)))
            {
                try
                {
                    game = gameRepo.Update(new Game(ID, teamOne, teamTwo, date));
                }
                catch (ValidationException ex)
                {
                    Console.WriteLine(ex.Message.ToString());
                }
            }
            return game;
        }

        public Game DeleteGame(int ID)
        {
            game = default(Game);
            try
            {
                game = gameRepo.Delete(ID);
                if (!(game is default(Game)))
                {
                    List<ActivePlayer> lst = activePlayerRepo.FindAll().ToList();
                    foreach (ActivePlayer a in lst)
                    {
                        string[] str = a.ID.Split('.');
                        int IDGame;
                        bool ok = int.TryParse(str[1], out IDGame);
                        if (ok)
                        {
                            if (IDGame == ID)
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
            return game;
        }

        public Game FindOne(int ID)
        {
            return gameRepo.FindOne(ID);
        }

        public IEnumerable<Game> GetAll()
        {
            return gameRepo.FindAll();
        }

        private int getMaxID()
        {
            int Max = 0;
            foreach (Game game in gameRepo.FindAll())
            {
                if (game.ID > Max)
                    Max = game.ID;
            }
            return Max + 1;
        }
    }
}
