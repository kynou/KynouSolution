namespace Kynou.Helpers
{
    public static class StringHelper
    {
        public static string GenerateTimestamp()
        {
            var d = DateTime.Now;

            return $"{d.Year}_{d.Month}_{d.Day}_{d.Hour}_{d.Minute}_{d.Second}_{d.Millisecond}";
        }
    }
}
