namespace BarcaBot.Infrastructure
{
    public class ToPer90Converter
    {
        private readonly int _minutesPlayed;

        public ToPer90Converter(int minutesPlayed)
        {
            _minutesPlayed = minutesPlayed;
        }
        
        public double ToPer90(int statistic)
        {
            if (_minutesPlayed == 0) return 0;
            return (double) statistic / _minutesPlayed * 90;
        }
    }
}