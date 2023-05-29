using System.ComponentModel.DataAnnotations;

namespace bloggerServer.Model
{
    public class SecurityQuestion
    {
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
    }
}
