using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapToMove : MonoBehaviour {
 private bool move = false, attack = false, tap = false, iniattack = false;
 //destination point
 private Vector3 endPoint;
 public float speed = 50.0f; 
 private float tapTimer = 0.0f;
 public float tapThreshold = 0.25f;
 Animator anim;
 void Start()
	{
		anim = GetComponentInChildren<Animator>();
	}
   
 void Update () {
 
  if((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) || (Input.GetMouseButtonDown(0)))
  	{
   		RaycastHit hit;
   		Ray ray = new Ray();
		#if UNITY_EDITOR
		ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		#elif (UNITY_ANDROID)
		ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
		#endif
		if(Time.time < tapTimer + tapThreshold)
		{
			if(Physics.Raycast(ray,out hit))
			{
				endPoint = hit.point;
				transform.LookAt(endPoint);
				anim.SetBool("walking", false);
				anim.SetTrigger("attacking");
				move = false;
				attack = true;
				tap = false;
			}  
		}
		else
			tap = true;
		if(tap && Time.time > tapTimer + tapThreshold){
			tap = false;
			if(Physics.Raycast(ray,out hit))
			{
				endPoint = hit.point;
				move = true;
				transform.LookAt(endPoint);
				anim.SetBool("walking", true);
			}
		}
		tapTimer = Time.time;
  }
  if(move && Time.time > tapTimer + tapThreshold && Vector3.Distance(transform.position, endPoint) > 0.05f){ 
		transform.position = Vector3.MoveTowards(transform.position, endPoint, speed * Time.deltaTime);
  }
  else if(move && Vector3.Distance(transform.position, endPoint) < 0.05f){//Mathf.Approximately(transform.position.magnitude, endPoint.magnitude)) {
	  	anim.SetBool("walking", false);
   		move = false;
  } 
  if(attack)
  {
	  if(!iniattack){
		gameObject.GetComponentInChildren<SpawnProjectiles>().SpawnVFX(endPoint);
		iniattack = true;
	  }
	  else{
		  if(Time.time > tapTimer + 2f)
			attack = gameObject.GetComponentInChildren<SpawnProjectiles>().MoveVFX();
	  	}
  }
  else
	{
		iniattack = false;
	}
 }
}
