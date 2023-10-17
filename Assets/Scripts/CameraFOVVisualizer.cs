using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class CameraFOV : MonoBehaviour
{
    public Camera referenceCamera;
    public Material frustumMaterial;  
    public Material target_Found_Mat;
    public Material target_Missing_Mat;
    public GameObject target;

    private bool target_visibility = false;
    void Start()
    {
        


       /* MeshFilter meshFilter = GetComponent<MeshFilter>();
        meshFilter.mesh = CreateFrustumMesh(referenceCamera);
        
        // Set the material
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        renderer.material = frustumMaterial;*/
       
    }

    void Update()
    {
        DrawCameraFrustum(referenceCamera); // Draw the frustum for the main camera
        target_visibility = IsTargetVisible(target); //attempt to find the target in the scene
        SwitchMaterial(target, target_visibility);  //change target's material if it is found in the scene
        

    }
    
    public bool IsTargetVisible(GameObject target)
    {
        Vector3 viewportPoint = referenceCamera.WorldToViewportPoint(target.transform.position);

        // Check if the target is within the camera's frustum
        if (viewportPoint.z > 0 && viewportPoint.x > 0 && viewportPoint.x < 1 && viewportPoint.y > 0 && viewportPoint.y < 1)
        {
            // The target is within the camera's frustum
            RaycastHit hit;
            Vector3 direction = target.transform.position - referenceCamera.transform.position;

            // Draw the ray in the Unity Editor's Scene view for debugging
            Debug.DrawRay(referenceCamera.transform.position, direction, Color.red, 1.0f); // This line draws the ray -- super cool way of doing physics measurements!
            

            if (Physics.Raycast(referenceCamera.transform.position, direction, out hit, direction.magnitude))
            {
                Debug.Log("Ray hit: " + hit.collider.gameObject.name);
                if(hit.collider.gameObject == target)
                { 
                    Debug.Log("TARGET FOUND!");
                    return true; 
                }
            }

        }
        Debug.Log("TARGET NOT FOUND!");
        return false; // Outside the camera's frustum or obstructed
    }
    
    
    public void SwitchMaterial(GameObject target, bool useMaterial1)
    {
        Renderer rend = target.GetComponent<Renderer>();

        if (rend != null) // Check if the target has a renderer
        {
            if (useMaterial1)
            {
                rend.material = target_Found_Mat;
            }
            else
            {
                rend.material = target_Missing_Mat;
            }
        }
    }

    
    //--------------------------- The code below helps us visualize the camera's frustum based on its sensor size and focal length ---------------------
    void DrawCameraFrustum(Camera cam)
    {
        float originalFOV = cam.fieldOfView;
        cam.fieldOfView = CalculatePhysicalCameraFOV(cam); // Set to the physical camera's calculated FOV
        
        // Calculate the frustum corners for the near and far clipping planes
        Vector3[] nearCorners = new Vector3[4];
        Vector3[] farCorners = new Vector3[4];
        cam.CalculateFrustumCorners(new Rect(0, 0, 1, 1), cam.nearClipPlane, Camera.MonoOrStereoscopicEye.Mono, nearCorners);
        cam.CalculateFrustumCorners(new Rect(0, 0, 1, 1), cam.farClipPlane, Camera.MonoOrStereoscopicEye.Mono, farCorners);

        // Convert the corners from local to world space
        for (int i = 0; i < 4; i++)
        {
            nearCorners[i] = cam.transform.TransformPoint(nearCorners[i]);
            farCorners[i] = cam.transform.TransformPoint(farCorners[i]);
        }

        // Draw the lines connecting the corners
        for (int i = 0; i < 4; i++)
        {
            int next = (i + 1) % 4; // Get the index of the next corner (to loop around and connect the last corner to the first)

            // Draw lines for the near clipping plane, far clipping plane, and lines connecting near to far
            Debug.DrawLine(nearCorners[i], nearCorners[next], Color.green);
            Debug.DrawLine(farCorners[i], farCorners[next], Color.green);
            Debug.DrawLine(nearCorners[i], farCorners[i], Color.green);
        }
        cam.fieldOfView = originalFOV; // Reset to the original FOV after drawing
    }

    float CalculatePhysicalCameraFOV(Camera cam)
    {
        // Assuming a horizontal sensor (adjust if using a vertical sensor)
        float sensorSize = cam.sensorSize.x;
        float fov = 2.0f * Mathf.Atan(sensorSize / (2.0f * cam.focalLength)) * Mathf.Rad2Deg;
        return fov;
    }


}

