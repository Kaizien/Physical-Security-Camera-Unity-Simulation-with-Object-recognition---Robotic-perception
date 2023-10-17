using System;
using Unity.VisualScripting;
using UnityEngine;


public class PanTiltController : MonoBehaviour
{
    public Transform panJoint; // Drag the PanJoint GameObject here in the inspector
    public Transform tiltObject; // Drag the Pan (cube) GameObject here in the inspector
    public float maxPanAngularVelocity = 100f;  // Degrees per second per the research paper provided
    public float maxTiltAngularVelocity = 100f;  // Degrees per second
    public float motorCoefficient = 100f; // degrees per (V * s^2). Assuming b1 and b2 are equal.
    public float maxTilt = 90f;   // Maximum tilt angle (upwards)
    public float minTilt = -90f;  // Minimum tilt angle (downwards)
    public float calc_FOV = 0;    //calculated FOV
    public float currentTiltAngle;
    public float currentPanAngle;
    public float panRotation;
    public float current_pan_velocity;
    public float current_tilt_velocity;
   
    

    

    public float voltage_input = 0f;
    
    void Update()
    {
        voltage_input = GetVoltage();  //this function always returns 24V right now.
        //float timeSinceStart = Time.time; // Time since the start of the game in seconds

        // Adjust pan and tilt speed based on motor coefficients
        
        // Adjust pan speed linearly based on voltage, such that max speed is achieved at 24V
        float adjustedPanSpeed = maxPanAngularVelocity * (voltage_input / 24f);
        // Adjust tilt speed linearly based on voltage, such that max speed is achieved at 24V
        float adjustedTiltSpeed = maxTiltAngularVelocity * (voltage_input / 24f);
    
        panRotation = Input.GetAxis("Horizontal") * adjustedPanSpeed * Time.deltaTime;
        current_pan_velocity = Mathf.Clamp(Mathf.Abs(panRotation)*adjustedPanSpeed, 0, 100); //calculate the current pan speed for the display
        float tiltRotation = Input.GetAxis("Vertical") * adjustedTiltSpeed * Time.deltaTime; 
        current_tilt_velocity = Mathf.Clamp(Mathf.Abs(tiltRotation)*adjustedTiltSpeed, 0, 100); //calculate current tilt speed for the display
        // Apply pan rotation to PanJoint
        panJoint.Rotate(Vector3.up, panRotation); 
    
        // Calculate the new tilt angle based on current angle and input
        currentTiltAngle = tiltObject.localEulerAngles.x;
        float newTiltAngle = currentTiltAngle - tiltRotation; 

        // Convert to [-180, 180] range for correct clamping
        if (newTiltAngle > 180) newTiltAngle -= 360;

        // Clamp the tilt angle
        newTiltAngle = Mathf.Clamp(newTiltAngle, -90, 90);

        // Apply the clamped tilt rotation to TiltObject
        tiltObject.localEulerAngles = new Vector3(newTiltAngle, 0, 0);
        currentTiltAngle = newTiltAngle;
        currentPanAngle = panJoint.localEulerAngles.y;
        
    

    }


    float GetVoltage()
    {
        //We can add some logic here for varying voltage, but I think we can just assume its the max voltage allowed for
        //the sensor we're trying to emulate. 
        
        //Typical pan/tilt camera systems, motors, etc. require 12v or 24v, so we'll use 12V
        return (24.0f);
    }

    /*float  CalculateFOV(float sensor_x, float focal_length)
    {
        float fov_horizontal_new = 0.0f;

        fov_horizontal_new = 2 * MathF.
            
            (MathF.Atan((sensor_x / (2 * focal_length)));
    
        return (fov_horizontal_new);
    }
    
    
    
    float Estimation_Theoretic_Approach(Vector3 target_pos, float fl_y, float fl_x, float skew, float cx, float cy)
    {
        float eta = 0.0f;
        return (eta);
    }
    */

}