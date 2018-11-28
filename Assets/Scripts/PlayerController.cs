using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private string horizontal, vertical, fire;
    public enum Player { Player1, Player2, Player3 };
    public Player currentPlayer = Player.Player1;

    public GameObject node;

    public float movementSpeed = 8f;
    public float zoneSpeed = 12f;

    private List<Collider> zones = new List<Collider>();

	// Use this for initialization
	void Start () {
        switch (currentPlayer)
            {
            case Player.Player1:
                gameObject.name = "Player1";
                horizontal = "Player1Horizontal";
                vertical = "Player1Vertical";
                fire = "Player1Fire";
                break;

            case Player.Player2:
                gameObject.name = "Player2";
                horizontal = "Player2Horizontal";
                vertical = "Player2Vertical";
                fire = "Player2Fire";
                break;

            case Player.Player3:
                gameObject.name = "Player3";
                horizontal = "Player3Horizontal";
                vertical = "Player3Vertical";
                fire = "Player3Fire";
                break;
            }
	}
	
	// Update is called once per frame
	void Update () {
        float speed;
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

        transform.Translate(new Vector3(Input.GetAxis(horizontal), 0, Input.GetAxis(vertical)) * speed * Time.deltaTime);

        if (Input.GetButtonDown(fire))
            {
            GameObject nodeCurrent;
            nodeCurrent = Instantiate(node, new Vector3(transform.position.x, 0, transform.position.z), transform.rotation);
            nodeCurrent.GetComponent<NodeScript>().owner = gameObject.name; 
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
    }
