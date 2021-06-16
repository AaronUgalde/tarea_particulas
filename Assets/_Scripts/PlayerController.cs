using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

[RequireComponent(typeof(Rigidbody))]

public class PlayerController : MonoBehaviour
{
    private const string SpeedMultiplier = "Speed multiplier";
    private const string JUMP_TRIGGER = "Jump_trig";
    private const string SPEED_F = "Speed_f";
    private const string DeathB = "Death_b";
    private const string DeathtypeINT = "DeathType_int";

    public float jumpForce;
    private Rigidbody playerRb;
    public float gravityMultiplier;
    public bool isOnTheGround = true;
    private bool _gameOver;

    public bool gameOver
    {
        get => _gameOver;
    }

    private Animator _animator;

    public ParticleSystem explosion;
    public ParticleSystem dirt;


    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity *= gravityMultiplier;
        playerRb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _animator.SetFloat(SPEED_F, 1);
    }

    // Update is called once per frame
    void Update()
    {
        _animator.SetFloat(SpeedMultiplier, 1 + Time.time / 10);

        if (Input.GetKeyDown(KeyCode.Space) && isOnTheGround && !gameOver)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnTheGround = false;
            _animator.SetTrigger(JUMP_TRIGGER);
            dirt.Stop();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("ground"))
        {
            isOnTheGround = true;
            dirt.Play();
        }
        else if (other.gameObject.CompareTag("Obstacle"))
        {
            _gameOver = true;
            explosion.Play();
            _animator.SetBool(DeathB, true);
            _animator.SetInteger(DeathtypeINT, 2);
            dirt.Stop();
        }
    }
}
