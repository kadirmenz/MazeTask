using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Movement : MonoBehaviour
{
   public GameObject myTarget;
    void Start()
    {
        
       
    }

    // Update is called once per frame
     void Update()
    {
       if (Input.GetMouseButtonDown(0)) {
             Vector2 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
             myTarget.transform.position=target;
         }
    }
   
   
}
