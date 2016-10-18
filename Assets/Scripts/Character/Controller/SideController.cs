using UnityEngine;
using System.Collections;
using System;

public class SideController : Controller
{
    private bool _isGrounded = false;
    public override void OnTop()
    {
        return;
    }

    public override void OnBot()
    {
        return;
    }

    public override void OnTopDown()
    {
        return;
    }

    public override void OnBotDown()
    {
        return;
    }

    public override void Init()
    {
        Start();
        _body.useGravity = true;
    }

    public override void OnNoInput()
    {

    }

    void Start()
    {
        _body = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {

    }

}
