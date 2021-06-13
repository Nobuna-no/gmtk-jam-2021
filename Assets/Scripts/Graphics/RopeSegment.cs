using UnityEngine;

[RequireComponent(typeof(HingeJoint2D))]
public class RopeSegment : MonoBehaviour
{
	[SerializeField]
	private new Renderer renderer;
	private new HingeJoint2D hingeJoint;
	private GameObject above;
	private GameObject below;

	public float Height => renderer.bounds.size.y;
	public Rigidbody2D Rigidbody { get; private set; }
	public HingeJoint2D HingeJoint
	{
		get
		{
			if (!hingeJoint)
				hingeJoint = GetComponent<HingeJoint2D>();

			return hingeJoint;
		}
	}

	private void Awake()
	{
		HingeJoint.connectedAnchor = Vector2.zero;
		Rigidbody = HingeJoint.attachedRigidbody;
	}

	public void SetConnection(RopeSegment ropeSegment)
	{
		HingeJoint.connectedBody = ropeSegment.Rigidbody;
		above = ropeSegment.gameObject;
		ropeSegment.below = gameObject;

		HingeJoint.connectedAnchor = new Vector2(0, -Height);
	}

	public void SetHookConnection(Rigidbody2D hook)
	{
		HingeJoint.connectedBody = hook;
	}
}
