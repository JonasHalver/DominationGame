using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneScript : MonoBehaviour {

    public GameObject node;
    public float area;

	// Use this for initialization
	void Start () {
        node = transform.parent.parent.gameObject;
        MatchManager.zones.Add(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
        area = Mathf.PI * Mathf.Pow(transform.localScale.x/4, 2); 
	}

    private void OnTriggerEnter(Collider other)
        {
        if (other.gameObject.name == "Zone")
            {
            Debug.Log("IsOverlapping");
            node.SendMessage("StopGrowing");
            }
        }
    }
