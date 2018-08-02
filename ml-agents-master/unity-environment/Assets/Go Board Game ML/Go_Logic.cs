using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Go_Logic : MonoBehaviour {

    public bool[] AvialableSpaces;
    public bool[] MySpaces;
    public bool[] EnemySpaces;
    public int SelectedSquare;
    public int SelectedSquareRow;
    public int SelectedSquareCol;
    public bool MyTurn = true;


    public GameObject Tile;
    public GameObject EnemyBrain;
    public bool AI = false;
    public float boardOffset = 4f;
    public Transform[] boardPositions;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (MyTurn)
            TakeTurn();


    }
    public void TakeTurn()
    {
        if (Input.GetButton("Fire1") || AI)
        {
            if (AvialableSpaces.Length >= SelectedSquare && AvialableSpaces[SelectedSquare])
            {
                AvialableSpaces[SelectedSquare] = false;
                MySpaces[SelectedSquare] = true;
                EnemyBrain.GetComponent<Go_Logic>().EnemySpaces[SelectedSquare] = true;
                EnemyBrain.GetComponent<Go_Logic>().AvialableSpaces[SelectedSquare] = false;
               GameObject GO= Instantiate(Tile, boardPositions[SelectedSquare].position, Quaternion.identity);

                //check if captured

            }

        }
        
    }
    public void CheckCapture()
    {

    }
    public void CaptureSqaure()
    {

    }

    public void ListEnemySpaces()
    {

    }
    public void ListMySpaces()
    {

    }
}
