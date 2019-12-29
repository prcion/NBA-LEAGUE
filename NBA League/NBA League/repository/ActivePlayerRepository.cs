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
    class ActivePlayerRepository : AbstractRepository<string, ActivePlayer>
    {
        string fileName;
        public ActivePlayerRepository(IValidator<ActivePlayer> validator, string fileName) : base(validator)
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
                    if (values.Length == 3)
                    {
                        double numberOfPoints;
                        bool ok = double.TryParse(values[1], out numberOfPoints);
                        if (ok)
                        {
                            string[] val = values[0].Split('.');
                            int IDOne;
                            int IDTwo;
                            ok = int.TryParse(val[0], out IDOne);
                            if (ok)
                            {
                                ok = int.TryParse(val[1], out IDTwo);
                                if (ok)
                                {
                                    ActivePlayer activePlayer = new ActivePlayer(IDOne, IDTwo, numberOfPoints, values[2]);
                                    base.Save(activePlayer);
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
                foreach (ActivePlayer activePlayer in FindAll())
                {
                    writer.WriteLine("{0};{1};{2}", activePlayer.ID, activePlayer.numberOfPoints, activePlayer.type);
                }
            }
        }

        public override ActivePlayer Save(ActivePlayer e)
        {
            ActivePlayer activePlayer = base.Save(e);
            WriteToFile();
            return activePlayer;
        }

        public override ActivePlayer Update(ActivePlayer e)
        {
            ActivePlayer activePlayer = base.Update(e);
            WriteToFile();
            return activePlayer;
        }

        public override ActivePlayer Delete(string id)
        {
            ActivePlayer activePlayer = base.Delete(id);
            WriteToFile();
            return activePlayer;
        }
    }
}
