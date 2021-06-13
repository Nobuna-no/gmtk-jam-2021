using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeTriggerAmplitude : MonoBehaviour
{

    [SerializeField] private CinemachineVirtualCamera cameraToShake;
    [SerializeField] private float maxAmplitude = 4;
    [SerializeField] private AnimationCurve evolution;
    [SerializeField] private Vector2 minMaxDistance = new Vector2(0, 10);

    private bool active = false;
    private CinemachineBasicMultiChannelPerlin perlinModule;
    private Transform target;
    private float distance;
  

	private void Start()
	{
        cameraToShake = Camera.main.GetComponentInChildren<CinemachineVirtualCamera>();
        perlinModule = cameraToShake?.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

	// Update is called once per frame
	void Update()
    {
        if (!active || !target || !perlinModule)
            return;
         distance = Mathf.Clamp(Vector3.Distance(target.position, transform.position), minMaxDistance.x, minMaxDistance.y);
        perlinModule.m_AmplitudeGain = maxAmplitude * evolution.Evaluate(1 - (distance / minMaxDistance.y));
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Player")
		{
            target = collision.transform;
            active = true;
           
        }
    }

	private void OnTriggerExit2D(Collider2D collision)
	{
        if (collision.tag == "Player")
        {
            target = null;
            active = false;

            perlinModule.m_AmplitudeGain = 0;
            Camera.main.transform.rotation = Quaternion.identity;
        }
    }


}
