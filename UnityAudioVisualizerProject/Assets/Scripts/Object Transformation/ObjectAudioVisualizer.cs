using UnityEngine;

public class ObjectAudioVisualizer : BaseAudioVisualizer
{
    //gives the minSize of the Cube
    public float minSize = 1f;
    public float maxSize = 5f;

    // Start is called before the first frame update
    protected override void Start() => base.Start();

    // Update is called once per frame
    protected override void Update() => base.Update();

    protected override void ProcessSampleData(float[] sampleData)
    {
        base.ProcessSampleData(sampleData);
        float height=0;
        float max = 0;
        int indexOfMax = 0;
        for(int i = 0; i < sampleData.Length; i++)
        {
            //find the highest amplitude
            if (sampleData[i] > max)
            {
                max = sampleData[i];
                indexOfMax = i;
            }
            //calculate the sum of all amplitudes
            height += sampleData[i];
        }
        //height shows the average amplitude of the sample
        height = minSize + (maxSize-minSize)*(height / samplesize);
        //width shows the Frequenz with the highest amplitude of the sample
        float width = minSize + indexOfMax * (maxSize - minSize) / samplesize;
        //depth shows the highest amplitude of the sample
        float depth = minSize + (maxSize - minSize) * max;
        transform.localScale = new Vector3(width, height, depth);

    }
}
