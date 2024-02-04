namespace QuizWebsite.Web.Utilities
{
    public static class HumanizeThatTimeSpan
    {
        public static string ToHumanizedString(this TimeSpan value)
        {
            if (value.Hours != 0)
                return value.ToString(@"hh\:mm\:ss");
            else if (value.Minutes != 0)
                return value.ToString(@"mm\:ss");
            else
                return value.ToString(@"m\:ss");
        }
    }
}
