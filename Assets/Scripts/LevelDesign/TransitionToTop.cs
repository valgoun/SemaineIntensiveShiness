using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionToTop : MonoBehaviour
{
    public float Speed;

    private Transform _target;


    // Use this for initialization
    void Start()
    {
        _target = transform.GetChild(0);
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            other.GetComponent<Character>().goTopView(_target, Speed);
        }
    }
}
