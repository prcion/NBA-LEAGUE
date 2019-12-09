using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using NBA_League.domain;

namespace NBA_League.test
{
    class TestDomains
    {
        public TestDomains()
        {
            TestTeam();
            TestStudent();
            TestPlayer();
            TestGame();
            TestActivePlayer();
        }
        private void TestTeam()
        {
            Team teamOne = new Team(1, "Houston Rockets");
            Team teamTwo = new Team(2, "Chicago Bulls");

            Debug.Assert(teamOne.ID == 1);
            Debug.Assert(teamOne.name == "Houston Rockets");

            Debug.Assert(teamTwo.ID == 2);
            Debug.Assert(teamTwo.name == "Chicago Bulls");

            teamOne.ID = 3;
            teamOne.name = "Chicago Bulls";
            teamTwo.ID = 4;
            teamTwo.name = "Houston Rockets";

            Debug.Assert(teamOne.ID == 3);
            Debug.Assert(teamOne.name == "Chicago Bulls");

            Debug.Assert(teamTwo.ID == 4);
            Debug.Assert(teamTwo.name == "Houston Rockets");
        }

        private void TestStudent()
        {
            Student studentOne = new Student(1, "Vlad", "Liceul Teoretic ELF");
            Student studentTwo = new Student(2, "Ion", "Liceul de Informatica Tiberiu Popoviciu");

            Debug.Assert(studentOne.ID == 1);
            Debug.Assert(studentOne.name == "Vlad");
            Debug.Assert(studentOne.school == "Liceul Teoretic ELF");

            Debug.Assert(studentTwo.ID == 2);
            Debug.Assert(studentTwo.name == "Ion");
            Debug.Assert(studentTwo.school == "Liceul de Informatica Tiberiu Popoviciu");

            studentOne.ID = 3;
            studentOne.name = "Tudor";
            studentOne.school = "Liceul de Informatica Tiberiu Popoviciu";

            studentTwo.ID = 4;
            studentTwo.name = "Octavian";
            studentTwo.school = "Liceul Teoretic ELF";

            Debug.Assert(studentOne.ID == 3);
            Debug.Assert(studentOne.name == "Tudor");
            Debug.Assert(studentOne.school == "Liceul de Informatica Tiberiu Popoviciu");

            Debug.Assert(studentTwo.ID == 4);
            Debug.Assert(studentTwo.name == "Octavian");
            Debug.Assert(studentTwo.school == "Liceul Teoretic ELF");

        }

        private void TestPlayer()
        {
            Team teamOne = new Team(1, "Houston Rockets");
            Team teamTwo = new Team(2, "Chicago Bulls");
            Player player = new Player(1, "Ion", "Liceul Teoretic ELF", teamOne);
            Player playerTwo = new Player(2, "Vasile", "Liceul de Informatica Tiberiu Popoviciu", teamTwo);

            Debug.Assert(player.ID == 1);
            Debug.Assert(player.name == "Ion");
            Debug.Assert(player.school == "Liceul Teoretic ELF");
            Debug.Assert(player.team == teamOne);

            Debug.Assert(playerTwo.ID == 2);
            Debug.Assert(playerTwo.name == "Vasile");
            Debug.Assert(playerTwo.school == "Liceul de Informatica Tiberiu Popoviciu");
            Debug.Assert(playerTwo.team == teamTwo);

        }

        private void TestGame()
        {
            Team teamOne = new Team(1, "Houston Rockets");
            Team teamTwo = new Team(2, "Chicago Bulls");
            DateTime dataTime = new DateTime(2018, 1, 30);
            Game game = new Game( 1, teamOne, teamTwo, dataTime);
            Debug.Assert(game.teamOne == teamOne);
            Debug.Assert(game.teamTwo == teamTwo);
            Debug.Assert(game.date == dataTime);

        }

        private void TestActivePlayer()
        {
            ActivePlayer activePlayer = new ActivePlayer(1, 1, 2, "Rezerva");
            ActivePlayer activePlayerTwo = new ActivePlayer(2, 2, 2, "Participant");

            Debug.Assert(activePlayer.ID == "1.1");
            Debug.Assert(activePlayer.numberOfPoints == 2);
            Debug.Assert(activePlayer.type == "Rezerva");

            Debug.Assert(activePlayerTwo.ID == "2.2");
            Debug.Assert(activePlayerTwo.numberOfPoints == 2);
            Debug.Assert(activePlayerTwo.type == "Participant");

        }

    }
}