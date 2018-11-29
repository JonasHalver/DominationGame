using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MatchManager : MonoBehaviour {

    public static List<GameObject> zones = new List<GameObject>();

    public IEnumerator coroutine;

    private GameObject arena;
    public UnityEngine.UI.Text winnerText;
    public GameObject resetText;
    public UnityEngine.UI.Text p1ScoreText;
    public UnityEngine.UI.Text p2ScoreText;
    public UnityEngine.UI.Text pScoreText;

    float p1score = 0, p2score = 0, p3score = 0;

    private float coveredArea = 0;
    private float arenaArea;
    public float areaPercentage = 1f;
    private float areaThreshold;

    public int zoneCount;
    private int sceneIndex;

	// Use this for initialization
	void Start () {
        Time.timeScale = 1;
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        coroutine = AreaChecker();
        StartCoroutine(coroutine);

        arena = GameObject.Find("Arena");
        arenaArea = Mathf.Pow((arena.transform.localScale.x * 10), 2);
        areaThreshold = arenaArea * areaPercentage;
	}
	
	// Update is called once per frame
	void Update () {
        zoneCount = zones.Count;

        if (Input.GetKeyDown("r"))
            {
            SceneManager.LoadScene(sceneIndex);
            }
	}

    IEnumerator AreaChecker()
        {
        while (true)
            {
            for (int i = 0; i < zones.Count; i++)
                {
                if (zones[i] != null)
                    {
                    coveredArea = coveredArea + zones[i].GetComponent<ZoneScript>().area;
                    }
                }
            yield return new WaitForEndOfFrame();
            if (coveredArea > areaThreshold)
                {
                winnerText.text = Winner();
                }
            else
                {
                Debug.Log("No winner, " + coveredArea + "/" + areaThreshold);
                coveredArea = 0;
                }

            yield return new WaitForSeconds(1f);
            }
        }

    public void Score()
        {
        foreach (GameObject zone in zones)
            {
            if (zone != null)
                {
                string zoneOwner = zone.transform.parent.GetComponent<NodeScript>().owner;
                switch (zoneOwner)
                    {
                    case "Player1":
                        p1score = p1score + zone.GetComponent<ZoneScript>().area;
                        break;
                    case "Player2":
                        p2score = p2score + zone.GetComponent<ZoneScript>().area;
                        break;
                    case "Player3":
                        p3score = p3score + zone.GetComponent<ZoneScript>().area;
                        break;
                    }
                }
            }
        //p1ScoreText.text = (p1score /  ToString + " %"
        }

    public string Winner()
        {
        //for (int i = 0; i < zones.Count; i++)
        //foreach (GameObject zone in zones)
        //    {
        //    if (zone != null)
        //        {
        //        string zoneOwner = zone.transform.parent.GetComponent<NodeScript>().owner;
        //        switch (zoneOwner)
        //            {
        //            case "Player1":
        //                p1score = p1score + zone.GetComponent<ZoneScript>().area;
        //                break;
        //            case "Player2":
        //                p2score = p2score + zone.GetComponent<ZoneScript>().area;
        //                break;
        //            case "Player3":
        //                p3score = p3score + zone.GetComponent<ZoneScript>().area;
        //                break;
        //            }
        //        }
        //    }
        Time.timeScale = 0.5f;
        resetText.SetActive(true);

        if (p1score > p2score && p1score > p2score)
            {
            return "Player 1 Wins!";
            }
        else if (p2score > p1score && p2score > p3score)
            {
            return "Player 2 Wins!";
            }
        else if (p3score > p1score && p3score > p2score)
            {
            return "Player 3 Wins!";
            }
        else
            {
            return "No Winner";
            }

        }
}
