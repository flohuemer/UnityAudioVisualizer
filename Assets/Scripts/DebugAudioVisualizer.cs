using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugAudioVisualizer : BaseAudioVisualizer
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        // additional startup script
    }

    // Update is called once per frame
    protected override void Update() // usually no need to override this method
    {
        base.Update();
    }

    protected override void ProcessSampleData(float[] sampleData)
    {
        for (int i = 1; i < sampleData.Length - 1; i++)
        {
            Debug.DrawLine(new Vector3(i - 1, sampleData[i] + 10, 0), new Vector3(i, sampleData[i + 1] + 10, 0), Color.red);
            Debug.DrawLine(new Vector3(i - 1, Mathf.Log(sampleData[i - 1]) + 10, 2), new Vector3(i, Mathf.Log(sampleData[i]) + 10, 2), Color.cyan);
            Debug.DrawLine(new Vector3(Mathf.Log(i - 1), sampleData[i - 1] - 10, 1), new Vector3(Mathf.Log(i), sampleData[i] - 10, 1), Color.green);
            Debug.DrawLine(new Vector3(Mathf.Log(i - 1), Mathf.Log(sampleData[i - 1]), 3), new Vector3(Mathf.Log(i), Mathf.Log(sampleData[i]), 3), Color.blue);
        }
    }
}
