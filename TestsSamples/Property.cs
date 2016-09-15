class TimePeriod
{
    private double seconds;

    public double Hours
    {
        private get { return seconds / 3600; }
        private set { seconds = value * 3600; }
    }
}