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
	[SerializeField] private bool _looping = true;
	public bool Looping { get { return _looping; } set { _looping = value; } }

	private Vector3 initialpos;
	private float time;

	private void Awake()
	{
		initialpos = transform.position;
		time = 0;
	}

	void Update()
    {
		time += Time.deltaTime;
		if (_looping || (!_looping && time <= duration))
			transform.position = initialpos + transform.up * displacementCurve.Evaluate(time / frequency) * intensity;
    }
}
