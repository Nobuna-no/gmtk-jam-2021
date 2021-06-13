using UnityEngine;

public class RigidbodyProxy : MonoBehaviour
{
	public Rigidbody2D original;
	private Rigidbody2D proxy;

	private void Start()
	{
		proxy = GetComponent<Rigidbody2D>();
	}

	private void FixedUpdate()
	{
		proxy.position = original.position;
	}
}
