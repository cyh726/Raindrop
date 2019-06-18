using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockManager : MonoBehaviour
{
    [Header("Fish Settings")]
    //[Range(0.0f, 5.0f)]
    //public float minSpeed;
    //[Range(0.0f, 5.0f)]
    //public float maxSpeed;
    [Range(0.0f, 2.0f)]
    public float neighbourDistance;
    [Range(0.0f, 5.0f)]
    public float rotationSpeed;

    public GameObject fishPrefab;                    //公共變量
    public GameObject fishPrefab2;
    public GameObject fishPrefab3;
    public int numFish;
    public GameObject[] allFish;                     //檢查器窗口更改魚的數量。然後創建一個allFish數組。當他們被實例化時，他們會去，也就是魚的數量將進入allFish數組
    public GameObject[] allFish2;
    public Vector3 swimLimits = new Vector3(2.2f, 2.5f, 0);
    public Vector3[] prepos;
    public Vector3[] prepos2;
    int num=0;
    // Use this for initialization
    void Start()
    {
        prepos = new Vector3[numFish / 4];
        prepos2 = new Vector3[numFish / 4];
        allFish = new GameObject[numFish];
        allFish2 = new GameObject[numFish/2];
        Vector3 small = new Vector3(0.06f + Random.Range(-0.05f, 0.01f), 0.06f + Random.Range(-0.01f, 0.1f), 0.1f);
        Vector3 big = new Vector3(0.12f + Random.Range(-0.03f, 0.05f), 0.15f + Random.Range(-0.03f, 0.05f), 0.1f);
        for (int i = 0; i < numFish/4; i++)
        {
            Vector3 pos = new Vector3(this.transform.position.x + Random.Range(-swimLimits.x, swimLimits.x), -this.transform.position.y - Random.Range(-swimLimits.y, swimLimits.y), -3);
            allFish[i] = (GameObject)Instantiate(fishPrefab, pos, Quaternion.identity);
            allFish[i].GetComponent<Flock>().myManager = this;
            prepos[i] = pos;
            allFish[i].transform.localScale = big;
        }
        for (int i = 0; i < numFish / 4*3; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-2.2f, 2.2f), Random.Range(-0.8f, 2.7f), -3.0f);
            allFish[i+ numFish / 4] = (GameObject)Instantiate(fishPrefab2, pos, Quaternion.identity);
            allFish[i+ numFish / 4].GetComponent<Flock>().myManager = this;
            allFish[i + numFish / 4].transform.localScale = small;
        }
        for (int i = 0; i < numFish / 2; i++)
        {
            Vector3 pos = new Vector3(3, 3, -3.0f);
            allFish2[i] = (GameObject)Instantiate(fishPrefab3, pos, Quaternion.identity);
            //
            //allFish2[i].GetComponent<Flock>().myManager = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (num %20==10)
        {
            for (int i = 0; i < numFish / 4; i++)
            {
                //prepos2[i] = new Vector3(allFish2[i].transform.position.x, allFish2[i].transform.position.y,-3);
                //allFish2[i].transform.position = prepos[i];
                prepos[i] = allFish[i].transform.position;
                allFish2[i + numFish / 4].transform.position = prepos2[i];
               
            }

        }
        else if(num % 20 == 0)
        {
            for (int i = 0; i < numFish / 4; i++)
            {
                prepos2[i] = allFish[i].transform.position;
                allFish2[i].transform.position = prepos[i];        
         
            }
        }
        for (int i = 0; i < numFish / 4; i++)
        {
            allFish2[i + numFish / 4].transform.position += new Vector3(0, -0.001f, 0.01f);
            allFish2[i].transform.position += new Vector3(0, -0.001f, 0.01f);
        }
        num++;
    }
}