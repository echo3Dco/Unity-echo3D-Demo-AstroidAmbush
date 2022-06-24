using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Controls Player object, counter, UI and level reset
public class Player : MonoBehaviour
{
    Rigidbody2D playerRB;
    public float moveSpeed;
    public float rotateAmount;
    float rotate;
    int score;
    public GameObject levelText;
    public GameObject playerObject;
    
    private void Awake()
    {
        // Get players rigidbody
        playerRB = GetComponent<Rigidbody2D>();

        // Turn off player in Hierarchy to allow echo3D assets to load, delay for 15 seconds
        playerObject.SetActive(false);
        Invoke("ReadyPlayer", 15.0f);
    }

    void ReadyPlayer()
    {
        playerObject.SetActive(true);
        print("Invoking...");
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if(mousePos.x < 0)
            {
                rotate = rotateAmount;
            }
            else
            {
                rotate = -rotateAmount;
            }

            transform.Rotate(0,0,rotate);
        }
    }

    void FixedUpdate()
    {
        playerRB.velocity = transform.up * moveSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Asteroid")
        {
            {
                // Add 1 for every collision with Planet tag
                Destroy(collision.gameObject);
                score++;

                if (score >= 3)
                {
                    print("Level Complete");
                    levelText.SetActive(true);
                }
            }
        }
        else if(collision.gameObject.tag == "Planet")
        {
            // If Player collides with Asteroid tag, restart scene
            SceneManager.LoadScene("Game");
        }
    }
}
