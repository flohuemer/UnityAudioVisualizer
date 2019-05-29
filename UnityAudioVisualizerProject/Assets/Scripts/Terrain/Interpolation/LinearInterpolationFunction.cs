namespace Assets.Scripts.Terrain.Interpolation
{
    public class LinearInterpolationFunction : IInterpolationFunction
    {
        public float[] GetMapData(float[] samples, int outputResolution)
        {
            int multiplier = outputResolution / samples.Length * 2;

            float[] result = new float[samples.Length * multiplier];
            int n = 0;

            for (int i = 1; i < samples.Length; i++)
            {
                var linearFunction = InterpolationHelper.GetLinearInterpolationFunction(samples[i - 1], samples[i], multiplier);
                for (int j = 0; j < multiplier; j++)
                {
                    result[n] = linearFunction(j);
                    n++;
                }
                n--;
            }

            return result;
        }
    }

}
