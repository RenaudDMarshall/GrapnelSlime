using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public UnityEngine.UI.Image healthBarHandle;
    public int healthMax = 10;
    public int health = 10;
    public float healthPercent = 1;
    public int speed = 3;
    public bool targetActive = false;
    public bool knockback = false;
    public int countdown = 10;
    public Vector3 knockbackCenterPoint = Vector3.zero;
    public Vector3 destination = Vector3.zero;
    private Camera cam;
    public GameObject grapnel;
    private GameObject grap = null;
    public int attackCountdown = 15;
    public bool attacking = false;


    public BoxCollider2D box2Dcollider;

	// Use this for initialization
	void Start () {
        cam = Camera.main;
        var tempUIImage = FindObjectsOfType<UnityEngine.UI.Image>();
        foreach (UnityEngine.UI.Image img in tempUIImage)
        {
            if (img.name == "HealthBar")
            {
                healthBarHandle = img;
            }

        }
    }

    // Update is called once per frame
    void Update () {
		if (Input.GetKey(KeyCode.A) == true)
        {
            transform.position += Vector3.left * Time.deltaTime * speed;
        }
        if (Input.GetKey(KeyCode.D) == true)
        {
            transform.position += Vector3.right * Time.deltaTime * speed;
        }
        if (Input.GetKey(KeyCode.S) == true)
        {
            transform.position += Vector3.down * Time.deltaTime * speed;
        }
        if (Input.GetKey(KeyCode.W) == true)
        {
            transform.position += Vector3.up * Time.deltaTime * speed;
        }

        if (Input.GetKeyDown(KeyCode.Space) == true)
        {
            transform.localScale = new Vector2(2, 2);
            box2Dcollider.size = new Vector2(2, 2);
            attackCountdown = 15;
            attacking = true;
        }

        if (attacking == true)
        {
            attackCountdown--;
            if (attackCountdown <= 0)
            {
                attacking = false;
                box2Dcollider.size = new Vector2(1, 1);
                transform.localScale = new Vector2(1, 1);
            }
        }

        if (Input.GetMouseButtonDown(0) == true)
        {
            if (grap != null)
            {
                destination = cam.ScreenToWorldPoint(Input.mousePosition);
                destination = new Vector3(destination.x, destination.y, -1);
                targetActive = true;
                grap.transform.position = destination;
            }
            else
            {
                destination = cam.ScreenToWorldPoint(Input.mousePosition);
                destination = new Vector3(destination.x, destination.y, -1);
                targetActive = true;
                grap = Instantiate(grapnel, destination, Quaternion.identity);
            }
        }

        if (targetActive == true && knockback == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, speed * 2 * Time.deltaTime);
            if (transform.position == destination)
            {
                targetActive = false;
                DestroyObject(grap);
                transform.position = new Vector3(transform.position.x, transform.position.y, -1);
            }
        }

        if (knockback == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, knockbackCenterPoint, -speed * Time.deltaTime * 4);
            countdown--;
            if (countdown <= 0)
            {
                knockback = false;
                countdown = 10;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Enemy")
        {
            if (attacking == true)
            {
                collision.collider.gameObject.GetComponent<ChaseMovement>().knockback = true;
                collision.collider.gameObject.GetComponent<ChaseMovement>().health--;
                collision.collider.gameObject.GetComponent<ChaseMovement>().knockbackCenterPoint = transform.position;
            }
            else
            {
                //Knockback
                knockback = true;
                knockbackCenterPoint = collision.collider.transform.position;
                //Damage player
                health--;
                healthPercent = (float)health / healthMax;
                healthBarHandle.transform.localScale = new Vector3(healthPercent, 1, 1);
                if (health <= 0)
                {
                    UnityEngine.SceneManagement.SceneManager.LoadScene("testRoom");
                }
            }
        }
    }
}
