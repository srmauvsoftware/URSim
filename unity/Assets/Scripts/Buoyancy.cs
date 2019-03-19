using UnityEngine;


public class Buoyancy : MonoBehaviour
{
	public Rigidbody rb;
	private float force;
	//private float scale = 0.000000001f;
	void Start()
	{
		Mesh mesh = GetComponent<MeshFilter>().sharedMesh;
		rb = GetComponent<Rigidbody> ();
		float volume = VolumeOfMesh(mesh);
		float density = 1;
		float gravity = 9.8f;
		string msg = "The volume of the mesh is " + volume + " cube units.";
		//Debug.Log(msg);
		force = volume * density * gravity;
		Debug.Log (force);

	}


	public float SignedVolumeOfTriangle(Vector3 p1, Vector3 p2, Vector3 p3)
	{
		float v321 = p3.x * p2.y * p1.z;
		float v231 = p2.x * p3.y * p1.z;
		float v312 = p3.x * p1.y * p2.z;
		float v132 = p1.x * p3.y * p2.z;
		float v213 = p2.x * p1.y * p3.z;
		float v123 = p1.x * p2.y * p3.z;
		return (1.0f / 6.0f) * (-v321 + v231 + v312 - v132 - v213 + v123);
	}
	public float VolumeOfMesh(Mesh mesh)
	{
		float volume = 0;
		Vector3[] vertices = mesh.vertices;
		int[] triangles = mesh.triangles;
		for (int i = 0; i < mesh.triangles.Length; i += 3)
		{
			Vector3 p1 = vertices[triangles[i + 0]];
			Vector3 p2 = vertices[triangles[i + 1]];
			Vector3 p3 = vertices[triangles[i + 2]];
			volume += SignedVolumeOfTriangle(p1, p2, p3);
		}
		return Mathf.Abs(volume);
	}

	void FixedUpdate(){
		Vector3 t = Physics.gravity * rb.mass;
		rb.AddForce(-t);
		//Debug.Log (t);
	}


}