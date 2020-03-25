using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public float pullForce;
    public float rotateSpeed;
    public bool isPulled = false;
    public PlayerController playerScript;

    // Start is called before the first frame update
    void Start()
    {
        playerScript = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        //Move the object
        //playerScript.rb2D.velocity = -transform.up * playerScript.moveSpeed;

        if (Input.GetMouseButton(0) && !isPulled)
        {
            if (playerScript.closestTower != null && playerScript.hookedTower == null)
            {
                playerScript.hookedTower = playerScript.closestTower;
            }
            if (playerScript.hookedTower)
            {
                float distance = Vector2.Distance(transform.position, playerScript.hookedTower.transform.position);

                //Gravitation toward tower
                Vector3 pullDirection = (playerScript.hookedTower.transform.position - transform.position).normalized;
                float newPullForce = Mathf.Clamp(pullForce / distance, 20, 50);
                playerScript.rb2D.AddForce(pullDirection * newPullForce);

                //Angular velocity
                playerScript.rb2D.angularVelocity = -rotateSpeed / distance;
                isPulled = true;
                Debug.Log("Pressing Mouse Button");
            }
        }
        else 
        {
            isPulled = false;
            playerScript.rb2D.angularVelocity = 0;
        }
    }
}
