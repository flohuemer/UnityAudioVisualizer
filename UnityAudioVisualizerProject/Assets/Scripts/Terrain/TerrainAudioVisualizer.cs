using Assets.Scripts.Terrain.Data;
using Assets.Scripts.Terrain.Interpolation;
using System;
using UnityEngine;

[RequireComponent(typeof(Terrain))]
public class TerrainAudioVisualizer : BaseAudioVisualizer
{
    //Scales the hight values by a given factor
    public float scalingFactor = 1f;

    //The number of samples used to generate the terrain
    [ValueList(16,32,64, 128, 256, 512, 1024, 2048, 4096)]
    public int numberOfSamples = 32;

    public SplitStrategy splitStrategie;
    public InterpolationStrategy interpolationStrategy;

    private TerrainData terrainData;
    private float[,] terrainHeights;
    private int terrainResolution = 0;

    private readonly Func<float, float, float, float> heightFunc = (x, y, s) => (x + y * 2f) * 200f * s / 100;
    private IDataSplitter splitter;
    private IInterpolationFunction interpolationFunction;


    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        //Initialize terrain data
        terrainData = GetComponent<Terrain>().terrainData;
        terrainResolution = terrainData.detailResolution / 2;
        terrainHeights = new float[terrainResolution, terrainResolution];

        //Initialize TerrainLayers for later texturing
        terrainData.terrainLayers = new TerrainLayer[]
        {
            new TerrainLayer()
        };

        SetConfiguration();
    }

    protected override void ProcessSampleData(float[] sampleData)
    {
        base.ProcessSampleData(sampleData);

        SplitSamplesIntoDataSets(sampleData, out float[] xDataSet, out float[] yDataSet);
        GetMapData(xDataSet, yDataSet, out float[] xMapData, out float[] yMapData);
        SetHeightsAndColors(xMapData, yMapData);

        //For testing and presentation purposes
        SetConfiguration();
    }

    private void SetConfiguration()
    {
        switch (splitStrategie)
        {
            case SplitStrategy.TakeEverySecond:
                splitter = new TakeEverySecondDataSplitter();
                break;
            case SplitStrategy.TwoParts:
                splitter = new TwoPartsDataSplitter();
                break;
            case SplitStrategy.Interval:
                splitter = new IntervalDataSplitter();
                break;
            default:
                splitter = new TwoPartsDataSplitter();
                break;
        }

        switch (interpolationStrategy)
        {
            case InterpolationStrategy.Linear:
                interpolationFunction = new LinearInterpolationFunction();
                break;
            case InterpolationStrategy.Quadratic:
                interpolationFunction = new QuadraticInterpolationFunction();
                break;
            case InterpolationStrategy.Bezier:
                interpolationFunction = new BezierInterpolationFunction();
                break;
            default:
                interpolationFunction = null;
                break;
        }
    }

    private void SplitSamplesIntoDataSets(float[] samples, out float[] xSamples, out float[] ySamples) => splitter.Split(samples, numberOfSamples, out xSamples, out ySamples);

    private void GetMapData(float[] xDataSet, float[] yDataSet, out float[] xMapData, out float[] yMapData)
    {
        if (interpolationFunction != null)
        {
            xMapData = interpolationFunction.GetMapData(xDataSet, terrainResolution / 2);
            yMapData = interpolationFunction.GetMapData(yDataSet, terrainResolution / 2);
        }
        else
        {
            xMapData = xDataSet;
            yMapData = yDataSet;
        }
    }

    private void SetHeightsAndColors(float[] xMapData, float[] yMapData)
    {
        //Create texture
        var terrainTexture = new Texture2D(terrainData.heightmapWidth, terrainData.heightmapHeight);

        for (int i = 0; i < xMapData.Length; i++)
        {
            for (int j = 0; j < yMapData.Length; j++)
            {
                float height = heightFunc(xMapData[i], yMapData[j], scalingFactor);
                terrainHeights[i, j] = height;
                var color = new Color(height + 0.2f, height * 8f + 0.5f, 0.3f);
                terrainTexture.SetPixel(j, i, color);
            }
        }

        //Set texture
        terrainData.SetHeights(0, 0, terrainHeights);
        terrainTexture.name = "MapColor";
        terrainTexture.Apply(true, false);
        terrainData.terrainLayers[0].diffuseTexture = terrainTexture;
        terrainData.terrainLayers[0].tileSize = new Vector2(terrainData.heightmapHeight * 2, terrainData.heightmapWidth * 2);

    }
}
