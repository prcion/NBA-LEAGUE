using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBA_League.domain
{
    class Team : IEntity<int>
    {
        public int ID { get; set; }
        public string name { get; set; }

        public Team(int ID, string name){
            this.ID = ID;
            this.name = name;
        }
    
        override public bool Equals(Object obj)
        {
            if (!(obj is Team))
                return false;
            Team objCopy = obj as Team;
            if (objCopy.ID == this.ID)
                return true;
            return false;
        }

        override public string ToString(){
            return ID + " " + name;
        }
        
        override public int GetHashCode()
        {
            return ID.GetHashCode();
        }

        public static bool operator == (Team teamOne, Team teamTwo)
        {
            return teamOne.Equals(teamTwo);
        }

        public static bool operator !=(Team teamOne, Team teamTwo)
        {
            return !teamOne.Equals(teamTwo);
        }
    }
}
