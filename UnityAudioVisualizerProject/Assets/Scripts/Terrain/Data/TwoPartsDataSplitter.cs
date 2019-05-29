namespace Assets.Scripts.Terrain.Data
{
    public class TwoPartsDataSplitter : IDataSplitter
    {
        public void Split(float[] samples, int numberOfSplitSamples, out float[] xSamples, out float[] ySamples)
        {
            xSamples = new float[numberOfSplitSamples];
            ySamples = new float[numberOfSplitSamples];

            for (int i = 0; i < numberOfSplitSamples; i++)
            {
                xSamples[i] = samples[i];
            }

            for (int i = numberOfSplitSamples; i < numberOfSplitSamples * 2; i++)
            {
                ySamples[i - numberOfSplitSamples] = samples[i];
            }
        }
    }
}

