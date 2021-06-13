using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TogglePostProcessShockwave : MonoBehaviour
{
    [SerializeField] Material postProcessMaterial;
    [SerializeField] float duration = 1.5f;

    private float time;

    void Start()
    {
        Vector2 posInScreenRatio = Camera.main.WorldToScreenPoint(transform.position) / new Vector2(Camera.main.scaledPixelWidth, Camera.main.scaledPixelHeight);
        postProcessMaterial.SetVector("_FocalPoint", posInScreenRatio);
        postProcessMaterial.SetFloat("_Percent", 0);
        postProcessMaterial.SetFloat("_Use", 1);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > duration)
            postProcessMaterial.SetFloat("_Use", 0);
        else
            postProcessMaterial.SetFloat("_Percent", time / duration);
    }
}
