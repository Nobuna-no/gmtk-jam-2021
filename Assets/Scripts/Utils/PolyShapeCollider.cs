#if UNITY_EDITOR
using UnityEditor.Experimental.SceneManagement;
using UnityEditor.SceneManagement;
#endif
using UnityEngine;
using UnityEngine.ProBuilder;
using UnityEngine.ProBuilder.MeshOperations;

[DisallowMultipleComponent]
public class PolyShapeCollider : MonoBehaviour
{
	public float width = 2.0f;

#if UNITY_EDITOR
	[ContextMenu("Setup")]
    void Setup()
	{
		gameObject.name = "Wall";
		transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0.0f);

		MeshCollider meshCollider = GetComponent<MeshCollider>();
		if (meshCollider)
			DestroyImmediate(meshCollider);

		PolyShape polyShape = GetComponent<PolyShape>();
		if (!polyShape)
			return;

		polyShape.extrude = width;
		AppendElements.CreateShapeFromPolygon(polyShape);

		PolygonCollider2D collider2D = GetComponentInChildren<PolygonCollider2D>();
		if (!collider2D)
		{
			GameObject child = new GameObject("collision", typeof(PolygonCollider2D));
			child.transform.parent = transform;
			child.transform.localPosition = Vector3.zero;
			child.transform.localEulerAngles = new Vector3(90.0f, 0.0f, 0.0f);
			child.transform.localScale = Vector3.one;
			collider2D = child.GetComponent<PolygonCollider2D>();
		}

		Vector2[] points = new Vector2[polyShape.controlPoints.Count];
		for (int i = 0; i < polyShape.controlPoints.Count; ++i)
		{
			Vector3 polyPoint = polyShape.controlPoints[i];
			points[i] = new Vector2(polyPoint.x, polyPoint.z);
		}
		
		collider2D.SetPath(0, points);

		var prefabStage = PrefabStageUtility.GetPrefabStage(gameObject);
		if (prefabStage != null)
		{
			EditorSceneManager.MarkSceneDirty(prefabStage.scene);
		}
#endif
	}
}
