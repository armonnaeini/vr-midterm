using UnityEngine;

public class MeshDeformerInput : MonoBehaviour {
	
	public float force = 100f;
	public float forceOffset = 10.1f;
	
	void Update () {
		if (Input.GetMouseButton(0)) {
			HandleInput();
		}
	}

	void HandleInput () {
		Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		
		if (Physics.Raycast(inputRay, out hit)) {
			MeshDeformer deformer = hit.collider.GetComponent<MeshDeformer>();
			if (deformer) {
<<<<<<< HEAD
                
=======
>>>>>>> e75924e926c1232da5b626ea2c4391c5770d4b30
				Vector3 point = hit.point;
				point += hit.normal * forceOffset;
				deformer.AddDeformingForce(point, force);
			}
		}
	}
}