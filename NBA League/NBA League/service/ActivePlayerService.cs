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
    class ActivePlayerService
    {
        private ActivePlayerRepository activePlayerRepo;
        private PlayerFileRepository playerRepo;
        private GameFileRepository gameRepo;
        private ActivePlayer activePlayer;
        public ActivePlayerService(ActivePlayerRepository activePlayerRepo, PlayerFileRepository playerRepo, 
                                      GameFileRepository gameRepo)
        {
            this.activePlayerRepo = activePlayerRepo;
            this.playerRepo = playerRepo;
            this.gameRepo = gameRepo;
        }

        public ActivePlayer SaveActivePlayer(int IDPlayer, int IDGame, double numberOfPoints, string type)
        {
            activePlayer = default(ActivePlayer);
            Player player = playerRepo.FindOne(IDPlayer);
            Game game = gameRepo.FindOne(IDGame);
            if (!(player is default(Player)) && !(game is default(Game))) {
                try
                {
                    activePlayer = activePlayerRepo.Save(new ActivePlayer(IDPlayer, IDGame, numberOfPoints, type));
                }catch(ValidationException ex)
                {
                    Console.WriteLine(ex.Message.ToString());
                }
            }
            return activePlayer;
        }

        public ActivePlayer UpdateActivePlayer(int IDPlayer, int IDGame, double numberOfPoints, string type)
        {
            activePlayer = default(ActivePlayer);
            Player player = playerRepo.FindOne(IDPlayer);
            Game game = gameRepo.FindOne(IDGame);
            if (!(player is default(Player)) && !(game is default(Game)))
            {
                try
                {
                    activePlayer = activePlayerRepo.Update(new ActivePlayer(IDPlayer, IDGame, numberOfPoints, type));
                }
                catch (ValidationException ex)
                {
                    Console.WriteLine(ex.Message.ToString());
                }
            }
            return activePlayer;
        }

        public ActivePlayer DeleteActivePlayer(int IDPlayer, int IDGame)
        {
            activePlayer = default(ActivePlayer);
            string ID = IDPlayer + "." + IDGame;
            try
            {
                activePlayer = activePlayerRepo.Delete(ID);
            }
            catch (ValidationException ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
            return activePlayer;
        }

        public ActivePlayer FindOne(int IDPlayer, int IDGame)
        {
            string ID = IDPlayer + "." + IDGame;
            return activePlayerRepo.FindOne(ID);
        }

        public IEnumerable<ActivePlayer> GetAll()
        {
            return activePlayerRepo.FindAll();
        }
    }
}
