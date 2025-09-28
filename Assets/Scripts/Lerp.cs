using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lerp : MonoBehaviour
{
    public GameObject player;
    public GameObject entity;
    public float duration = 60f;
    private float elapsed = 0;
    private float t;
    private Vector3 start;
    private Vector3 end;
    // Start is called before the first frame update
    void Start()
    {
        start = new Vector3(entity.transform.position.x, entity.transform.position.y, entity.transform.position.z);
        end = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);


    }

    // Update is called once per frame
    void Update()
    {
        elapsed += Time.deltaTime;
        t = elapsed / duration;
        entity.transform.position = Vector3.Lerp(start, end, t);

    }
}
