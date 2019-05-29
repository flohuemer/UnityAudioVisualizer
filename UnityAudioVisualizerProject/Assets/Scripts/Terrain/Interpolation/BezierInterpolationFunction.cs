using System;

namespace Assets.Scripts.Terrain.Interpolation
{
    public class BezierInterpolationFunction : IInterpolationFunction
    {
        public float[] GetMapData(float[] samples, int outputResolution)
        {
            int multiplier = outputResolution / samples.Length * 2;
            int startIndex = 0;
            float slope = samples[startIndex + 1] - samples[startIndex];
            float previousSlope = slope / Math.Abs(slope);

            float[] result = new float[samples.Length * multiplier];

            int n = 0;

            for (int i = 1; i < samples.Length; i++)
            {
                slope = samples[i] - samples[i - 1];
                if (slope != 0)
                {
                    slope /= Math.Abs(slope);
                }
                if (slope != previousSlope)
                {
                    int endIndex = i;
                    //Bezier Function
                    float p0 = samples[startIndex];
                    float p1 = samples[startIndex + 1];
                    float p2 = samples[endIndex - 1];
                    float p3 = samples[endIndex];
                    var bezier = InterpolationHelper.GetBezierInterpolationFunction(p0, p1, p2, p3);
                    int difference = endIndex - startIndex;

                    //Correction Function
                    float diff = difference * (float)multiplier;
                    float valueDifference = 0;
                    if (startIndex > 0)
                    {
                        valueDifference = samples[startIndex] - samples[startIndex + 1];
                    }
                    for (int j = 0; j < diff; j++)
                    {
                        float y = bezier(j / diff) - (diff - j) / diff * valueDifference;
                        result[n] = y;
                        n++;
                        if (n == samples.Length * multiplier)
                        {
                            return result;
                        }
                    }
                    startIndex = endIndex - 1;
                }
            }

            return result;
        }
    }
}
