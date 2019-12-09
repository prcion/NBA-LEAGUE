using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBA_League.domain
{
    class ActivePlayer:IEntity<string>
    {
        public string ID { get; set; }
        public double numberOfPoints { get; set; }
        public string type { get; set; }

        public ActivePlayer(int IDPlayer, int IDGame, double numberOfPoints, string type)
        {
            this.ID = IDPlayer + "." + IDGame;
            this.numberOfPoints = numberOfPoints;
            this.type = type;
        }

        override public bool Equals(Object obj)
        {
            if (!(obj is ActivePlayer))
                return false;
            ActivePlayer objCopy = obj as ActivePlayer;
            if (objCopy.ID == this.ID)
                return true;
            return false;
        }

        override public string ToString()
        {
            return ID + " " + numberOfPoints + " " + type;
        }

        override public int GetHashCode()
        {
            return ID.GetHashCode();
        }

        public static bool operator ==(ActivePlayer teamOne, ActivePlayer teamTwo)
        {
            return teamOne.Equals(teamTwo);
        }

        public static bool operator !=(ActivePlayer teamOne, ActivePlayer teamTwo)
        {
            return !teamOne.Equals(teamTwo);
        }
    }
}
