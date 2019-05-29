namespace Assets.Scripts.Terrain.Data
{
    public enum SplitStrategy
    {
        TwoParts, //first z samples into x, second z into y
        TakeEverySecond, //Odd index values into x, even into y
        Interval //Take one sample every a steps and put it into x or y, the first z get into x the second z into y 
    }
}
