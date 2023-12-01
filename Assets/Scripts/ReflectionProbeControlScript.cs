using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class ReflectionProbeControlScript : MonoBehaviour
{
    private ReflectionProbe rProbe;
    public float renderFrequency;
    [SerializeField] float freqOff;
    [SerializeField] float freqLow;
    [SerializeField] float freqHigh;
    private float lastRenderTime;
    void Start()
    {
        rProbe = GetComponent<ReflectionProbe>();

        switch (QualitySettings.GetQualityLevel())
        {
            case 0:
                renderFrequency = freqOff;
                break;
            case 1:
                renderFrequency = freqLow;
                break;
            case 2:
                renderFrequency = freqHigh;
                break;
        }
        if (renderFrequency == freqOff)
        {
            rProbe.RenderProbe();
        }
    }
    private void LateUpdate() 
    {
        if (renderFrequency != freqOff)
        {
            if (Time.time > lastRenderTime + renderFrequency)
             {
                 rProbe.RenderProbe();
                lastRenderTime = Time.time;
             }
        }
    }
}