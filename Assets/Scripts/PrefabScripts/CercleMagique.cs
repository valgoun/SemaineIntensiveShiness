using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CercleMagique : MonoBehaviour
{
    public float BoostSpeed;
    public float DecelerationTime;
    public float boostLock = 0.1f;


    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<TopController>().Boost(BoostSpeed, DecelerationTime, boostLock, transform.right, transform.position);
        }
    }
}
