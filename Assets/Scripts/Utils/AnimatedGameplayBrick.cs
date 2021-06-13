using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimatedGameplayBrick : MonoBehaviour
{
	[Header("Following this formula: initialPosition + transform.up * displacementCurve.Evaluate(time / frequency) * intensity")]
    [SerializeField] private AnimationCurve displacementCurve = new AnimationCurve();
	[SerializeField] private float intensity = 10;
	[SerializeField] private float frequency = 0.5f;
	[SerializeField] private float duration = 2f;
	[SerializeField] private bool autoPlay = true;
	[SerializeField] private bool _looping = true;

	[SerializeField] private float delayBeforeStart = 0;

	public bool Looping { get { return _looping; } set { _looping = value; } }

	private Vector3 initialpos;
	private float time;
	private bool isPlaying = false;
	private bool isTimeFrequencyCheck = false;

	[SerializeField] private UnityEvent onPlayStart;
	[SerializeField] private UnityEvent onPlayStop;

	private void Awake()
	{
		initialpos = transform.position;
		time = 0;
		if (autoPlay)
			StartCoroutine(PlayDelayed());
	}

	void Update()
    {
		if (!isPlaying)
		{
			return;
        }

		time += Time.deltaTime;
		if (time <= duration)
		{
			transform.position = initialpos + transform.up * displacementCurve.Evaluate(time / frequency) * intensity;

			if (_looping && !isTimeFrequencyCheck && time >= frequency)
            {
				isTimeFrequencyCheck = true;
				onPlayStop?.Invoke();
			}
		}
		else
		{
			if (_looping)
			{
				isTimeFrequencyCheck = false;
				onPlayStart?.Invoke();
				time = 0;
			}
			else
			{
				onPlayStop?.Invoke();
				isPlaying = false;
			}
		}
    }

	private IEnumerator PlayDelayed()
	{
		yield return new WaitForSeconds(delayBeforeStart);
		Play();
	}

	public void Play()
	{
		onPlayStart?.Invoke();
		isPlaying = true;
	}

}
