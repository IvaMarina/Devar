using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour {

	public GameObject DevarFX;
	void OnCollisionEnter(Collision other)
	{
		GameObject o = Instantiate(DevarFX, transform.position, Quaternion.identity, GameObject.Find("ImageTarget").GetComponent<Transform>());
		Vector3 cameraPosition = Camera.main.transform.position;
		cameraPosition.y = o.transform.position.y;
		o.transform.LookAt(cameraPosition);
		Destroy(gameObject);
	}
	
}
