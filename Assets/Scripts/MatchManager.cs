using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchManager : MonoBehaviour {

    public static List<GameObject> zones = new List<GameObject>();

    public IEnumerator coroutine;

    public float totalArea = 0;
    public float areaThreshold = 1000f;

    public int zoneCount;

	// Use this for initialization
	void Start () {
        coroutine = AreaChecker();
        StartCoroutine(coroutine);
	}
	
	// Update is called once per frame
	void Update () {
        zoneCount = zones.Count;
	}

    IEnumerator AreaChecker()
        {
        while (true)
            {
            for (int i = 0; i < zones.Count; i++)
                {
                totalArea = totalArea + zones[i].GetComponent<ZoneScript>().area;
                }
            yield return new WaitForEndOfFrame();
            if (totalArea > areaThreshold)
                {

                }
            else
                {
                Debug.Log("No winner, " + totalArea + "/" + areaThreshold);
                totalArea = 0;
                }

            yield return new WaitForSeconds(1f);
            }
        }
}
