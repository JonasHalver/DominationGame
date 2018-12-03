using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeScript : MonoBehaviour {

    private SphereCollider zoneCol;
    private GameObject zone;
    private GameObject terrain;
    public string owner;
    public GameObject space, virus, snow;

    public float timeToMaxSize = 10f;
    public float zoneMaxSize = 20f;
    public float terrainHeighChange = 5f;

    public Color p1Color, p2Color, p3Color;

    public GameObject environmentAsset1, environmentAsset2, environmentAsset3;
    private List<GameObject> assets = new List<GameObject>();
    private GameObject currentObject;
    public float assetYOffset = -1f;
    public float maxObjects = 5;
    public float delayMin = 2f, delayMax = 5f;
    //private float placedObjects; 

    private IEnumerator grow;
    private IEnumerator objects;

	// Use this for initialization
	void Start () {

        //space = transform.Find("SpaceFlag").gameObject;
        //virus = transform.Find("VirusFlag").gameObject;
        //snow = transform.Find("SnowFlag").gameObject;

        switch (owner)
            {
            case "Player3":
                snow.SetActive(true);
                zone = snow.transform.Find("Zone").gameObject;
                terrain = snow.transform.Find("Terrain").gameObject;
                break;
            case "Player1":
                virus.SetActive(true);
                zone = virus.transform.Find("Zone").gameObject;
                terrain = virus.transform.Find("Terrain").gameObject;
                break;
            case "Player2":
                space.SetActive(true);
                zone = space.transform.Find("Zone").gameObject;
                terrain = space.transform.Find("Terrain").gameObject;
                break;
            }
        //zone = transform.GetChild(0).gameObject;
        //terrain = transform.GetChild(1).gameObject;
        zoneCol = zone.GetComponent<SphereCollider>();

        grow = Grow();
        //objects = Objects();
        StartCoroutine(grow);
        //StartCoroutine(objects);
        //assets.Add(environmentAsset1);
        //assets.Add(environmentAsset2);
        //assets.Add(environmentAsset3);

        
        }
	
	// Update is called once per frame
	void Update () {
        //currentObject.transform.position = new Vector3(currentObject.transform.position.x, Mathf.Lerp(currentObject.transform.position.y, 1, 10 * Time.deltaTime), currentObject.transform.position.z);
        }

    IEnumerator Grow()
        {
        for (float f = 2.5f; f < zoneMaxSize; f = f + ((zoneMaxSize / timeToMaxSize)/10))
            {
            //zoneCol.radius = f;
            zone.transform.localScale = new Vector3(f, f, f);

            Vector3 ttp = transform.position;
            transform.position = new Vector3(ttp.x, ttp.y + ((terrainHeighChange / timeToMaxSize)/zoneMaxSize), ttp.z);

            yield return new WaitForSeconds(0.1f);
            }
        }

    public void StopGrowing()
        {
        StopCoroutine(grow);
        }

    //IEnumerator Objects()
    //    {
    //    for (float f = 0; f <= maxObjects; f++)
    //        { 
    //        yield return new WaitForSeconds(Random.Range(delayMin, delayMax));
    //
    //        float area = zone.transform.localScale.x - 1;
    //        Vector3 spawnPos = new Vector3(Random.Range((area/4)*-1, area / 4), assetYOffset, Random.Range((area/4)*-1, area / 4));
    //        currentObject = Instantiate(assets[Random.Range(0, 3)], transform, false);
    //        currentObject.transform.localPosition = spawnPos;
    //        //placedObjects++;
    //        //currentObject.transform.position = new Vector3(currentObject.transform.position.x, Mathf.Lerp(currentObject.transform.position.y, 1, 10 * Time.deltaTime), currentObject.transform.position.z);
    //        //yield return new WaitForSeconds(delayMin);
    //        }
            
    //    }

    private void OnTriggerStay(Collider other)
        {
        if (other.gameObject != zone && other.gameObject.name == "Zone" && other.GetComponent<ZoneScript>().area > zone.GetComponent<ZoneScript>().area)
            {
            Debug.Log("In Zone");
            Destroy(gameObject);
            }
        }
    }
