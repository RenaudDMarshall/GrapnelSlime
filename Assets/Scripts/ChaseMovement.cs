using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseMovement : MonoBehaviour {

    public GameObject player;
    public bool knockback = false;
    public Vector3 knockbackCenterPoint = Vector3.zero;
    public int countdown = 30;
    public int health = 2;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("slime001");
	}
	
	// Update is called once per frame
	void Update () {
        

        if (knockback == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, knockbackCenterPoint, -Time.deltaTime * 4);
            countdown--;
            if (countdown <= 0)
            {
                knockback = false;
                countdown = 30;
            }

        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 2 * Time.deltaTime);
        }

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
