using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionToSide : MonoBehaviour
{
    private Transform _runTarget, _rollTarget;
    public float _runSpeed = 15f,
                 _rollSpeed = 15f;
    // Use this for initialization
    void Start()
    {
        _runTarget = transform.GetChild(0);
        _rollTarget = transform.GetChild(1);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            other.GetComponent<Character>().goSideView(_runTarget, _rollTarget, _runSpeed, _rollSpeed);
        }
    }
}
