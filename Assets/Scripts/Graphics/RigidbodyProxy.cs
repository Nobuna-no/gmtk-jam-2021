using UnityEngine;

public class RigidbodyProxy : MonoBehaviour
{
	public Rigidbody2D original;
	private Rigidbody2D proxy;
	private Vector2 offset;

	private void Start()
	{
		proxy = GetComponent<Rigidbody2D>();
		offset = proxy.position - original.position;
	}

	private void FixedUpdate()
	{
		proxy.position = original.position + offset;
	}
}
