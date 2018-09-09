using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player : MonoBehaviour {

    AudioSource wingSound;
    AudioSource hitSound;
    AudioSource pointSound;
    Rigidbody2D rb;
    Vector3 initialPosition;
    public float forca = 100;
    public bool isAlive = true;
    public int points = 0;
    public Text pointText;
    public Text recordNum;
    public Text recordText;
    public bool isGameStarted = false;
    public Button btnStart;
    public bool isGrounded = false;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        wingSound = GetComponents<AudioSource>() [0];
        hitSound = GetComponents<AudioSource>() [1];
        pointSound = GetComponents<AudioSource>() [2];
        initialPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if(isGameStarted == true) { 
            if (Input.GetMouseButtonDown(0) && isAlive == true)
            {
                rb.velocity = Vector2.zero;
                rb.AddForce(new Vector2(0, forca));
                wingSound.Play();
            }
            if(isGrounded == false)
            {
                if (rb.velocity.y > 0)
                {
                    transform.rotation = Quaternion.Euler(0, 0, 45);
                }
                else if (rb.velocity.y < 0)
                {
                    transform.rotation = Quaternion.Euler(0, 0, -45);
                }
                else
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                }
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, -90);
            }
            
        }
        else
        {
            rb.velocity = Vector2.zero;
            transform.position = initialPosition;
        }

	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.transform.CompareTag("pipe") || collision.gameObject.name == "ground") && isAlive == true)
        {
            isAlive = false;
            hitSound.Play();
            btnStart.gameObject.SetActive(true);
            isGrounded = true;

            if(points > int.Parse(recordNum.text))
            {
                recordNum.text = points.ToString();
            }
            recordText.enabled = true;
            recordNum.enabled = true;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("pipe") && isAlive == true)
        {
            points++;
            pointText.text = points.ToString();
            pointSound.Play();
        }
    }

    public void OnGameStart()
    {
        foreach(var item in GameObject.FindGameObjectsWithTag("pipe"))
        {
            GameObject.Destroy(item);
        }
        points = 0;
        pointText.text = points.ToString();
        transform.position = initialPosition;
        isAlive = true;
        rb.velocity = Vector2.zero;
        isGameStarted = true;
        btnStart.gameObject.SetActive(false);
        recordText.enabled = false;
        recordNum.enabled = false;
        isGrounded = false;
    }
}
