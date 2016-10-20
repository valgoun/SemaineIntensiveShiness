using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CercleMagique : MonoBehaviour
{
    public float BoostSpeed;
    public float DecelerationTime;
    public float NoStabilisationTime = 0.2f;

    void Start()
    {

    }

    void Update()
    {

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<TopController>().Boost(BoostSpeed, DecelerationTime, transform.right, NoStabilisationTime, transform.position);
        }
    }
}
