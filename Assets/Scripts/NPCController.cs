using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    public Transform player; // Referensi ke Transform player
    public float moveSpeed = 0.1f; // Kecepatan NPC
    public float stoppingDistance = 1.0f; // Jarak aman dari player
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        float distance = Vector2.Distance(transform.position, player.position);

        if (distance > stoppingDistance)
        {
            Vector2 direction = (player.position - transform.position).normalized;

            Vector2 move = direction * moveSpeed * Time.deltaTime;

            transform.position = new Vector2(transform.position.x + move.x, transform.position.y + move.y);

            animator.SetFloat("moveX", direction.x);
            animator.SetFloat("moveY", direction.y);
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }
}