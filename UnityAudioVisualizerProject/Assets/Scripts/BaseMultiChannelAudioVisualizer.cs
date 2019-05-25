using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMultiChannelAudioVisualizer : MonoBehaviour
{
    private float[][] samples;
    private int old_samplesize;
    private int old_channelcount;

    private AudioSource source;

    [ValueList(64, 128, 256, 512, 1024, 2048, 4096, 8192)]
    public int samplesize = 1024;

    public int[] channels;
    public FFTWindow fftWindow = FFTWindow.Rectangular;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        samples = new float[channels.Length][];
        for (int i = 0; i < samples.Length; i++)
        {
            samples[i] = new float[samplesize];
        }
        old_samplesize = samplesize;
        old_channelcount = channels.Length;
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (old_channelcount != channels.Length)
        {
            if (old_channelcount < channels.Length)
            {
                old_samplesize = -1; // use an impossible value here to induce a resizing for the sub-arrays (only when amount of channels gets increased)
            }
            old_channelcount = channels.Length;
            Array.Resize<float[]>(ref samples, channels.Length);
        }
        if (old_samplesize != samplesize) // check if samplesize was changed and recreate samples array accordingly
        {
            old_samplesize = samplesize;

            for (int i = 0; i < samples.Length; i++)
            {
                if (samples[i] == null)
                {
                    samples[i] = new float[samplesize];
                }
                else if(samples[i].Length != samplesize) // only resize when necessary
                {
                    Array.Resize<float>(ref samples[i], samplesize);
                }
            }
        }
        for (int i = 0; i < samples.Length && i < channels.Length; i++)
        {
            source.GetSpectrumData(samples[i], channels[i], fftWindow);
        }
        ProcessSampleData(samples);
    }

    protected virtual void ProcessSampleData(float[][] sampleData)
    {
        // override this method to process sample data
    }
}
