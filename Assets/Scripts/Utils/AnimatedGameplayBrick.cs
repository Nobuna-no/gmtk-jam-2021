using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedGameplayBrick : MonoBehaviour
{
	[Header("Following this formula: initialPosition + transform.up * displacementCurve.Evaluate(time / frequency) * intensity")]
    [SerializeField] private AnimationCurve displacementCurve = new AnimationCurve();
	[SerializeField] private float intensity = 10;
	[SerializeField] private float frequency = 0.5f;
	[SerializeField] private float duration = 2f;
	[SerializeField] private bool autoPlay = true;
	[SerializeField] private bool _looping = true;
	public bool Looping { get { return _looping; } set { _looping = value; } }

	private Vector3 initialpos;
	private float time;
	private bool isPlaying = false;

	private void Awake()
	{
		initialpos = transform.position;
		time = 0;
		if (autoPlay)
			Play();
	}

	void Update()
    {
		if (isPlaying)
			return;
		time += Time.deltaTime;
		if (time <= duration)
			transform.position = initialpos + transform.up * displacementCurve.Evaluate(time / frequency) * intensity;
		else
		{
			if (_looping)
				time = 0;
			else
				isPlaying = false;
		}
    }

	public void Play()
	{
		isPlaying = true;
	}

}
