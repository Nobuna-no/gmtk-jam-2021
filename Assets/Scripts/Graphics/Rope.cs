using UnityEngine;

public class Rope : MonoBehaviour
{
	public Rigidbody2D hook;
	public HingeJoint2D end;
	public RopeSegment ropeSegmentPrefab;
	public int numLinks = 5;
	
    void Start()
	{
		RopeSegment prevSegment = null;
		for (int i = 0; i < numLinks; ++i)
		{
			RopeSegment newSegment = Instantiate(ropeSegmentPrefab);
			newSegment.transform.parent = transform;
			newSegment.transform.position = transform.position;

			if (prevSegment)
				newSegment.SetConnection(prevSegment);
			else
				newSegment.SetHookConnection(hook);

			prevSegment = newSegment;
		}

		if (prevSegment)
		{
			end.connectedBody = prevSegment.Rigidbody;
			end.connectedAnchor = new Vector2(0.0f, -prevSegment.Height);
		}
	}
}
