using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public float trackSpeed = 10;
    public Transform target;
    private Vector3 offset;
    
    [Range(0.1f,0.9f)]
    public float smoothness = 0.125f;
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - target.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        var targetPostion = Vector3.MoveTowards(transform.position, target.position + offset, trackSpeed * Time.deltaTime);
        
        var smoothedPosition = Vector3.Lerp(transform.position, targetPostion, smoothness);
        transform.position = smoothedPosition;
    }
}