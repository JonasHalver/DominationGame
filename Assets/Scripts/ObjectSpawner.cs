using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour {

    public float maxObjects = 5f, delayMin = 2f, delayMax = 5f, assetYOffset = -1f, objectFinalPos = 1f;
    private GameObject currentObject, zone;
    public GameObject environmentAsset1, environmentAsset2, environmentAsset3;
    private List<GameObject> assets = new List<GameObject>();

    private IEnumerator objects;

    // Use this for initialization
    void Start () {
        assets.Add(environmentAsset1);
        assets.Add(environmentAsset2);
        assets.Add(environmentAsset3);

        objects = Objects();
        zone = transform.Find("Zone").gameObject;
        StartCoroutine(objects);
	}
	
	// Update is called once per frame
	void Update () {
        currentObject.transform.position = new Vector3(currentObject.transform.position.x, Mathf.Lerp(currentObject.transform.position.y, objectFinalPos, 10 * Time.deltaTime), currentObject.transform.position.z);
        }

    IEnumerator Objects()
        {
        for (float f = 0; f <= maxObjects; f++)
            { 
            yield return new WaitForSeconds(Random.Range(delayMin, delayMax));
    
            float area = zone.transform.localScale.x - 1;
            Vector3 spawnPos = new Vector3(Random.Range((area/4)*-1, area / 4), assetYOffset, Random.Range((area/4)*-1, area / 4));
            currentObject = Instantiate(assets[Random.Range(0, 3)], transform, false);
            currentObject.transform.localPosition = spawnPos;
            //placedObjects++;
            //currentObject.transform.position = new Vector3(currentObject.transform.position.x, Mathf.Lerp(currentObject.transform.position.y, 1, 10 * Time.deltaTime), currentObject.transform.position.z);
            //yield return new WaitForSeconds(delayMin);
            }

        }
    }
