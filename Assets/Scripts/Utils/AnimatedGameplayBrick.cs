using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedGameplayBrick : MonoBehaviour
{
	[Header("Following this formula: initialPosition + transform.up * displacementCurve.Evaluate(time / frequency) * intensity")]
    [SerializeField] private AnimationCurve displacementCurve = new AnimationCurve();
	[SerializeField] float intensity = 10;
	[SerializeField] float frequency = 0.5f;


	private Vector3 initialpos;

	private void Awake()
	{
		initialpos = transform.position;
	}

	void Update()
    {
        transform.position = initialpos + transform.up * displacementCurve.Evaluate(Time.time / frequency) * intensity;
    }
}
