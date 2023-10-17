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


    public float test_tilt_angleValue = 0f;
    public float test_pan_angleValue = 0f;
    public float test_tilt_velocityValue = 0f;
    public float test_pan_velocityValue = 0f;
    public float test_targetState = 0f;
    // Start is called before the first frame update
    void Start()
    {
        sourceScript = panTiltCamera.GetComponent<PanTiltController>();
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

        test_targetState = sourceScript.current_target_location;
        targetState.text = test_targetState.ToString();



    }
}
