namespace Assets.Scripts.Terrain.Interpolation
{
    public interface IInterpolationFunction
    {
        float[] GetMapData(float[] samples, int outputResolution);
    }
}
