namespace Blog.Api.Extensions
{
    public static class StringExtensions
    {

        public static bool NotNullOrEmpty(string str)
        {
            return !string.IsNullOrEmpty(str);
        }
    }
}
