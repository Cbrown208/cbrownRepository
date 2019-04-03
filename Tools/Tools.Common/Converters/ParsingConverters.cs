namespace Tools.Common.Converters
{
    public static class ParsingConverters
    {
        public static int GetIntParse(string value)
        {
            if (value == null)
                return -1;

            int result;
            int.TryParse(value, out result);
            return result;
        }
    }
}