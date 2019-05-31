using System;
using UnityEngine;

public class ObjectAudioVisualizer : BaseAudioVisualizer
{
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
