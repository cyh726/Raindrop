using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{

    public FlockManager myManager;
    float speed;
    float startTime;
    void Start()
    {
        speed = Random.Range(1.0f,5.0f);
        startTime = Time.time;
        //smoothTime = Random.Range(1.0f, 3.0f);
    }
    int num = 1;
    // Update is called once per frame
    void Update()
    {
        //transform.position = new Vector3(transform.position.x, transform.position.y - 0.01f, -3);
        //transform.Translate(0, -Time.deltaTime, 0);
        float t = Time.time;//Time.deltaTime; //(Time.time - startTime) / 3.0f;
        //float t2 = 1+Time.time-(num-1)*5;
        float t2 = Random.Range(1, 6) ;
        float x = (Random.Range(0.03f, 0.08f) - .005f) * .8f;//sin(3 * w)*pow(sin(w), 6)*.45;
        //float w = transform.position.y * 0.2f;
        // x += (.4f - Mathf.Abs(t)) * Mathf.Sin(t) * .8f;
        x -= Mathf.Sin(1.01f*x) * .8f;
        //float y = -Mathf.Sin(t)-Mathf.Sin(t + Mathf.Sin(t + Mathf.Sin(t))*.5f) * .25f;
        //float y = -Mathf.Sin(t) - Mathf.Sin(t + Mathf.Sin(t+ Mathf.Sin(1.03f*t + 6.1234f)) * .5f) * .25f + 6.1234f;
        //y -= (transform.position.x - x); * (transform.position.x - x);
        //y = -t;
        //transform.Translate(x, y, 0);
        //float t = (Time.time - startTime) / duration;
        //float y = Mathf.SmoothStep(minimum, maximum, t);
        //float y2 = -0.1f*(Mathf.Log(Mathf.Log(t)) + 0.1f);
        //float x = (Random.Range(0.03f, 0.08f) - .005f) * .8f;
        float y2 = Mathf.Log(1+Mathf.Pow(1/(3*t2),1.2f));

        if (transform.localScale.x > 0.1f)
        {
            //transform.Translate(x, -Random.Range(0.001f, 0.008f) * Mathf.Abs(transform.position.y - y) * (Mathf.Pow(2, 10 * (transform.localScale.x - 0.12f))), 0);
            if (t2>=1 && t2 <= 6)
            {
                transform.Translate(x, -1*y2, 0);
            }
            /*else if (t2 >= 15 || t2<=0.3)
            {
                transform.Translate(x, -Random.Range(0.001f, 0.003f), 0);
            }*/
        }
        else
        {
            transform.Translate(0, -t* Random.Range(0.001f, 0.003f), 0);
        }
       

        if (transform.position.y < -3)
        {
            
            if (transform.localScale.x > 0.1f)
            {
                Vector3 big = new Vector3(0.12f + Random.Range(-0.04f, 0.04f), 0.15f + Random.Range(-0.04f, 0.04f), 0.1f);
                transform.position = new Vector3(Random.Range(-2.5f, 2.5f), 2.4f + Random.Range(0f, 8f), -3);
                transform.localScale = big;
            }
            else
            {
                Vector3 small = new Vector3(0.06f + Random.Range(-0.05f, 0.01f), 0.06f + Random.Range(-0.01f, 0.1f), 0.1f);
                transform.position = new Vector3(Random.Range(-2.5f, 2.5f), 2.4f + Random.Range(0f, 4f), -3);
                transform.localScale = small;
            }
        }
        
        /*
        transform.Translate(0, -Time.deltaTime , 0);
        num++;
        if (num>300)
        {
            transform.position = new Vector3(Random.Range(-4.0f,4.0f), 2.4f + Random.Range(0f, 6.0f), -3);
            num = 0;
        }*/
        ApplyRules();

    }
    void ApplyRules()
    {
        GameObject[] gos;
        gos = myManager.allFish;

        Vector3 vcentre = Vector3.zero;
        Vector3 vavoid = Vector3.zero;
        float gSpeed = 0.01f;
        float nDistance;
        int groupSize = 0;
        Vector3 small = new Vector3(0.06f + Random.Range(-0.05f, 0.01f), 0.06f + Random.Range(-0.02f, 0.1f), 0.1f);
        //Vector3 small_pos = new Vector3(Random.Range(-2.2f, 2.2f), Random.Range(-0.8f, 2.7f), -3.0f);
        Vector3 big = new Vector3(0.12f + Random.Range(-0.04f, 0.04f), 0.15f + Random.Range(-0.04f, 0.04f), 0.1f);
        foreach (GameObject go in gos)
        {
            if (go != this.gameObject)
            {
                nDistance = Vector3.Distance(go.transform.position, this.transform.position);
                if (nDistance <= myManager.neighbourDistance)
                {
                    vcentre += go.transform.position;
                    groupSize++;

                    if (nDistance < 0.08f * (1 + transform.localScale.x))//0.15f*(1+transform.localScale.x))
                    {
                        if (transform.localScale.x > go.transform.localScale.x)
                        {
                            if (go.transform.localScale.x > 0.1f)
                            {
                                go.transform.position = new Vector3(Random.Range(-2.5f, 2.5f), 2.4f + Random.Range(0f, 6f), -3);
                                go.transform.localScale = big;
                                transform.localScale += new Vector3((1 + .01f * (Mathf.Abs(go.transform.localScale.x - 0.1f))) * 0.001f, (1 + .01f * (Mathf.Abs(go.transform.localScale.y - 0.1f))) * 0.003f, 0);
                            }
                            else
                            {
                                go.transform.position = transform.position+ new Vector3(0, 0.5f, 0);
                                go.transform.localScale = small;
                                transform.localScale += new Vector3((1 +.01f * (Mathf.Abs(go.transform.localScale.x - 0.1f))) * 0.001f, (1 +.01f * (Mathf.Abs(go.transform.localScale.y - 0.1f))) * 0.003f, 0);
                            }

                        }
                        else {

                            if (transform.localScale.x > 0.1f)
                            {
                                transform.position = new Vector3(Random.Range(-2.5f, 2.5f), 2.4f + Random.Range(0f, 6f), -3);
                                transform.localScale = big;
                                //go.transform.localScale += new Vector3(0.001f, 0.003f, 0);
                                go.transform.localScale += new Vector3((1 + .01f * (Mathf.Abs(transform.localScale.x - 0.1f))) * 0.001f, (1 + .01f * (Mathf.Abs(transform.localScale.y - 0.1f))) * 0.003f, 0);
                            }
                            else
                            {
                                transform.position = go.transform.position + new Vector3(0, 0.5f, 0);
                                transform.localScale = small;
                                //go.transform.localScale += new Vector3(0.001f, 0.003f, 0);
                                go.transform.localScale += new Vector3((1 + .01f * (Mathf.Abs(transform.localScale.x - 0.1f))) * 0.001f, (1 + .01f * (Mathf.Abs(transform.localScale.y - 0.1f))) * 0.003f, 0);
                            }
                                
                        }                   
                    }

                    Flock anotherFlock = go.GetComponent<Flock>();
                    gSpeed = gSpeed + anotherFlock.speed;
                }
            }
        }

        if (groupSize > 0)
        {
            vcentre = vcentre / groupSize;
            speed = gSpeed / groupSize;

            Vector3 direction = vcentre - transform.position;
            if (direction != Vector3.zero)
                transform.rotation = Quaternion.Slerp(transform.rotation,
                                                      Quaternion.LookRotation(direction),
                                                      myManager.rotationSpeed * Time.deltaTime);
            transform.localRotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, 0);
            transform.position = new Vector3(transform.position.x, transform.position.y, -3);
        }
    }
}