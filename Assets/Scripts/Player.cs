using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody playerRb;
    private Animator playerAnim;

    private int doubleJump = 0;

    public float jumpForce = 10;
    public float gravityModifier;
    //public bool isOnGround = true;
    public bool gameOver;

    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;

    public AudioSource playerAudio;
    public AudioClip jumpSound;
    public AudioClip crashSound;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        Physics.gravity *= gravityModifier;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !gameOver && doubleJump < 2) 
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            playerAnim.SetTrigger(NameManager.JUMP_ANIM_PARAMETER);
            //isOnGround = false;
            dirtParticle.Stop();
            doubleJump++;
            playerAudio.PlayOneShot(jumpSound, 1f);
            Debug.Log(doubleJump);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag(NameManager.GROUND_TAG))
        {
            //isOnGround = true;
            doubleJump = 0;
            if(!gameOver)
                dirtParticle.Play();
        }
        else if(collision.gameObject.CompareTag(NameManager.OBSTACLE_TAG))
        {
            gameOver = true;
            playerAnim.SetBool(NameManager.DEATH_ANIMATION_PARAMETER, true);
            explosionParticle.Play();
            dirtParticle.Stop();
            playerAudio.PlayOneShot(crashSound, 1f);
        }
    }
}
