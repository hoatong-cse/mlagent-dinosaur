using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;

public class PlayerController : Agent
{
    [SerializeField] private Animator _animator;

    [SerializeField] private Rigidbody2D _rigidbody2D;

    [SerializeField] private Transform _groundCheck;

    public LayerMask groundLayer; // Insert the layer here.


    private int jumpCount;
    public Spawner spawner;

    void Start()
    {
        spawner.Restart();
        //Time.timeScale = 2f;
    }

    // Update is called once per frame

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag.Equals("Obstacle")
            || other.gameObject.tag.Equals("Bird"))
        {
            SetReward(-10);
            EndEpisode();
        }

        if (other.gameObject.tag.Equals("Ground"))
        {
            jumpCount = 1;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Point"))
        {
            Debug.Log("Point");
            AddReward(10);
        }
    }

    public override void OnEpisodeBegin()
    {
        spawner.Restart();
        transform.localPosition = new Vector3(-1.2f, -0.16f, 0);
    }

    public override void OnActionReceived(float[] vectorAction)
    {
        _animator.SetFloat("action", vectorAction[0]);
        if (vectorAction[0] > 0.7f)
        {
            bool isGrounded = Physics2D.OverlapCircle(_groundCheck.position, 0.1f, groundLayer);

            if (isGrounded && jumpCount > 0)
            {
                _rigidbody2D.AddForce(new Vector2(0, 250));
                jumpCount--;
            }
        }

        AddReward(-0.1f);
    }

    private void Update()
    {
        // if (Input.GetKeyDown(KeyCode.UpArrow))
        // {
        //     bool isGrounded = Physics2D.OverlapCircle(_groundCheck.position, 0.1f, groundLayer);
        //
        //     if (isGrounded && jumpCount > 0)
        //     {
        //         _rigidbody2D.AddForce(new Vector2(0,200));
        //         jumpCount--;
        //     }
        //     
        //     _animator.SetFloat("action", 0.5f);
        // }
        //
        //
        // if (Input.GetKeyDown(KeyCode.DownArrow))
        // {
        //     _animator.SetFloat("action", 0.3f);
        // }
        //
    }

    public override void Heuristic(float[] actionsOut)
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            actionsOut[0] = 0.6f;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            actionsOut[0] = 0.3f;
        }
        else
        {
            actionsOut[0] = 0.5f;
        }
    }
}