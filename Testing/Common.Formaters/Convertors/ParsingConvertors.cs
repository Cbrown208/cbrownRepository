namespace PAS.Pricing.Common.Convertors
{
    public static class ParsingConvertors
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