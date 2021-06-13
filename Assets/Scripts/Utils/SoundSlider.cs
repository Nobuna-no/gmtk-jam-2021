using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SoundSlider : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private string floatName;

    private Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        float f;
		mixer.GetFloat(Mathf.exfloatName, out f);
        slider.value = f;
    }

    public void OnValueChanged()
	{
        mixer.SetFloat(floatName, Mathf.Log10(slider.value) *20);
	}
}
