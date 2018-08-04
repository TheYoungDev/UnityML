using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileLogic : MonoBehaviour {

    public int MyPosition =-1;
    public string MyTag= "tag";
    public List<GameObject> MyChain;
    public string EnemyTag = "white";
    public List<GameObject> EnemyChain;
    public int liberties = 4;
    public bool Captured = true;
    public GameObject EnemyBrain;
    public GameObject MyBrain;
    public bool CapFlag = false;
    public Material CapMat;
    public bool CapDestroy = true;
    public bool disabled = false;
    // Use this for initialization
    void Start () {
        MyChain = new List<GameObject>();
        MyChain.Add(gameObject);
    }

	void Update () {
        if(!CapFlag)
            checkChain();

    }
    public void MoveMade()
    {

    }

    public void OnTriggerStay(Collider other)
    {
        GameObject Go = other.gameObject;
        if (Go.tag == MyTag)
        {
            var temp = Go.GetComponent<TileLogic>().MyChain;
            foreach (GameObject Tile in temp)
            {

                if (!MyChain.Contains(Tile))
                {
                    MyChain.Add(Tile);
                }

            }
        }
    }

    public void checkChain()
    {
        Captured = true;
        foreach(GameObject Tile in MyChain)
        {
            if(Tile.GetComponent<TileLogic>().disabled == true)
            {
                MyChain.Remove(Tile);
                break;
            }
            if (Tile.GetComponent<TileLogic>().disabled == false && Tile.GetComponent<TileLogic>().liberties > 0)
            {
                Captured = false;
            }
        }
       
        if (Captured)
        {
            Debug.Log("Name: " + gameObject.name + " Cap: " + Captured + " Team: " + MyTag);
            if (EnemyBrain == null)
            {
                return;
            }
           EnemyBrain.GetComponent<GOAgent>().AddScore(1f);
           MyBrain.GetComponent<GOAgent>().AddScore(-0.5f);
           CapFlag = true;
           gameObject.GetComponent<MeshRenderer>().material = CapMat;
            if (CapDestroy) { 

                Debug.Log("destory");
                EnemyBrain.GetComponent<GOAgent>().AvialableSpaces[MyPosition] =true;
                MyBrain.GetComponent<GOAgent>().AvialableSpaces[MyPosition] = true;
                gameObject.GetComponent<MeshRenderer>().enabled = false;
                var temp = gameObject.GetComponents<BoxCollider>();
                foreach(BoxCollider col in temp)
                {
                    col.enabled = false;
                }
                gameObject.GetComponent<SphereCollider>().enabled =false;
                disabled = true;
                //gameObject.SetActive(false);
            }
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        GameObject Go = other.gameObject;
        if (Go.tag == MyTag)
        {
            if (!MyChain.Contains(Go))
            {
                MyChain.Add(Go);
            }
            
            liberties -= 1;
        }
        if (Go.tag == EnemyTag)
        {
            liberties -= 1;
        }
        if (Go.tag == "wall")
        {
            liberties -= 1;
        }
    }

}
