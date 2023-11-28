using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDestroyer : MonoBehaviour
{
    // game object to be destroyed when colliding with the item destroyer.
    private GameObject mItemToDestroy;
    
    //This object's Mesh Collider used to trigger destroy item
    [SerializeField] private MeshCollider mMeshCollider;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Destroy the game object colliding with the the Object Destroyer's collider (mMeshCollider)
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        // destroy the game object which collided with the item destroyer.
        Destroy(other.gameObject);
    }
}
