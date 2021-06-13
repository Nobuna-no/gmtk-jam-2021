using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class SinLightIntensity : MonoBehaviour
{
    Light light;
    [SerializeField] private float baseIntensity = 10;
    float time = 0;
    // Start is called before the first frame update
    void OnEnable()
    {
        light = GetComponent<Light>();
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        light.intensity = Mathf.Sin(time) * baseIntensity;
    }
}
