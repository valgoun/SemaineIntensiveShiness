using UnityEngine;

public class CercleMagique : MonoBehaviour
{
    public float BoostSpeed;
    public float DecelerationTime;
    public float NoStabilisationTime = 0.2f;
    public AudioSource boostSound;


    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            boostSound.Play();
            other.GetComponent<TopController>().Boost(BoostSpeed, DecelerationTime, transform.right, NoStabilisationTime, transform.position);
        }
    }
}
