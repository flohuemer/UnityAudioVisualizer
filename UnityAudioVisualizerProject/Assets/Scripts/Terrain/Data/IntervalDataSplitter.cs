namespace Assets.Scripts.Terrain.Data
{
    public class IntervalDataSplitter : IDataSplitter
    {
        public void Split(float[] samples, int numberOfSplitSamples, out float[] xSamples, out float[] ySamples)
        {
            xSamples = new float[numberOfSplitSamples];
            ySamples = new float[numberOfSplitSamples];
            int dataPointsBetweenSamples = samples.Length / numberOfSplitSamples;
            int k = 0;
            for (int i = 0; i < samples.Length; i += dataPointsBetweenSamples)
            {
                if (k % 2 == 0)
                {
                    xSamples[k / 2] = samples[i];
                }
                else
                {
                    ySamples[k / 2 + 1] = samples[i];
                }
                k++;
            }
        }
    }
}
