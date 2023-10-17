using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class ValueFinder : MonoBehaviour
{
    //G: add TMP game objects for each value
    public TextMeshProUGUI tiltAngleValue;
    public TextMeshProUGUI PanAngleValue;
    public TextMeshProUGUI tiltVelocityValue;
    public TextMeshProUGUI panVelocityValue;
    public TextMeshProUGUI targetState;
    
    
    
    //This is where we pull the current values from
    public GameObject panTiltCamera;
    private PanTiltController sourceScript;

    public GameObject painholeCamera;
    private CameraFOV sourceScript2;
   


    public float test_tilt_angleValue = 0f;
    public float test_pan_angleValue = 0f;
    public float test_tilt_velocityValue = 0f;
    public float test_pan_velocityValue = 0f;
    public bool test_target_visibility = false;
    public Vector3 test_target_position = Vector3.zero;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        sourceScript = panTiltCamera.GetComponent<PanTiltController>();
        sourceScript2 = painholeCamera.GetComponent<CameraFOV>();
    }

    // Update is called once per frame
    void Update()
    {
        test_tilt_angleValue = sourceScript.currentTiltAngle;
        tiltAngleValue.text = test_tilt_angleValue.ToString();

        test_pan_angleValue = sourceScript.currentPanAngle;
        PanAngleValue.text = test_pan_angleValue.ToString();

        test_tilt_velocityValue = sourceScript.current_tilt_velocity;
        tiltVelocityValue.text = test_tilt_velocityValue.ToString();

        test_pan_velocityValue = sourceScript.current_pan_velocity;
        panVelocityValue.text = test_pan_velocityValue.ToString();

        test_target_visibility = sourceScript2.target_visibility;
        if (test_target_visibility)
        {
            targetState.text = ("TRUE - Position: " + sourceScript2.target_position.ToString());
        }
        else
        {
            targetState.text = "FALSE";
        }



    }
}
