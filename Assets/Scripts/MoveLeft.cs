using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private float speed = 30;
    private Player playerScript;
    private float leftBound = -15f;
    // Start is called before the first frame update
    void Start()
    {
        playerScript = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        //Speed();

        if (!playerScript.gameOver)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
        if (transform.position.x < leftBound && gameObject.CompareTag(NameManager.OBSTACLE_TAG))
        {
            Destroy(gameObject);
        }
    }

    /*void Speed()
    {
        if (Input.GetKey(KeyCode.D))
        {
            speed = 60f;
            leftBound = -10f;
        }
        else
        {
            speed = 30f;
            leftBound = -15f;
        }
    }*/
}
