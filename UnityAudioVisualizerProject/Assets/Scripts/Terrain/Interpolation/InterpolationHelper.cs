using System;
using UnityEngine;

namespace Assets.Scripts.Terrain.Interpolation
{
    internal class InterpolationHelper
    {
        internal static Func<float, float> GetLinearInterpolationFunction(float p0, float p1, int t)
        {
            float a = (p1 - p0) / t * 1f;
            float b = p0;
            return x => a * x + b;
        }

        internal static Func<float, float> GetQuadraticInterpolationFunction(float p0, float p1, float p2, int t)
        {
            float a = (p2 + p0 - 2 * p1) / (Mathf.Pow(2 * t, 2) - 2 * Mathf.Pow(t, 2));
            float b = (p1 - p0 - a * Mathf.Pow(t, 2)) / t * 1f;
            float c = p0;
            return x => a * Mathf.Pow(x, 2) + b * x + c;
        }

        internal static Func<float, float> GetBezierInterpolationFunction(float p0, float p1, float p2, float p3) => t => Mathf.Pow((1 - t), 3f) * p0 + 3 * Mathf.Pow((1 - t), 2) * t * p1 + 3 * (1 - t) * Mathf.Pow(t, 2) * p2 + Mathf.Pow(t, 3) * p3;
    }
}
