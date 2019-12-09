using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using NBA_League.validation;
using NBA_League.domain;

namespace NBA_League.test
{
    class TestValidation
    {
        public TestValidation()
        {
            TestValidationTeam();
            TestValidationStudent();
            TestValidationPlayer();
            TestValidionGame();
            TestValidationActivePlayer();
        }

        private void TestValidationTeam()
        {
            ValidationTeam validationTeam = new ValidationTeam();
            Team team = new Team(1, "");
            try
            {
                validationTeam.Validate(team);
            }catch(ValidationException v)
            {
                Debug.Assert(v.Message.ToString() == "Name shouldn't be null!\n");
            }

            Team teamTwo = new Team(-1, "Aba");
            try
            {
                validationTeam.Validate(teamTwo);
            }
            catch (ValidationException v)
            {
                Debug.Assert(v.Message.ToString() == "ID shouldn't be less than 0!\n");
            }

            Team teamThree = new Team(-1, "");
            try
            {
                validationTeam.Validate(teamThree);
            }
            catch (ValidationException v)
            {
                Debug.Assert(v.Message.ToString() == "ID shouldn't be less than 0!\nName shouldn't be null!\n");
            }
        }

        private void TestValidationStudent()
        {
            ValidationStudent validationStudent = new ValidationStudent();

            Student student = new Student(-1, "Ion", "school");
            try
            {
                validationStudent.Validate(student);
            }
            catch (ValidationException v)
            {
                Debug.Assert(v.Message.ToString() == "ID shouldn't be less than 0!\n");
            }

            Student studentTwo = new Student(1, "", "school");
            try
            {
                validationStudent.Validate(studentTwo);
            }
            catch (ValidationException v)
            {
                Debug.Assert(v.Message.ToString() == "Name shouldn't be null!\n");
            }

            Student studentThree = new Student(1, "Ion", "");
            try
            {
                validationStudent.Validate(studentThree);
            }
            catch (ValidationException v)
            {
                Debug.Assert(v.Message.ToString() == "School shouldn't be null!\n");
            }

            Student studentFour = new Student(-1, "", "");
            try
            {
                validationStudent.Validate(studentFour);
            }
            catch (ValidationException v)
            {
                Debug.Assert(v.Message.ToString() == "ID shouldn't be less than 0!\nName shouldn't be null!\nSchool shouldn't be null!\n");
            }
        }

        private void TestValidationPlayer()
        {
            ValidationPlayer validationPlayer = new ValidationPlayer();
            Team team = new Team(1, "sad");
            Player player = new Player(-1, "", "", team);
            try
            {
                validationPlayer.Validate(player);
            }
            catch (ValidationException v)
            {
                Debug.Assert(v.Message.ToString() == "ID shouldn't be less than 0!\nName shouldn't be null!\nSchool shouldn't be null!\n");
            }
        }

        private void TestValidionGame()
        {
            ValidationGame validationGame = new ValidationGame();
            Team team = new Team(1, "2");
            Team teamTwo = new Team(2, "3");
            Game game = new Game(-1, team, teamTwo, DateTime.Now);
            try
            {
                validationGame.Validate(game);
            }
            catch (ValidationException v)
            {
                Debug.Assert(v.Message.ToString() == "ID shouldn't be less than 0!\n");
            }
        }

        private void TestValidationActivePlayer()
        {
            ValidationActivePlayer validationActivePlayer = new ValidationActivePlayer();

            ActivePlayer activePlayer = new ActivePlayer(1, 1, 2, "das");
            try
            {
                validationActivePlayer.Validate(activePlayer);
            }
            catch (ValidationException v)
            {
                Debug.Assert(v.Message.ToString() == "Type should be Rezerva or Participant!\n");
            }
        }
    }
}
