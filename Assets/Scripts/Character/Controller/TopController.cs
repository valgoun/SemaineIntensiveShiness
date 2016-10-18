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
    public float RollingInitialTime, ResetTime;

    private float _velZ = 0;
    private bool _isColliding = false;
    private Transform _Wall = null;

    private float _speed;
    private bool _isRolling;


    private Tween _rollingTween;
    private MeshRenderer _mRend;



    void Start()
    {
        _body = GetComponent<Rigidbody>();
        _mRend = GetComponent<MeshRenderer>();
    }

    public override void OnNoInput()
    {
        _body.MoveRotation(Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(Vector3.right, Vector3.up), StabilizationSpeed * Time.deltaTime));
        _body.velocity = Vector3.RotateTowards(_body.velocity.normalized, transform.forward, 1.5f, 200).normalized * _body.velocity.magnitude;
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
        Debug.Log("Top Down");
    }

    public override void OnBotDown()
    {
        Debug.Log("Bot Down");
    }

    public override void Init()
    {
        Start();
        _speed = MaxRunSpeed;

        WaitForRoll();
    }

    public void Rotate(float rot)
    {
        if (_isColliding)
            return;

        //Debug.Log(_Wall);
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
    void Update()
    {
        Debug.LogWarning("Rolling : " + _isRolling);
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
            _isColliding = false;
            _Wall = null;
        }
    }
    void OnCollisionEnter(Collision other)
    {

        if (other.collider.transform.CompareTag("Wall"))
        {
            _Wall = other.transform;
            if (!_isRolling)
            {
                _body.MoveRotation(Quaternion.LookRotation(_body.velocity.normalized, Vector3.up));
                return;
            }
            _isColliding = true;
            ResetRoll();
        }
        else
            return;

        Vector3 vel = _body.velocity;
        vel.z = _velZ * -1f;
        _velZ *= -1f;
        _body.velocity = vel;
        _body.MoveRotation(Quaternion.LookRotation(_body.velocity.normalized, Vector3.up));
    }
}
