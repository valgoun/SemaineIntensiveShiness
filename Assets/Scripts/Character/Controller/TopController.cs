﻿using UnityEngine;
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
    public AudioSource audioBounce;

    private float _velZ = 0;
    private Transform _Wall = null;

    private float _speed;
    private float _alterationSpeed = 0.0f;
    private bool _isRolling;
    private bool _isColliding = false;

    private Tween _rollingTween;
    private MeshRenderer _mRend;

    private bool _isBoosting = false;

    public float RollingSpeed;
    public float BlendSpeed;

    private Vector3 rotation;
    private GameObject Poui;
    private GameObject Pivot;
    private Animator Anim;
    private bool _isDead;

    void Start()
    {
        _body = GetComponent<Rigidbody>();
        _mRend = GetComponent<MeshRenderer>();

        Pivot = transform.GetChild(0).gameObject;
        Poui = Pivot.transform.GetChild(0).gameObject;
        rotation = new Vector3(Poui.transform.rotation.eulerAngles.x + RollingSpeed * Time.deltaTime, 0, 0);
        Anim = Poui.GetComponent<Animator>();
    }

    public override void OnNoInput()
    {
        if (!_isColliding && !_isBoosting)
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
        if (_Wall && _isRolling)
        {
            if (transform.position.z - _Wall.position.z < 0)
            {
                Bounce(-1f);
            }
        }
    }

    public override void OnBotDown()
    {
        if (_Wall && _isRolling)
        {
            if (transform.position.z - _Wall.position.z > 0)
            {
                Bounce(1f);
            }
        }
    }

    public override void Init(bool IsRolling)
    {
        Start();
        if (!_isRolling)
        {
            WaitForRoll();
        }
        else
        {
            Roll(ResetTime);
            _body.velocity = Vector3.right * _speed * 0.7f;
        }
    }

    /// <summary>
    /// Rotate the character (and its velocity)
    /// </summary>
    /// <param name="rot">the amount of rotation (signed)</param>
    public void Rotate(float rot)
    {
        if (_isColliding)
            return;
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

    /// <summary>
    /// Wait for execute the next rolling phase
    /// </summary>
    public void WaitForRoll()
    {
        if (!enabled)
            return;
        _isRolling = false;
        _speed = MaxRunSpeed;
        DOVirtual.DelayedCall(TimeBetweenRoll, () => Roll(ResetTime));
    }

    /// <summary>
    /// Roll the player, should only be called to initiliaze the first roll, then use ResetRoll
    /// </summary>
    /// <param name="time">the amount of time the player has to stay in roll</param>
    void Roll(float time)
    {
        _isRolling = true;
        _speed = MaxRollSpeed;
        _rollingTween = DOVirtual.DelayedCall(time, () => WaitForRoll()).SetDelay(RollingInitialTime - ResetTime);
    }
    /// <summary>
    /// Reset the timer for roll
    /// </summary>
    public void ResetRoll()
    {
        if (_rollingTween != null)
            _rollingTween.Restart(false);
    }
    void FixedUpdate()
    {
        if (!_body)
            return;
        if (_body.velocity.magnitude < (_speed * (1 + _alterationSpeed)))
        {
            _body.AddForce(transform.forward * Acceleration, ForceMode.Acceleration);
        }
        else
        {
            _body.velocity = _body.velocity.normalized * (_speed * (1 + _alterationSpeed));
        }
    }

    void Update()
    {
        Anim.SetBool("IsRolling", _isRolling);
        Anim.SetBool("IsDead", _isDead);
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
        if (!enabled)
            return;

        if (other.collider.transform.CompareTag("Wall"))
        {
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
        if (_isRolling)
            _isColliding = true;
        Vector3 vel = _body.velocity;
        float modifierZ = (Mathf.Abs(_velZ) > MinimalBounce) ? _velZ : Mathf.Sign(_velZ) * MinimalBounce;
        vel.z = modifierZ * -1f;
        _body.velocity = vel;
        _body.MoveRotation(Quaternion.LookRotation(_body.velocity.normalized, Vector3.up));
        _velZ = _body.velocity.z;

        audioBounce.pitch = 1f + UnityEngine.Random.Range(-0.2f, 0.2f);
        audioBounce.Play();
    }

    /// <summary>
    /// Manually bounce the character in one direction
    /// </summary>
    /// <param name="direction">the direction to bounce the character</param>
    void Bounce(float direction)
    {
        Vector3 vel = _body.velocity;
        float modifierZ = (Mathf.Abs(_velZ) > MinimalBounce) ? _velZ : Mathf.Sign(_velZ) * MinimalBounce;
        vel.z = modifierZ * direction;
        _body.velocity = vel;
        _body.MoveRotation(Quaternion.LookRotation(_body.velocity.normalized, Vector3.up));
        _velZ = _body.velocity.z;

        audioBounce.pitch = 1f + UnityEngine.Random.Range(-0.2f, 0.2f);
        audioBounce.Play();
    }

    /// <summary>
    /// Boost the player in one direction
    /// </summary>
    /// <param name="BoostSpeed"></param>
    /// <param name="DecelerationTime"></param>
    /// <param name="Direction"></param>
    public void Boost(float BoostSpeed, float DecelerationTime, Vector3 Direction, float NoStabilisationTime, Vector3 pos)
    {
        _isBoosting = true;
        pos.y = transform.position.y;

        DOTween.To(() => { return _speed; }, x => _speed = x, 0, DecelerationTime).OnComplete(() =>
        {
            transform.position = pos;
        });

        DOVirtual.DelayedCall(DecelerationTime + 0.1f, () =>
        {
            if (Direction.z != 0)
            {
                float rot = -Mathf.Atan(Direction.x / Direction.z) * 10000;
                Rotate(rot);
            }
            _speed = MaxRollSpeed;
            _body.AddForce(Direction * BoostSpeed, ForceMode.VelocityChange);
            ResetRoll();
            DOVirtual.DelayedCall(NoStabilisationTime, () => _isBoosting = false);
        });
    }

    public override bool IsRolling()
    {
        return _isRolling;
    }

    public override void Disable()
    {
        Vector3 rotation = transform.rotation.eulerAngles; ;
        rotation.y = 0;
        _body.MoveRotation(Quaternion.Euler(rotation));
        _body.velocity = Vector3.RotateTowards(_body.velocity.normalized, transform.forward, 1.5f, 200).normalized * _body.velocity.magnitude;
        _rollingTween.Kill(false);
    }

    /// <summary>
    /// increment the speed modifier by the amount specified (can be negative)
    /// </summary>
    /// <param name="amount"></param>
    public void ModifySpeed(float amount)
    {
        _alterationSpeed += amount;
    }
}
