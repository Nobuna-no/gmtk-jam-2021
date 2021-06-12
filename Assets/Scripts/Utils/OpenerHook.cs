using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OpenerHook : MonoBehaviour
{
    public UnityEvent OnHookUsed;

    [SerializeField] private float thresholdActivation = 3f;

    private Vector3 statingPos;
    private Transform _target;
    void Start()
    {
        statingPos = transform.position;
    }

    void Update()
    {
        if (_target != null)
            transform.position = _target.position;
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Mouth")
            _target = collision.transform;

	}

	private void OnTriggerExit2D(Collider2D collision)
	{
        if (collision.tag == "Mouth")
		{
            _target = null;
            if (Vector3.Distance(statingPos, transform.position) > thresholdActivation)
                OnHookUsed?.Invoke();
		}
    }
}
