using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NBA_League.domain;
using NBA_League.validation;

namespace NBA_League.repository
{
    class GameFileRepository : AbstractRepository<int, Game>
    {
        string fileName;
        private TeamFileRepository teamFileRepository;
        public GameFileRepository(IValidator<Game> validator, TeamFileRepository teamFileRepository, string fileName) : base(validator)
        {
            this.fileName = fileName;
            this.teamFileRepository = teamFileRepository;
            LoadFromFile();
        }

        private void LoadFromFile()
        {
            using (TextReader read = File.OpenText(fileName))
            {
                string line;
                while ((line = read.ReadLine()) != null)
                {
                    string[] values = line.Split(';');
                    if (values.Length == 3)
                    {
                        int id;
                        bool ok = int.TryParse(values[0], out id);
                        if (ok)
                        {
                            int idFirstTeam;
                            ok = int.TryParse(values[1], out idFirstTeam);
                            if (ok)
                            {
                                Team team = teamFileRepository.FindOne(idFirstTeam);
                                if (!(team is default(Team)))
                                {
                                    int idSecondTeam;
                                    ok = int.TryParse(values[2], out idSecondTeam);

                                    if (ok)
                                    {
                                        Team teamTwo = teamFileRepository.FindOne(idSecondTeam);
                                        if (!(teamTwo is default(Team)))
                                        {
                                            DateTime data;
                                            ok = DateTime.TryParse(values[3], out data);
                                            Game game = new Game(id, team, teamTwo, data);
                                            base.Save(game);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void WriteToFile()
        {
            using (TextWriter writer = File.CreateText(fileName))
            {
                foreach (Game game in FindAll())
                {
                    writer.WriteLine("{0};{1};{2};{3}", game.ID, game.teamOne.ID, game.teamTwo.ID, game.date.ToString());
                }
            }
        }

        public override Game Save(Game e)
        {
            Game game = base.Save(e);
            WriteToFile();
            return game;
        }

        public override Game Update(Game e)
        {
            Game game = base.Update(e);
            WriteToFile();
            return game;
        }

        public override Game Delete(int id)
        {
            Game game = base.Delete(id);
            WriteToFile();
            return game;
        }
    }

}
