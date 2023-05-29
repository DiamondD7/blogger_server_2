using System.ComponentModel.DataAnnotations;

namespace bloggerServer.Model
{
    public class SecurityQuestion
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }

        [Required]
        public string SecurityQuestionOne { get; set; }
        [Required]
        public string SecurityQuestionTwo { get; set; }
        [Required]
        public string SecurityQuestionThree { get; set; }



        [Required]
        private string SecurityAnswerOne { get; set; }

        [Required]
        private string SecurityAnswerTwo { get; set; }

        [Required]
        private string SecurityAnswerThree { get; set; }



        public bool IsAuthorize(string AnswerOne, string AnswerTwo, string AnswerThree)
        {
            if (AnswerOne == SecurityAnswerOne && AnswerTwo == SecurityAnswerTwo && AnswerThree == SecurityAnswerThree)
            {
                return true;
            }
            return false;
        }

        public void GetValues(string quesFirst, string quesSecond, string quesThird)
        {
            SecurityAnswerOne = quesFirst;
            SecurityAnswerTwo = quesSecond;
            SecurityAnswerThree = quesThird;
        }
    }
}
