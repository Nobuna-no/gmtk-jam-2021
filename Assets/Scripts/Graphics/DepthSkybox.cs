using UnityEngine;

public class DepthSkybox : MonoBehaviour
{
	public float minY = 0.0f;
	public float maxY = 100.0f;

	private void Update()
	{
		float depth = Mathf.Clamp01(Mathf.InverseLerp(minY, maxY, transform.position.y));
		RenderSettings.skybox.SetFloat("Depth", depth);
	}
}
