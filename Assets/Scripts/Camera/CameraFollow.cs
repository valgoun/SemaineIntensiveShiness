using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{

    public Transform Target;
    public float SmoothTime = 0.3f;
    private Vector3 vel = Vector3.zero;

    // Use this for initialization
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {

    }
    void FixedUpdate()
    {
        transform.position = Vector3.SmoothDamp(transform.position, Target.position, ref vel, SmoothTime);

    }
}
