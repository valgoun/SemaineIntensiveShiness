using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System;
using DG.Tweening;

public class TopController : Controller
{
    [Header("Speed")]
    public float MaxRunSpeed;
    public float MaxRollSpeed, Acceleration, RotationSpeed, AngleMax, StabilizationSpeed;
    [Header("Roll")]
    public float TimeBetweenRoll;
    public float RollingInitialTime, ResetTime, MinimalBounce;

    private float _velZ = 0;
    private Transform _Wall = null;

    private float _speed;
    private bool _isRolling;
    private bool _isColliding = false;

    private Tween _rollingTween;
    private MeshRenderer _mRend;



    void Start()
    {
        _body = GetComponent<Rigidbody>();
        _mRend = GetComponent<MeshRenderer>();
    }

    public override void OnNoInput()
    {
        if (!_isColliding)
        {
            _body.MoveRotation(Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(Vector3.right, Vector3.up), StabilizationSpeed * Time.deltaTime));
            _body.velocity = Vector3.RotateTowards(_body.velocity.normalized, transform.forward, 1.5f, 200).normalized * _body.velocity.magnitude;
            _velZ = _body.velocity.z;
        }
    }
    public override void OnTop()
    {
        Rotate(-RotationSpeed);
    }

    public override void OnBot()
    {
        Rotate(RotationSpeed);
    }

    public override void OnTopDown()
    {

    }

    public override void OnBotDown()
    {

    }

    public override void Init()
    {
        Start();
        _speed = MaxRunSpeed;

        WaitForRoll();
    }

    public void Rotate(float rot)
    {
        if (_Wall)
        {
            if (Mathf.Sign(transform.position.z - _Wall.position.z) == Mathf.Sign(rot))
                return;
        }
        Vector3 rotation = transform.rotation.eulerAngles;
        rotation.y += Time.deltaTime * rot;
        rotation.y = Mathf.Clamp(rotation.y, 90 - AngleMax, 90 + AngleMax);
        _body.MoveRotation(Quaternion.Euler(rotation));
        _body.velocity = Vector3.RotateTowards(_body.velocity.normalized, transform.forward, 1.5f, 200).normalized * _body.velocity.magnitude;
        _velZ = _body.velocity.z;
    }
    void WaitForRoll()
    {
        _isRolling = false;
        _speed = MaxRunSpeed;
        _mRend.material.color = Color.red;
        DOVirtual.DelayedCall(TimeBetweenRoll, () => Roll(ResetTime));
    }

    void Roll(float time)
    {
        _isRolling = true;
        _speed = MaxRollSpeed;
        _mRend.material.color = Color.green;
        _rollingTween = DOVirtual.DelayedCall(time, () => WaitForRoll()).SetDelay(RollingInitialTime - ResetTime);
    }
    void ResetRoll()
    {
        _rollingTween.Restart(false);
    }
    void FixedUpdate()
    {
        if (!_body)
            return;
        if (_body.velocity.magnitude < _speed)
        {
            _body.AddForce(transform.forward * Acceleration, ForceMode.Acceleration);
        }
        else
        {
            _body.velocity = _body.velocity.normalized * _speed;
        }

    }
    void OnCollisionExit(Collision other)
    {
        if (other.collider.transform.CompareTag("Wall"))
        {
            _Wall = null;
            _isColliding = false;
        }
    }
    void OnCollisionEnter(Collision other)
    {

        if (other.collider.transform.CompareTag("Wall"))
        {
            _isColliding = true;
            _Wall = other.transform;
            if (!_isRolling)
            {
                _body.MoveRotation(Quaternion.LookRotation(_body.velocity.normalized, Vector3.up));
                return;
            }
            ResetRoll();
        }
        else
            return;

        Vector3 vel = _body.velocity;
        float modifierZ = (Mathf.Abs(_velZ) > MinimalBounce) ? _velZ : Mathf.Sign(_velZ) * MinimalBounce;
        Debug.Log("caca + " + modifierZ);
        vel.z = modifierZ * -1f;
        _body.velocity = vel;
        _body.MoveRotation(Quaternion.LookRotation(_body.velocity.normalized, Vector3.up));
        _velZ = _body.velocity.z;
    }
}
