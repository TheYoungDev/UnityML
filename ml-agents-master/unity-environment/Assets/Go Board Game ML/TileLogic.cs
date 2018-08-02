using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileLogic : MonoBehaviour {

    public int MyPosition =-1;
    public string MyTag= "tag";
    public List<GameObject> MyChain;
    public string EnemyTag = "white";
    public List<GameObject> EnemyChain;
    // Use this for initialization
    void Start () {
        MyChain = new List<GameObject>();
        MyChain.Add(gameObject);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void checkChain()
    {
        foreach(GameObject Tile in MyChain)
        {

        }
    }
    public void OnTriggerEnter(Collider other)
    {
        GameObject Go = other.gameObject;
        if (Go.tag == MyTag)
        {
            MyChain.Add(Go);
        }
        if (Go.tag == EnemyTag)
        {
            MyChain.Add(Go);
        }
    }
    public void CheckNextTo()
    {
        RaycastHit hit;
        // Forward
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit,5f))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);
            Debug.Log("Did Hit");
            if(hit.transform.gameObject.tag == MyTag)
            {

            }
        }
        else
        {
            Debug.Log("EmptySquare");
        }


        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 5f))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);
            
        }
    }
}
