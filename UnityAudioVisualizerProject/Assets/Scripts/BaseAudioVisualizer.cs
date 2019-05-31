using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BaseAudioVisualizer : MonoBehaviour
{
    private float[] samples;
    private int old_samplesize;

    private AudioSource source;

    [ValueList(64, 128, 256, 512, 1024, 2048, 4096, 8192)]
    public int samplesize = 1024;

    public int channel = 0;
    public FFTWindow fftWindow = FFTWindow.Rectangular;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        samples = new float[samplesize];
        old_samplesize = samplesize;
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (old_samplesize != samplesize) // check if samplesize was changed and recreate samples array accordingly
        {
            old_samplesize = samplesize;
            samples = new float[samplesize];
        }

        source.GetSpectrumData(samples, channel, fftWindow);
        ProcessSampleData(samples);
    }

    protected virtual void ProcessSampleData(float[] sampleData)
    {
        // override this method to process sample data
    }
}
