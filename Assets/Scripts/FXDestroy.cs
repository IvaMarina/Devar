using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXDestroy : MonoBehaviour {
private ParticleSystem ps;
 
 
     public void Start() 
     {
         ps = GameObject.Find("Devar").GetComponent<ParticleSystem>();
     }
 
     public void Update() 
     {
         if(ps)
         {
             if(!ps.IsAlive())
             {
                 Destroy(gameObject);
             }
         }
     }
}
