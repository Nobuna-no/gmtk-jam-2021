using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OpenerHook : MonoBehaviour
{
    public UnityEvent OnHookUsed;
    public UnityEvent OnCollision;

    [SerializeField] private float thresholdActivation = 3f;

    private Vector3 statingPos;
    private Transform _target;
    private bool _active = false;
    void Awake()
    {
        statingPos = transform.position;
    }

    private void OnEnable()
    {
        if (_active)
        {
            _active = false;
            transform.position = statingPos;
        }
    }

	void Update()
    {
        if (_target != null)
            transform.position = _target.position;
    }

    public void ActivateCollision()
	{
        _active = true;
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
        if (_active)
            OnCollision?.Invoke();
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
