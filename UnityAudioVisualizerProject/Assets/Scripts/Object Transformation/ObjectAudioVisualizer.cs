using System;
using UnityEngine;

public class ObjectAudioVisualizer : BaseAudioVisualizer
{
    //Scales the hight values by a given factor
    public float scalingFactor = 10000f;

    private readonly Func<float, float, float> heightFunc = (y,s) => y*s;

    //The number of samples used to generate the terrain
    [ValueList(16, 32, 64, 128, 256, 512, 1024, 2048, 4096)]
    public int numberOfSamples = 32;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        
    }

    protected override void ProcessSampleData(float[] sampleData)
    {
        base.ProcessSampleData(sampleData);
        float height=0;
        for(int i = 0; i < sampleData.Length; i++)
        {
            height += sampleData[i]*(i+1f);
        }
        height = 1f + height / sampleData.Length;
        transform.localScale = new Vector3(height, height, height);

    }
}
