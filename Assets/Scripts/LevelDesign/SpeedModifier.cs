using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedModifier : MonoBehaviour
{
    [TooltipAttribute("The speed of the player will be modified by the ModifierAmount until the player stay in the zone")]
    [Range(-1f, 1f)]
    public float ModifierAmount = 5f;

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<TopController>().ModifySpeed(ModifierAmount);
        }
    }

    /// <summary>
    /// OnTriggerExit is called when the Collider other has stopped touching the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<TopController>().ModifySpeed(-ModifierAmount);
        }
    }
}
