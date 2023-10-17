using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class CameraFOV : MonoBehaviour
{
    public Camera referenceCamera;
    public Material frustumMaterial;  // Drag your material here in the inspector
    public Material target_Found_Mat;
    public Material target_Missing_Mat;
    public GameObject target;
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
        bool target_visibility = false;
        target_visibility = IsTargetVisible(target);

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


    Mesh CreateFrustumMesh(Camera cam)
    {
        Mesh mesh = new Mesh();

        Vector3[] vertices = new Vector3[8];

        float tanFov = Mathf.Tan(cam.fieldOfView * 0.5f * Mathf.Deg2Rad);
        float nearHeight = tanFov * cam.nearClipPlane * 2.0f;
        float nearWidth = nearHeight * cam.aspect;

        float farHeight = tanFov * cam.farClipPlane * 2.0f;
        float farWidth = farHeight * cam.aspect;

        // Near plane vertices
        vertices[0] = new Vector3(-nearWidth * 0.5f, -nearHeight * 0.5f, cam.nearClipPlane);
        vertices[1] = new Vector3(nearWidth * 0.5f, -nearHeight * 0.5f, cam.nearClipPlane);
        vertices[2] = new Vector3(nearWidth * 0.5f, nearHeight * 0.5f, cam.nearClipPlane);
        vertices[3] = new Vector3(-nearWidth * 0.5f, nearHeight * 0.5f, cam.nearClipPlane);

        // Far plane vertices
        vertices[4] = new Vector3(-farWidth * 0.5f, -farHeight * 0.5f, cam.farClipPlane);
        vertices[5] = new Vector3(farWidth * 0.5f, -farHeight * 0.5f, cam.farClipPlane);
        vertices[6] = new Vector3(farWidth * 0.5f, farHeight * 0.5f, cam.farClipPlane);
        vertices[7] = new Vector3(-farWidth * 0.5f, farHeight * 0.5f, cam.farClipPlane);

        int[] triangles = {
            // Near plane
            0, 1, 2, 0, 2, 3,
            // Far plane
            4, 6, 5, 4, 7, 6,
            // Left side
            4, 0, 3, 4, 3, 7,
            // Right side
            5, 2, 1, 5, 6, 2,
            // Top
            3, 2, 6, 3, 6, 7,
            // Bottom
            4, 5, 1, 4, 1, 0
        };

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        return mesh;
    }
}

