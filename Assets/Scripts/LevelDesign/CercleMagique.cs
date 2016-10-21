using UnityEngine;

public class CercleMagique : MonoBehaviour
{
    public float BoostSpeed;
    public float DecelerationTime;
    public float NoStabilisationTime = 0.2f;


    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<TopController>().Boost(BoostSpeed, DecelerationTime, transform.right, NoStabilisationTime, transform.position);
        }
    }
}
