using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetGenerator : MonoBehaviour
{
    
    // contains a list of GameObjects which can be spawned from the ItemGenerator
    [SerializeField] private GameObject[] targetPrefabs;
    
    //timer for spawning items
    [SerializeField] private float mSpawnInterval = 5.0f;
    
    // current count of the # of target items spawned
    [SerializeField] int mTargetCount = 0;
    
    // Destroyer game object which destroys the target items when colliding with them.
    [SerializeField] private GameObject mItemDestroyer;
    
    // Item Creator Game Object which creates the target items
    [SerializeField] private GameObject mItemCreator;
    
    // Start is called before the first frame update
    void Start()
    {
        mTargetCount = 0;
        //SpawnTarget is a coroutine, so we need to start it using StartCoroutine
        StartCoroutine(SpawnTarget()); 
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    /// <summary>
    /// SpawnTarget is a coroutine which spawns a random item from the list of prefabs
    /// It should only spawn objects every mSpawnInterval seconds.
    /// </summary>
    IEnumerator SpawnTarget()
    {
        while (true)
        {
            // spawn a random item from the list of prefabs every mSpawnInterval seconds
            GameObject target = Instantiate(targetPrefabs[Random.Range(0, targetPrefabs.Length)]);

            // set the position of the item to be 0,0,0 of the center of the mItemCreator
            target.transform.position = mItemCreator.transform.position;

            // Increment the number of target items which have been spawned by the target generator.
            if (target == targetPrefabs[0])
            {
                mTargetCount += 1;
            }
            else
            {
                // increment the count of the # of items spawned
                mTargetCount++;
            }

            // wait for the spawn interval before spawning another item
            yield return new WaitForSeconds(mSpawnInterval);
        }
    }



    
}
