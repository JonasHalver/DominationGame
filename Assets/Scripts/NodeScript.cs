using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeScript : MonoBehaviour {

    private SphereCollider zoneCol;
    private GameObject zone;
    public string owner;

    public Color p1Color, p2Color, p3Color;

    private IEnumerator grow;

	// Use this for initialization
	void Start () {
        zone = transform.GetChild(0).gameObject;
        zoneCol = zone.GetComponent<SphereCollider>();

        grow = Grow();
        StartCoroutine(grow);
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    IEnumerator Grow()
        {
        for (float f = 1f; f < 20f; f = f + 0.1f)
            {
            //zoneCol.radius = f;
            zone.transform.localScale = new Vector3(f, f, f);
            yield return new WaitForSeconds(0.1f);
            }
        }

    public void StopGrowing()
        {
        StopCoroutine(grow);
        }

    private void OnTriggerStay(Collider other)
        {
        if (other.gameObject != zone && other.gameObject.name == "Zone")
            {
            Debug.Log("In Zone");
            Destroy(gameObject);
            }
        }
    }
