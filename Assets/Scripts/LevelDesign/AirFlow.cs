using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirFlow : MonoBehaviour
{
    public float Force;

    /// <summary>
    /// OnTriggerStay is called once per frame for every Collider other
    /// that is touching the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerStay(Collider other)
    {
        var body = other.GetComponent<Rigidbody>();
        if (body)
        {
            body.AddForce(Vector3.up * Force, ForceMode.Acceleration);
        }

    }

}
