using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeScript : MonoBehaviour {

    private SphereCollider zoneCol;
    private GameObject zone;
    private GameObject terrain;
    public string owner;

    public float timeToMaxSize = 10f;
    public float zoneMaxSize = 20f;
    public float terrainHeighChange = 5f;

    public Color p1Color, p2Color, p3Color;

    private IEnumerator grow;

	// Use this for initialization
	void Start () {
        zone = transform.GetChild(0).gameObject;
        terrain = transform.GetChild(1).gameObject;
        zoneCol = zone.GetComponent<SphereCollider>();

        grow = Grow();
        StartCoroutine(grow);
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    IEnumerator Grow()
        {
        for (float f = 2.5f; f < zoneMaxSize; f = f + ((zoneMaxSize / timeToMaxSize)/10))
            {
            //zoneCol.radius = f;
            zone.transform.localScale = new Vector3(f, f, f);

            Vector3 ttp = terrain.transform.position;
            terrain.transform.position = new Vector3(ttp.x, ttp.y + ((terrainHeighChange / timeToMaxSize)/zoneMaxSize), ttp.z);

            yield return new WaitForSeconds(0.1f);
            }
        }

    public void StopGrowing()
        {
        StopCoroutine(grow);
        }

    private void OnTriggerStay(Collider other)
        {
        if (other.gameObject != zone && other.gameObject.name == "Zone" && other.GetComponent<ZoneScript>().area > zone.GetComponent<ZoneScript>().area)
            {
            Debug.Log("In Zone");
            Destroy(gameObject);
            }
        }
    }
