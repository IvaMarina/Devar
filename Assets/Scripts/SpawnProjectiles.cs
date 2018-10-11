using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnProjectiles : MonoBehaviour {
	public List<GameObject> vfx = new List<GameObject>();
	private GameObject effectToSpawn;
	public float speedattack = 2f;
	GameObject o;
	bool isParent = false;
	Vector3 endPoint;

	void Start(){
		effectToSpawn = vfx[0];
	}

	
	public void SpawnVFX(Vector3 target){
		if(transform!=null){
			o = Instantiate( effectToSpawn, transform.position + new Vector3(0,0.08f,0), Quaternion.identity, transform);
			o.transform.LookAt(target);
			endPoint = target;
			isParent = false;
		}
	}

	public bool MoveVFX(){
		if(o != null){
			if(isParent)
				o.transform.position = Vector3.MoveTowards(o.transform.position, endPoint, speedattack * Time.deltaTime);
			else
			{
				o.transform.SetParent(GameObject.Find("ImageTarget").GetComponent<Transform>());
				isParent = true;
			}
			return true;
		}
		else return false;
	}

}
