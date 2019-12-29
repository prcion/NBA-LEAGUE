using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NBA_League.controller;
using NBA_League.domain;

namespace NBA_League.console
{
    class UI
    {
        Controller controller;
        public UI(Controller controller)
        {
            this.controller = controller;
        }

        public void StartMenu()
        {
            Console.WriteLine("Pentru afisarea a toti jucatorii a unei echipe tasteaza 1.");
            Console.WriteLine("Pentru afisarea a toti jucatorii activi a unei echipe de la un anumit meci tasteaza 2.");
            Console.WriteLine("Pentru afisarea meciurile dintr-o perioada calanderistica tasteaza 3.");
            Console.WriteLine("Pentru afisarea scorul a unui meci tasteaza 4.");
            Console.WriteLine("Pentru iesire tasteaza 0.");
        }

        public void raport1()
        {
            Console.WriteLine("Id-ul echipei ");
            string menu;
            menu = Console.ReadLine();
            int ok = Convert.ToInt32(menu);
            List<Player> lst = controller.raportOne(ok);
            Console.WriteLine("Jucatorii echipei sunt:");
            foreach (Player a in lst)
                Console.WriteLine(a);
        }

        public void raport2()
        {
            Console.WriteLine("Id-ul meciului ");
            string menu;
            menu = Console.ReadLine();
            int ok = Convert.ToInt32(menu);
            Console.WriteLine("Id-ul echipei ");
            menu = Console.ReadLine();
            int ok2 = Convert.ToInt32(menu);

            List<Player> lst = controller.raportTwo(ok, ok2);
            Console.WriteLine("Jucatorii activi ai echipei sunt:");
            foreach (Player a in lst)
                Console.WriteLine(a);
        }

        public void raport3()
        {
            Console.WriteLine("Data de inceput format(an luna zi).");
            string menu;
            menu = Console.ReadLine();
            string[] line = menu.Split(' ');
            DateTime date1 = new DateTime(Convert.ToInt32(line[0]), Convert.ToInt32(line[1]),
                               Convert.ToInt32(line[2]), 0, 0, 0);
            Console.WriteLine("Data de sfarsit format(an luna zi).");
            menu = Console.ReadLine();
            line = menu.Split(' ');
            DateTime date2 = new DateTime(Convert.ToInt32(line[0]), Convert.ToInt32(line[1]),
                               Convert.ToInt32(line[2]), 0, 0, 0);
            List<Game> lst = controller.raportThree(date1, date2);
            foreach (Game a in lst)
                Console.WriteLine(a);
        }
        public void raport4()
        {
            Console.WriteLine("Id-ul meciului ");
            string menu;
            menu = Console.ReadLine();
            int ok = Convert.ToInt32(menu);
            Console.WriteLine(controller.raportFour(ok));
        }

        public void run()
        {
            int ok = -1;
            while (ok != 0)
            {
                StartMenu();
                string menu;
                menu = Console.ReadLine();
                ok = Convert.ToInt32(menu);
                switch (ok)
                {
                    case 1:
                        raport1();
                        break;
                    case 2:
                        raport2();
                        break;
                    case 3:
                        raport3();
                        break;
                    case 4:
                        raport4();
                        break;
                    case 0:
                        return;
                    default:
                        Console.WriteLine("Nu exista.");
                        break;
                }
            }
        }
    }
}
