using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb2D;
    public float moveSpeed = 5f;
    private UIControllerScript uiControl;
    private AudioSource myAudio;
    private bool isCrashed = false;
    public Tower towerScript;
    public GameObject closestTower;
    public GameObject hookedTower;
    public float startPosX;
    public float startPosY;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = this.gameObject.GetComponent<Rigidbody2D>();
        uiControl = GameObject.Find("Canvas").GetComponent<UIControllerScript>();
        myAudio = this.gameObject.GetComponent<AudioSource>();
        towerScript = FindObjectOfType<Tower>();
    }

    // Update is called once per frame
    void Update()
    {
        rb2D.velocity = -transform.up * moveSpeed;
        if (isCrashed)
        {
            if (!myAudio.isPlaying)
            {
                //Restart scene
                restartPosition();
            }
        }
        else if (!isCrashed)
        {
            //Move the object
            rb2D.velocity = -transform.up * moveSpeed;
            rb2D.angularVelocity = 0f;
        }
        

    }

    public void restartPosition()
    {
        this.transform.position = new Vector3(startPosX, startPosY, 0);
        //Restart rotation
        this.transform.rotation = Quaternion.Euler(0f, 0f, 90f);

        //Set isCrashed to false
        isCrashed = false;

        if (closestTower)
        {
            closestTower.GetComponent<SpriteRenderer>().color = Color.white;
            closestTower = null;
        }

    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            if (!isCrashed)
            {
                //Play SFX
                myAudio.Play();
                rb2D.velocity = new Vector3(0f, 0f, 0f);
                rb2D.angularVelocity = 0f;
                isCrashed = true;
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Goal")
        {
            Debug.Log("Levelclear!");
            uiControl.endGame();
            this.gameObject.SetActive(false);
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Tower")
        {
            closestTower = collision.gameObject;

            //Change tower color back to green as indicator
            collision.gameObject.GetComponent<SpriteRenderer>().color = Color.green;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (towerScript.isPulled) return;

        if (collision.gameObject.tag == "Tower")
        {
            closestTower = null;

            //Change tower color back to normal
            collision.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
}
