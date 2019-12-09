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
    class TeamFileRepository : AbstractRepository<int, Team>
    {
        string fileName;
        public TeamFileRepository(IValidator<Team> validator, string fileName) : base(validator)
        {
            this.fileName = fileName;
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
                    if (values.Length == 2)
                    {
                        int id;
                        bool ok = int.TryParse(values[0], out id);
                        if (ok)
                        {
                            Team team = new Team(id, values[1]);
                            base.Save(team);
                        }
                    }
                }
            }
        }

        private void WriteToFile()
        {
            using (TextWriter writer = File.CreateText(fileName))
            {
                foreach (Team team in FindAll())
                {
                    writer.WriteLine("{0};{1}", team.ID, team.name);
                }
            }
        }

        public override Team Save(Team e)
        {
            Team team = base.Save(e);
            WriteToFile();
            return team;
        }

        public override Team Update(Team e)
        {
            Team team = base.Update(e);
            WriteToFile();
            return team;
        }

        public override Team Delete(int id)
        {
            Team team = base.Delete(id);
            WriteToFile();
            return team;
        }
    }
}
