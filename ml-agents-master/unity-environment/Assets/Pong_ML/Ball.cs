using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    public float speed = 5f;
    public Vector3 direction = new Vector3(1, 0, 1);

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(direction * speed * Time.deltaTime);
	}
    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Change Dir");
        if(collision.gameObject.name.Contains("Vert"))
            direction.z = -direction.z;
        if (collision.gameObject.name.Contains("Hor"))
            direction.x = -direction.x;
    }
}
