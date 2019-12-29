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
    delegate T convertFromString<T>(string s);
    class FileRepository<ID, T>: AbstractRepository<ID, T> where T : IEntity<ID>
    {
        string fileName;
        private convertFromString<T> str;
        public FileRepository(IValidator<T> validator, string fileName, convertFromString<T> str) : base(validator)
        {
            this.fileName = fileName;
            this.str = str;
            LoadFromFile();
        }

        private void LoadFromFile()
        {
            using (TextReader read = File.OpenText(fileName))
            {
                string line;
                while ((line = read.ReadLine()) != null)
                {
                    T n = str(line);
                    base.Save(n);
                }
            }
        }
    }
}
