using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleSwitch : MonoBehaviour
{
    [SerializeField] private Toggle a;
    [SerializeField] private Toggle b;

    public bool IsA { get { return a.isOn; } }
    public bool IsB { get { return b.isOn; } }

	private void Awake()
	{
		a?.onValueChanged.AddListener(delegate
		{
			OnValueChange("a");
		});

		b?.onValueChanged.AddListener(delegate
		{
			OnValueChange("b");
		});
	}

	private void OnValueChange(string toogleName)
	{
		switch(toogleName)
		{
			case "a":
				b.isOn = !a.isOn;
				break;
			case "b":
				a.isOn = !b.isOn;
				break;
		}

	}
}
