using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBA_League.domain
{
    class Player : Student
    {
        public Team team{get; set;}
        public Player(int ID, string name, string school, Team team): base(ID, name, school)
        {
            this.team = team;
        }

        override public bool Equals(Object obj)
        {
            if (!(obj is Player))
                return false;
            Player objCopy = obj as Player;
            if (objCopy.ID == this.ID)
                return true;
            return false;
        }

        override public String ToString()
        {
            return base.ToString() + " " + team;
        }

        override public int GetHashCode()
        {
            return base.ID.GetHashCode();
        }

        public static bool operator ==(Player teamOne, Player teamTwo)
        {
            return teamOne.Equals(teamTwo);
        }

        public static bool operator !=(Player teamOne, Player teamTwo)
        {
            return !teamOne.Equals(teamTwo);
        }
    }
}
