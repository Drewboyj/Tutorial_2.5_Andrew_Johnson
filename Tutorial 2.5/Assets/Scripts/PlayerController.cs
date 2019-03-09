using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Animator anim;

    private bool facingRight;
    private Rigidbody2D rb2d;
    public float speed;
    public float jumpforce;
    private int count;
    public Text countText;
    public Text winText;
    public Text livesText;
    private int lives;
    public AudioSource winSource;
    


    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        count = 0;
        SetCountText();
        winText.text = "";
        lives = 3;
        SetlivesText();
        winSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        facingRight = true;
    }


    void Update()
    {
        if (Input.GetKey("escape"))
            Application.Quit();

        if (Input.GetKeyDown (KeyCode.LeftArrow))
        {
            anim.SetInteger("State", 1);

           
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            anim.SetInteger("State", 0);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            anim.SetInteger("State", 1);
            
        }

        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            anim.SetInteger("State", 0);
        }


        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            anim.SetInteger("State", 0);
        }

        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            anim.SetInteger("State", 0);
        }

        


    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");

        Vector2 movement = new Vector2(moveHorizontal, 0);

        rb2d.AddForce(movement * speed);

        Flip(moveHorizontal);

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
           
            if (Input.GetKey(KeyCode.UpArrow))
            {
                rb2d.AddForce(new Vector2(0, jumpforce), ForceMode2D.Impulse);
                anim.SetInteger("State", 2);

                if (Input.GetKey(KeyCode.UpArrow) && collision.collider.tag != "Ground")
                {
                    anim.SetInteger("State", 0);
                }
            }
        }
        

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();

            if (count == 4)
            {
                transform.position = new Vector3(43.0f, .0f, .0f);
                Camera.main.transform.position = new Vector3(43.0f, .0f, -10.0f);
                lives = 3;
                SetlivesText();
            }
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.SetActive(false);
            lives = lives - 1;
            SetlivesText();
        }

        
    }



    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 8)
        {
            winText.text = "You Win!";

            winSource.Play();
            transform.position = new Vector3(-43.0f, .0f, .0f);


        }


    }

    void SetlivesText()
    {
        livesText.text = "lives: " + lives.ToString();
        bool v = lives <= 0;
        if (v)
        {
            winText.text = "You Lose.";
            if (gameObject.tag == "Player")
            {
                Destroy(obj: gameObject);
            }

        }

    }

    private void Flip(float moveHorizontal)
    {
        if (moveHorizontal > 0 && !facingRight || moveHorizontal < 0 && facingRight)
        {
            facingRight = !facingRight;

            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }

    
}






