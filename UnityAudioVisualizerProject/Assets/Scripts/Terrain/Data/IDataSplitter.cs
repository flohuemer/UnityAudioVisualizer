namespace Assets.Scripts.Terrain.Data
{
    public interface IDataSplitter
    {
        void Split(float[] samples, int numberOfSplitSamples, out float[] xSamples, out float[] ySamples);
    }
}
