using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    public Transform player; // Referensi ke karakter utama
    public Vector3 offset = new Vector3(1f, -0.5f, 0f); // Offset relatif untuk posisi NPC
    private Animator animator;
    private Animator playerAnimator; // Referensi ke animator karakter utama

    private void Awake()
    {
        animator = GetComponent<Animator>();
        
        // Menemukan komponen Animator dari karakter utama
        if(player != null)
        {
            playerAnimator = player.GetComponent<Animator>();
        }
    }

    private void Update()
    {
        if (player != null)
        {
            // Mendapatkan posisi target NPC
            Vector3 targetPosition = player.position + offset;

            // Mengatur posisi NPC dengan offset
            transform.position = targetPosition;

            // Mendapatkan arah karakter utama terhadap NPC
            float direction = Mathf.Sign(player.localScale.x);

            // Mendapatkan arah karakter utama
            float playerHorizontalInput = Input.GetAxisRaw("Horizontal");
            float playerVerticalInput = Input.GetAxisRaw("Vertical");

            // Mengatur animasi NPC berdasarkan arah karakter utama
            if (playerHorizontalInput != 0 || playerVerticalInput != 0)
            {
                // Jika ada input dari pemain, gunakan input tersebut
                animator.SetFloat("moveX", playerHorizontalInput);
                animator.SetFloat("moveY", playerVerticalInput);
            }
            else if (playerAnimator != null)
            {
                // Jika tidak ada input, gunakan arah terakhir dari karakter utama
                bool isMoving = playerAnimator.GetBool("isMoving");
                float lastMoveX = playerAnimator.GetFloat("moveX");
                float lastMoveY = playerAnimator.GetFloat("moveY");
                animator.SetFloat("moveX", lastMoveX);
                animator.SetFloat("moveY", lastMoveY);
            }

            // Memanggil fungsi animasi dari skrip karakter utama
            if(playerAnimator != null)
            {
                // Memanggil variabel isMoving dari skrip karakter utama
                bool isMoving = playerAnimator.GetBool("isMoving");
                animator.SetBool("isMoving", isMoving);
            }
        }
    }
}
