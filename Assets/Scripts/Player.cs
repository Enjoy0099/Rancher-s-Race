using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody playerRb;
    private Animator playerAnim;

    public float jumpForce = 10;
    public float gravityModifier;
    public bool isOnGround = true;
    public bool gameOver;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        Physics.gravity *= gravityModifier;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver) 
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            playerAnim.SetTrigger(NameManager.JUMP_ANIM_PARAMETER);
            isOnGround = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag(NameManager.GROUND_TAG))
        {
            isOnGround = true;
        }
        else if(collision.gameObject.CompareTag(NameManager.OBSTACLE_TAG))
        {
            gameOver = true;
            playerAnim.SetBool(NameManager.DEATH_ANIMATION_PARAMETER, true);
            print("Game Over");
        }
    }
}
