using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OwnerWalkingScript : MonoBehaviour
{
    [SerializeField]
    Vector3 initial_position = new Vector3(1, 1, 2);

    // the lengths of these three arrays need to be the same!
    [SerializeField]
    private Vector3[] positions;

    [SerializeField]
    private float[] timeAtEachPosition;

    [SerializeField]
    private float[] lerpSpeedToEachPosition;

    private int locationToMoveTo = 0;
    


    // Start is called before the first frame update
    void Start()
    {
        transform.position = positions[0];
        StartCoroutine(MoveOwnerAround());
    }

    void FixedUpdate()
    {
        Vector3 newLocation = positions[locationToMoveTo];
        float lerpRate = lerpSpeedToEachPosition[locationToMoveTo];
        transform.position = Vector3.MoveTowards(transform.position, newLocation, Time.fixedDeltaTime * lerpRate);
    }

    private IEnumerator MoveOwnerAround()
    {
        for (int i = 0; i < positions.Length; i += 1)
        {
            locationToMoveTo = i;
            yield return new WaitForSeconds(timeAtEachPosition[i]);
        }
        yield return null;
    }
}
