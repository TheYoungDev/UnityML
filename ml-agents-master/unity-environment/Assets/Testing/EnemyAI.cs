using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

    public bool IsCheckPointBased = false;
    public bool IsRandomDirection = true;
    public float Speed = 2f;
    public Vector3 direction;
    public Transform[] CheckPoints;
    public Transform CurrentCheckPoint;
    public float UpdateRate = 5;
    private float nextUpdate;

    // Use this for initialization
    void Start () {
        direction = new Vector3(Random.Range(-1.0f, 1.0f), 0.0f, Random.Range(-1.0f, 1.0f));
        if (IsCheckPointBased)
            UpdateCheckPoint();
    }
   /* private void OnCollisionEnter(Collision collision)
    {
        if (IsRandomDirection)
        {
            if (collision.gameObject.tag.Contains("wall")){
                direction = new Vector3(Random.Range(-1.0f, 1.0f), 0.0f, Random.Range(-1.0f, 1.0f));
            }
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (IsRandomDirection)
        {
            if (collision.transform.gameObject.tag.Contains("wall"))
            {
                direction = new Vector3(Random.Range(-1.0f, 1.0f), 0.0f, Random.Range(-1.0f, 1.0f));
            }
        }
    }*/
    // Update is called once per frame
    void Update () {
        if (IsRandomDirection) { 

            transform.position += direction * Speed * Time.deltaTime;
            if(Time.time > nextUpdate)
            {
                nextUpdate = Time.time + UpdateRate;
                direction = new Vector3(Random.Range(-1.0f, 1.0f), 0.0f, Random.Range(-1.0f, 1.0f));
            }
            RaycastHit hit;
            if (Physics.Raycast(transform.position, direction, out hit, Mathf.Infinity))
            {
               /* Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                if(Vector3.Distance(hit.transform.position,transform.position) <= 1f)
                {
                    if (hit.transform.gameObject.CompareTag("wall"))
                    {
                        direction = new Vector3(Random.Range(-1.0f, 1.0f), 0.0f, Random.Range(-1.0f, 1.0f));
                    }
                }
                /*
               /* if (hit.transform.gameObject.CompareTag("Player"))
                {

                }*/
            }
        }
        if (IsCheckPointBased)
        {
            if (Vector3.Distance(transform.position, CurrentCheckPoint.position) <= 1f)
                UpdateCheckPoint();
            else
            {
                // The step size is equal to speed times frame time.
                float step = Speed * Time.deltaTime;

                // Move our position a step closer to the target.
                transform.position = Vector3.MoveTowards(transform.position, CurrentCheckPoint.position, step);
            }

        }

    }
    public void UpdateCheckPoint()
    {
        var index = Random.Range(0, CheckPoints.Length);
        CurrentCheckPoint = CheckPoints[index];
    }
}
