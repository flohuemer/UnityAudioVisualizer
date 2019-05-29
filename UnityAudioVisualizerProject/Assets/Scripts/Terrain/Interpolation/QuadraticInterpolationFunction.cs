namespace Assets.Scripts.Terrain.Interpolation
{
    public class QuadraticInterpolationFunction : IInterpolationFunction
    {
        public float[] GetMapData(float[] samples, int outputResolution)
        {
            int multiplier = outputResolution / samples.Length * 2;

            float[] result = new float[samples.Length * multiplier];
            int n = 0;

            for (int i = 2; i < samples.Length; i+=2)
            {
                var quadraticFunction = InterpolationHelper.GetQuadraticInterpolationFunction(samples[i - 2], samples[i - 1], samples[i], multiplier);
                for (int j = 0; j < multiplier * 2; j++)
                {
                    result[n] = quadraticFunction(j);
                    n++;
                }
                n--;
            }

            return result;
        }
    }
}
