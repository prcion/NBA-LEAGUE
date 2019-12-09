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
    class PlayerFileRepository : AbstractRepository<int, Player>
    {
        private string fileName;
        private TeamFileRepository teamFileRepository;
        public PlayerFileRepository(IValidator<Player> validator, TeamFileRepository teamFileRepository, string fileName) : base(validator)
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
                    if (values.Length == 4)
                    {
                        int id;
                        int teamID;
                        bool ok = int.TryParse(values[0], out id);
                        if (ok)
                        {
                            ok = int.TryParse(values[3], out teamID);
                            if (ok)
                            {
                                Team team = teamFileRepository.FindOne(teamID);
                                if (!(team is default(Team)))
                                {
                                    Player player = new Player(id, values[1], values[2], team);
                                    base.Save(player);
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
                foreach (Player player in FindAll())
                {
                    writer.WriteLine("{0};{1};{2};{3}", player.ID, player.name, player.school, player.team.ID);
                }
            }
        }

        public override Player Save(Player e)
        {
            
            Player player = base.Save(e);
            WriteToFile();
            return player;
        }

        public override Player Update(Player e)
        {
            Player player = base.Update(e);
            WriteToFile();
            return player;
        }

        public override Player Delete(int id)
        {
            Player player = base.Delete(id);
            WriteToFile();
            return player;
        }
        
    }
}
