using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public bool usePhysics;
    private Rigidbody rb;

    private Animator anim; 

    private string horizontal, vertical, fire;
    public enum Player { Player1, Player2, Player3 };
    public Player currentPlayer = Player.Player1;

    public Material p1Mat, p2Mat, p3Mat;
    private Material thisMat;

    public GameObject node;
    public float cooldown = 1f;
    private bool isReady = true;

    public float speed;
    public float movementSpeed = 8f;
    public float zoneSpeed = 12f;

    private List<Collider> zones = new List<Collider>();

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        switch (currentPlayer)
            {
            case Player.Player1:
                gameObject.name = "Player1";
                horizontal = "Player1Horizontal";
                vertical = "Player1Vertical";
                fire = "Player1Fire";
                thisMat = p1Mat;
                break;

            case Player.Player2:
                gameObject.name = "Player2";
                horizontal = "Player2Horizontal";
                vertical = "Player2Vertical";
                fire = "Player2Fire";
                thisMat = p2Mat;
                break;

            case Player.Player3:
                gameObject.name = "Player3";
                horizontal = "Player3Horizontal";
                vertical = "Player3Vertical";
                fire = "Player3Fire";
                thisMat = p3Mat;
                break;
            }
	}
	
	// Update is called once per frame
	void Update () {
        
        int count;

        count = zones.Count;

        if (count != 0)
            {
            speed = zoneSpeed;
            }
        else
            {
            speed = movementSpeed;
            }

        if (!usePhysics)
            {
            transform.Translate(new Vector3(Input.GetAxis(horizontal), 0, Input.GetAxis(vertical)) * speed * Time.deltaTime);
            }

        if (isReady)
            {
            if (Input.GetButtonDown(fire))
                {
                GameObject nodeCurrent;
                nodeCurrent = Instantiate(node, new Vector3(transform.position.x, 0, transform.position.z), transform.rotation);
                nodeCurrent.GetComponent<NodeScript>().owner = gameObject.name;
                nodeCurrent.GetComponent<Renderer>().material = thisMat;
                nodeCurrent.transform.GetChild(0).GetComponent<Renderer>().material = thisMat;
                nodeCurrent.transform.GetChild(1).GetComponent<Renderer>().material = thisMat;
                StartCoroutine(Cooldown());
                }
            }
	}

    private void LateUpdate()
        {
        if (usePhysics)
            {
            Vector3 empty = new Vector3(0, 0, 0);
            Vector3 movement = new Vector3(Input.GetAxis(horizontal), 0, Input.GetAxis(vertical));
            rb.velocity = movement * speed;
            if (movement != empty)
                {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.5f);
                anim.SetBool("isMoving", true);
                }
            else
                {
                anim.SetBool("isMoving", false);
                }
            }
        }

    private void OnTriggerEnter(Collider other)
        {
        if (other.gameObject.name == "Zone" && other.transform.parent.GetComponent<NodeScript>().owner == gameObject.name)
            {
            zones.Add(other);
            }
        }

    private void OnTriggerExit(Collider other)
        {
        if (zones.Contains(other))
            {
            zones.Remove(other);
            }
        }

    IEnumerator Cooldown()
        {
        isReady = false;
        yield return new WaitForSeconds(cooldown);
        isReady = true;
        }
    }
