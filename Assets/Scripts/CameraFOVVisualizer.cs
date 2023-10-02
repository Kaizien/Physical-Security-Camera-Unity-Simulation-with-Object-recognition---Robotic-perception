using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Camera))]
public class CameraFOVVisualizer : MonoBehaviour
{
    public Color frustumColor = Color.cyan;
    public bool alwaysDisplay = false;

    private Camera cam;
    private Mesh frustumMesh;
    private GameObject frustumObject;
    private Material frustumMaterial;

    void OnDrawGizmos()
    {
        if (alwaysDisplay)
            DrawFrustum();
    }

    void OnDrawGizmosSelected()
    {
        if (!alwaysDisplay)
            DrawFrustum();
    }

    void Start()
    {
        cam = GetComponent<Camera>();
        CreateFrustumMesh();
        CreateFrustumObject();
    }

    


    void CreateFrustumObject()
    {
        // Use a standard shader that supports transparency
        frustumMaterial = new Material(Shader.Find("Standard"));
        Color transparentColor = frustumColor;
    
        // Set the alpha to a value less than 1 to make it transparent
        transparentColor.a = 0.25f; // 50% transparent
        frustumMaterial.color = transparentColor;
        frustumMaterial.SetFloat("_Mode", 3);  // Set to Transparent mode
        frustumMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        frustumMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        frustumMaterial.SetInt("_ZWrite", 0);
        frustumMaterial.DisableKeyword("_ALPHATEST_ON");
        frustumMaterial.DisableKeyword("_ALPHABLEND_ON");
        frustumMaterial.EnableKeyword("_ALPHAPREMULTIPLY_ON");
        frustumMaterial.renderQueue = 3000;

        frustumObject = new GameObject("FOV_Frustum");
        frustumObject.transform.SetParent(transform, false);
        frustumObject.AddComponent<MeshFilter>().mesh = frustumMesh;
        frustumObject.AddComponent<MeshRenderer>().material = frustumMaterial;
    }

    void CreateFrustumMesh()
{
    frustumMesh = new Mesh();

    Vector3[] vertices = new Vector3[8];
    int[] triangles = new int[24];

    float aspect = cam.aspect;
    float verticalFOV = cam.fieldOfView;
    
    // If using the Physical Camera properties
    if (cam.usePhysicalProperties)
    {
        float sensorSize = cam.sensorSize.y;
        float focalLength = cam.focalLength;
        verticalFOV = 2.0f * Mathf.Atan(sensorSize / (2.0f * focalLength)) * Mathf.Rad2Deg;
    }

    float tanFov = Mathf.Tan(verticalFOV * 0.5f * Mathf.Deg2Rad);
    float nearHeight = cam.nearClipPlane * tanFov;
    float nearWidth = nearHeight * aspect;
    
    float farHeight = cam.farClipPlane * tanFov;
    float farWidth = farHeight * aspect;

    Vector3 origin = Vector3.zero;
    RaycastHit hit;

    // Bottom-left
    if (Physics.Raycast(origin, new Vector3(-farWidth, -farHeight, cam.farClipPlane).normalized, out hit, cam.farClipPlane))
        vertices[4] = hit.point;
        
    else
        vertices[4] = new Vector3(-farWidth, -farHeight, cam.farClipPlane);

    // Bottom-right
    if (Physics.Raycast(origin, new Vector3(farWidth, -farHeight, cam.farClipPlane).normalized, out hit, cam.farClipPlane))
        vertices[5] = hit.point;
    else
        vertices[5] = new Vector3(farWidth, -farHeight, cam.farClipPlane);

    // Top-right
    if (Physics.Raycast(origin, new Vector3(farWidth, farHeight, cam.farClipPlane).normalized, out hit, cam.farClipPlane))
        vertices[6] = hit.point;
    else
        vertices[6] = new Vector3(farWidth, farHeight, cam.farClipPlane);

    // Top-left
    if (Physics.Raycast(origin, new Vector3(-farWidth, farHeight, cam.farClipPlane).normalized, out hit, cam.farClipPlane))
        vertices[7] = hit.point;
    else
        vertices[7] = new Vector3(-farWidth, farHeight, cam.farClipPlane);

    // Near plane vertices remain unchanged
    vertices[0] = new Vector3(-nearWidth, -nearHeight, cam.nearClipPlane);
    vertices[1] = new Vector3(nearWidth, -nearHeight, cam.nearClipPlane);
    vertices[2] = new Vector3(nearWidth, nearHeight, cam.nearClipPlane);
    vertices[3] = new Vector3(-nearWidth, nearHeight, cam.nearClipPlane);

    triangles = new int[]
    {
        0, 2, 1, 0, 3, 2,
        4, 5, 6, 4, 6, 7,
        0, 1, 5, 0, 5, 4,
        1, 2, 6, 1, 6, 5,
        2, 3, 7, 2, 7, 6,
        3, 0, 4, 3, 4, 7
    };

    frustumMesh.vertices = vertices;
    frustumMesh.triangles = triangles;
}

    Vector3 GetIntersectionPoint(Vector3 target)
    {
        Ray ray = new Ray(transform.position, transform.TransformDirection(target.normalized));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, cam.farClipPlane))
        {
            return hit.point - transform.position;
        }
        return target;
    }

    void DrawFrustum()
    {
        Gizmos.color = frustumColor;
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawMesh(frustumMesh);
    }
}
