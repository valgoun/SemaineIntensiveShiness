/**
- bug :
    - re saute automatiquement
*/

using UnityEngine;
using System.Collections;
using System;
using DG.Tweening;

public class SideController : Controller
{
    public float GroundDistance = 0.6f;
    public float JumpHeight = 2f;
    public float JumpTime = 0.2f;
    public float GlidingVerticalSpeed = 0.5f;
    public float Speed = 7.5f;
    public float RollingSpeed = 12.5f;
    public float StompingSpeed = 25f;
    public float StompingTime = 0.2f;
    public Ease JumpEase, StompingEase;
    public LayerMask Ground;
    public float BlendSpeed;
    public float RotationSpeed;
    public AudioSource jumpSound;
    private bool _isGrounded = false;
    private bool _isJumping = false;
    private bool _isGliding = false;
    private bool _isRolling = false;
    private bool _isStomping = false;
    private bool _isDead = false;
    private Tweener tn;

    private Animator Anim;
    private GameObject Poui;
    private Vector3 rotation;
    private GameObject Pivot;

    public override void OnTop()
    {
        if (_isJumping || _isGrounded || _isStomping)
            return;
        _isGliding = true;
        _isRolling = false;
    }

    public override void OnBot()
    {
        OnNoInput();
        return;
    }

    public override void OnTopDown()
    {
        if (_isGrounded && !_isStomping)
        {
            jumpSound.pitch = 0.9f;
            jumpSound.Play();
            jumpSound.pitch = 1.1f;
            _isJumping = true;
            _body.DOMoveY(JumpHeight, JumpTime).SetRelative().SetEase(JumpEase).OnComplete(() => _isJumping = false);
            if (tn != null)
            {
                tn.Kill(false);
                tn = null;
            }
        }
        return;
    }

    public void Bump(float JumpMultiplicator)
    {
        _isJumping = true;
        _body.DOMoveY(JumpHeight * JumpMultiplicator, JumpTime).SetRelative().SetEase(JumpEase).OnComplete(() => _isJumping = false);
    }

    public override void OnBotDown()
    {
        if (_isStomping)
            return;
        _isRolling = true;
        if (_isGrounded)
            return;
        _body.DOKill();
        _isStomping = true;


        tn = DOTween.To(() => { return _body.velocity.y; }, x =>
        {
            Vector3 vel = _body.velocity;
            vel.y = x;
            _body.velocity = vel;
        }, -StompingSpeed, StompingTime).SetEase(StompingEase);
        return;
    }

    public override void Init(bool IsRolling)
    {
        Start();
        _body.useGravity = true;
        _body.constraints = RigidbodyConstraints.FreezePositionZ;
    }

    public override void OnNoInput()
    {
        if (_isGliding)
        {
            _isGliding = false;

            Vector3 vel = _body.velocity;
            vel.y -= 2f;
            _body.velocity = vel;
        }
    }

    void Start()
    {
        _body = GetComponent<Rigidbody>();
        Poui = transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
        Anim = Poui.GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        Vector3 vel = _body.velocity;
        if (_isGliding && !_isGrounded)
        {
            vel.y = Mathf.Clamp(vel.y, -GlidingVerticalSpeed, 0);
        }
        vel.x = !_isRolling ? Speed : RollingSpeed;
        _body.velocity = vel;
    }

    void Update()
    {
        checkGround();
        Anim.SetBool("IsGliding", _isGliding);
        Anim.SetBool("IsGrounded", _isGrounded);
        Anim.SetBool("IsRolling", _isRolling);
        Anim.SetBool("IsJumping", _isJumping);
        Anim.SetBool("IsStomping", _isStomping);
        Anim.SetBool("IsDead", _isDead);
    }

    /// <summary>
    /// Check under the player if there is Ground
    /// The ground need to be on the same layer as specified by GroundDistance
    /// </summary>
    void checkGround()
    {
        Ray r = new Ray(transform.position, Vector3.down);
        RaycastHit hit;
        _isGrounded = Physics.Raycast(r, out hit, GroundDistance, Ground);
        if (_isGrounded && _isStomping)
        {
            _isStomping = false;
        }
        _isJumping = false;
    }

    public override bool IsRolling()
    {
        return _isRolling;
    }

    public override void Disable()
    {
        _body.constraints = RigidbodyConstraints.None;
        //nothing to do when disable
    }
}
