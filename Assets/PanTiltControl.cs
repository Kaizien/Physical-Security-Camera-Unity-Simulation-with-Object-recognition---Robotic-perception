using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
 public class PanTiltControl : MonoBehaviour
 {
     public Transform panJoint;
     public Transform tiltJoint;
     public float panSpeed = 30f;
     public float tiltSpeed = 30f;
 
     void Update()
     {
         //horizontal rotation
         float pan = Input.GetAxis("Horizontal") * panSpeed * Time.deltaTime;
         //vertical location
         float tilt = Input.GetAxis("Vertical") * tiltSpeed * Time.deltaTime;
        
         panJoint.Rotate(Vector3.up, pan);
         tiltJoint.Rotate(Vector3.left, tilt);
     }
 }
