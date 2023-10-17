using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{

    public Vector3[] waypoints; //the list of waypoints the target will move between
    public float moveDuration = 3.0f; //duration of time (s) the move between waypoints points
    public float waitDuration = 1.0f; //duration of time (s) that the target stays at a point before moving between waypoints 
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MoveThroughPositions(gameObject));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    IEnumerator MoveToPosition(GameObject obj, Vector3 targetPosition, float duration)
    {
        Vector3 startingPosition = obj.transform.position;
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            obj.transform.position = Vector3.Lerp(startingPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        obj.transform.position = targetPosition; // Ensure the final position is exact
    }
    

    public IEnumerator MoveThroughPositions(GameObject obj)
    {
        int currentPos = 0;

        while (true) // Infinite loop
        {
            Vector3 nextPosition = waypoints[currentPos];
            yield return MoveToPosition(obj, nextPosition, moveDuration);
            yield return new WaitForSeconds(waitDuration);

            currentPos = (currentPos + 1) % waypoints.Length; // Move to the next position, loop back to 0 if we're at the end
        }
    }


    
    
    
}
