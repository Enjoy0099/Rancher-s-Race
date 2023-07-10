using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Rigidbody playerRb;
    private Animator playerAnim;

    private int doubleJump = 0;

    private float score;
    public float jumpForce = 10;
    public float gravityModifier;

    //public bool isOnGround = true;
    public bool gameOver;

    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;

    private AudioSource playerAudio;
    public AudioClip jumpSound;
    public AudioClip crashSound;

    private float duration = 1f;
    private float timeElapsed = 0f;
    private Vector3 startPos, endPos;

    public TextMeshProUGUI score_Text;
    
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        Physics.gravity *= gravityModifier;
        score = 0;
        startPos = transform.position;
        endPos = Vector3.zero;
        InvokeRepeating("GameScore", 0f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (timeElapsed < duration)
            PlayerMoveAtStart();
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) && !gameOver && doubleJump < 2)
            {
                playerRb.velocity = Vector3.zero;
                playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                playerAnim.SetTrigger(NameManager.JUMP_ANIM_PARAMETER);
                //isOnGround = false;
                dirtParticle.Stop();
                doubleJump++;
                playerAudio.PlayOneShot(jumpSound, 1f);
            }

            /*if(Input.GetKey(KeyCode.D) && !gameOver)
            {
                playerAnim.speed = 2f;
                score += 2 * Time.deltaTime;
            }
            else if(!gameOver)
            {
                playerAnim.speed = 1f;
                score += 1 * Time.deltaTime;
            }*/

            if (Input.GetKey(KeyCode.D) && !gameOver)
            {
                Time.timeScale = 2f;
                score += 2 * Time.deltaTime;
            }
            else
            {
                Time.timeScale = 1f;
                score += 1 * Time.deltaTime;
            }
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

    void PlayerMoveAtStart()
    {
        float t = timeElapsed / duration;
        transform.position = Vector3.Lerp(startPos, endPos, t);
        timeElapsed += Time.deltaTime;
        if(timeElapsed > duration)
            transform.position = new Vector3(0f, 0f, 0f);
    }

    void GameScore()
    {
        if (!gameOver)
            score_Text.text = ((int)score).ToString();
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
