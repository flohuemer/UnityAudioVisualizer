namespace Assets.Scripts.Terrain.Data
{
    public class TakeEverySecondDataSplitter : IDataSplitter
    {
        public void Split(float[] samples, int numberOfSplitSamples, out float[] xSamples, out float[] ySamples)
        {
            xSamples = new float[numberOfSplitSamples];
            ySamples = new float[numberOfSplitSamples];

            for (int i = 0; i < numberOfSplitSamples * 2; i++)
            {
                if (i % 2 == 0)
                {
                    xSamples[i / 2] = samples[i];
                }
                else
                {
                    xSamples[i / 2] = samples[i];
                }
            }
        }
    }
}
