using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OpenerHook : MonoBehaviour
{
    public UnityEvent OnHookUsed;
    public UnityEvent OnCollision;

    [SerializeField] private float thresholdActivation = 3f;
    [SerializeField] private Material activatedMaterial;
    [SerializeField] private GameObject holdFeedback;

    private Vector3 statingPos;
    private Transform _target;
    private bool _active = false;
    private Material originalMaterial;
    private MeshRenderer meshRenderer;

	public float DistanceThreshold => thresholdActivation;

	void Awake()
    {
        statingPos = transform.position;
        meshRenderer = GetComponent<MeshRenderer>();
        originalMaterial = meshRenderer?.material;
    }

    private void OnEnable()
    {
        holdFeedback?.SetActive(false);
        if (_active)
        {
            _active = false;
            transform.position = statingPos;
            if (meshRenderer != null) meshRenderer.material = originalMaterial;
        }
    }

	void Update()
    {
        if (_target != null)
            transform.position = _target.position;
    }

    public void ActivateCollision()
	{
        StartCoroutine(DelayedActivate());
	}

    private IEnumerator DelayedActivate()
	{
        yield return new WaitForSeconds(0.5f);
        _active = true;
        if (meshRenderer != null) meshRenderer.material = activatedMaterial;
    }

    private void OnCollisionEnter2D(Collision2D collision)
	{
        if (_active)
            OnCollision?.Invoke();
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
        if (collision.tag == "Mouth")
        {
            holdFeedback?.SetActive(true);
            _target = collision.transform;
        }

	}

	private void OnTriggerExit2D(Collider2D collision)
	{
        if (collision.tag == "Mouth")
		{
            _target = null;
            holdFeedback?.SetActive(false);
            if (Vector3.Distance(statingPos, transform.position) > thresholdActivation)
                OnHookUsed?.Invoke();
		}
    }
}
