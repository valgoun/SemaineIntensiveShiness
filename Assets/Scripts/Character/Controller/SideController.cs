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
    private bool _isGrounded = false;
    private bool _isJumping = false;
    private bool _isGliding = false;
    private bool _isRolling = false;
    private bool _isStomping = false;
    private MeshRenderer _mRend;
    private Animator Anim;
    private GameObject Poui;
    private GameObject[] BabyPoui;
    private Animator[] BabyAnim;

    public override void OnTop()
    {
        if (_isJumping || _isGrounded || _isStomping)
            return;
        _isGliding = true;
        _isRolling = false;
        _mRend.material.color = Color.red;
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
            _isJumping = true;
            _body.DOMoveY(JumpHeight, JumpTime).SetRelative().SetEase(JumpEase).OnComplete(() => _isJumping = false);
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
        Anim.SetTrigger("Stomp");
        _body.DOKill();
        _isRolling = true;
        _isStomping = true;
        _mRend.material.color = Color.green;
        DOTween.To(() => { return _body.velocity.y; }, x =>
        {
            Vector3 vel = _body.velocity;
            vel.y = x;
            _body.velocity = vel;
        }, -StompingSpeed, StompingTime).SetEase(StompingEase);
        return;
    }

    public override void Init()
    {
        Start();
        _body.useGravity = true;
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
        _mRend = GetComponent<MeshRenderer>();
        Poui = transform.GetChild(0).gameObject;
        Anim = Poui.GetComponent<Animator>();
        BabyPoui = new GameObject[3];
        BabyAnim = new Animator[3];
        for (int i = 0; i < 3; i++)
        {
            BabyPoui[i] = transform.GetChild(i + 1).gameObject;
            BabyAnim[i] = BabyPoui[i].GetComponent<Animator>();
        }

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
        Debug.Log(_isGrounded + " " + _isGliding);
        Anim.SetBool("IsGliding", _isGliding);
        Anim.SetBool("IsGrounded", _isGrounded);
        Anim.SetBool("IsRolling", _isRolling);
        Anim.SetBool("IsJumping", _isJumping);

        for(int i=0; i<3; i++)
        {
            BabyAnim[i].SetBool("IsRunning", _isGrounded);
        }
        Debug.Log(_isGrounded + " " + _isGliding);
    }


    void checkGround()
    {
        Ray r = new Ray(transform.position, Vector3.down);
        RaycastHit hit;
        _isGrounded = Physics.Raycast(r, out hit, GroundDistance, Ground);
        if (_isGrounded && _isStomping)
            _isStomping = false;
    }

}
