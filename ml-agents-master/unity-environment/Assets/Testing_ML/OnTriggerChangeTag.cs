using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerChangeTag : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("agent"))
        {
            gameObject.SetActive(false);
        }
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("agent"))
        {
            gameObject.SetActive(false);
        }
    }
}
