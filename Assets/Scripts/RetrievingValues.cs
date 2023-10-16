using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RetrievingValues : MonoBehaviour
{
    // [SerializeField] public PanTiltCamera script;
    
    // public Text PanAngle;
    // public Text TiltAngle;

    public int test = 10;
   
    public Text ValueText;


    private void StoreValue()
    {

        // int Value1 = Debug.Log(script.panRotation);
        // int Value2 = Debug.Log(script.currentTiltAngle);

        // PanAngle.text = Value1.ToString();
        // TiltAngle.text = Value2.ToString();

        ValueText.text = test.ToString();

       

    }

}
