using Microsoft.AspNetCore.Mvc;

namespace QuizWebsite.Pages
{
    public class AnswerOption
    {
        public long Id { get; set; }

        public string OptionText { get; set; }

        public bool IsCorrect { get; set; }

        public string ResponseValue { get; set; } //TODO: This is some jank. Can probably refactor to be better. - JB

        public bool IsChecked { get { return bool.Parse(ResponseValue); } } //TODO: Related to the jank above. - JB
    }
}