using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Randomobstacle : MonoBehaviour {

    public Transform[] SpawnPoints;
    public Transform wall;
    // Use this for initialization
    void Start() {
        RandomLocation();
    }

    // Update is called once per frame
    void Update() {

    }
    public void RandomLocation(){
       var index = Random.Range(0, SpawnPoints.Length);
        wall.transform.position = SpawnPoints[index].position;

   }
}
