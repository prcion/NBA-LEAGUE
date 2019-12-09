using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBA_League.domain
{
    class Game:IEntity<int>
    {
        public int ID { get; set; }
        public Team teamOne { get; set; }
        public Team teamTwo { get; set; }
        public DateTime date { get; set; }

        public Game(int ID, Team teamOne, Team teamTwo, DateTime date)
        {
            this.ID = ID;
            this.teamOne = teamOne;
            this.teamTwo = teamTwo;
            this.date = date;
        }

        override public bool Equals(Object obj)
        {
            if (!(obj is Game))
                return false;
            Game objCopy = obj as Game;
            if (objCopy.ID == this.ID)
                return true;
            return false;
        }

        override public string ToString()
        {
            return ID + " " + teamOne + " " + teamTwo + " " + date; 
        }

        override public int GetHashCode()
        {
            return ID.GetHashCode();
        }

        public static bool operator ==(Game teamOne, Game teamTwo)
        {
            return teamOne.Equals(teamTwo);
        }

        public static bool operator !=(Game teamOne, Game teamTwo)
        {
            return !teamOne.Equals(teamTwo);
        }
    }
}
