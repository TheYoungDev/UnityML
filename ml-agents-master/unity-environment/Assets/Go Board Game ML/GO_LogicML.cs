using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine;
using MLAgents;
public class Go_LogicML : Agent
{

    public string MyTeam = "white";
    public int MyScore = 0;
    public bool[] AvialableSpaces;
    public bool[] MySpaces;
    public bool[] EnemySpaces;
    public int SelectedSquare;
    public int SelectedSquareTens = -1;
    public int SelectedSquareOnes = -1;
    public bool MyTurn = true;

    public List<GameObject> MyTiles;
    public GameObject[] m_Tiles;
    public GameObject Tile;
    public GameObject EnemyBrain;
    public bool AI = false;
    public float boardOffset = 4f;
    public GameObject[] boardPositions;
    public Text InfoLabel;
    public InputField InputNumber;
    public Material[] boardPosMats;
    public int EnemySelection;
    public int previousNum;
    public bool flag = false;

    // Use this for initialization
    void Start () {
        boardPositions = GameObject.FindGameObjectsWithTag("boardpos");
        for(int i=0;i< AvialableSpaces.Length;i++)
        {
            AvialableSpaces[i] = true;
        }
    }

    public override void InitializeAgent()
    {
        boardPositions = GameObject.FindGameObjectsWithTag("boardpos");
        for (int i = 0; i < AvialableSpaces.Length; i++)
        {
            AvialableSpaces[i] = true;
        }

    }

    public override void CollectObservations()
    {
        AddVectorObs(EnemySelection);//observe enemy selection
        AddVectorObs(SelectedSquare);//observer previous move
       
    }

    public override void AgentAction(float[] vectorAction, string textAction)
    {
        if (!MyTurn)
            return;

        var key1 = (int)vectorAction[0];
        var key2 = (int)vectorAction[1];


        Debug.Log(SelectedSquare + MyTeam);


        if (key1 == 1)//increment
        {

            if (SelectedSquare == 80)
            {
                SelectedSquare = 0;
            }
            else
            {
                SelectedSquare += 1;
            }
            if (SelectedSquare > 0)
            {
                boardPositions[SelectedSquare - 1].GetComponent<MeshRenderer>().material = boardPosMats[0];
                boardPositions[SelectedSquare].GetComponent<MeshRenderer>().material = boardPosMats[1];
            }
            else
            {
                boardPositions[80].GetComponent<MeshRenderer>().material = boardPosMats[0];
                boardPositions[SelectedSquare].GetComponent<MeshRenderer>().material = boardPosMats[1];
            }

        }
        if (key1 == 2)
        {
            if (AvialableSpaces.Length >= SelectedSquare && AvialableSpaces[SelectedSquare])
            {
                AddReward(0.2f);
                AvialableSpaces[SelectedSquare] = false;
                // MySpaces[SelectedSquare] = true;
                // EnemyBrain.GetComponent<Go_Logic>().EnemySpaces[SelectedSquare] = true;
                EnemyBrain.GetComponent<Go_Logic>().AvialableSpaces[SelectedSquare] = false;
                GameObject GO = Instantiate(Tile, boardPositions[SelectedSquare].transform.position, Quaternion.identity);
                MyTurn = false;
                EnemyBrain.GetComponent<Go_Logic>().MoveMade(SelectedSquare);

                Debug.Log("SelectedSquare: " + SelectedSquare);
                int tempint = SelectedSquare;
                InfoLabel.text += " Selected: " + tempint + "\n";
                flag = false;
                EnemyBrain.GetComponent<Go_Logic>().MyTurn = true;


            }
            else //selected a used space
            {
                AddReward(-0.01f);
            }
        }
        else if (key1 == 0)//did nothing shouldent happen
        {
            AddReward(-0.05f);
        }

    }

    public override void AgentReset()
    {
        m_Tiles = GameObject.FindGameObjectsWithTag(MyTeam);
        foreach (GameObject GO in m_Tiles)
        {
            Destroy(GO);
        }

    }






    // Update is called once per frame
    void Update()
    {
        /*if (MyTurn && !flag) //start of tuern
        {
            MoveMade();
        }*/
        /*if (MyTurn)
            TakeTurn();

        */

    }

    public void TakeTurn()
    {

        if (Input.GetKey(KeyCode.W))
        {
            if (SelectedSquare <= 79)
            {
                ++SelectedSquare;
            }
            else
            {
                SelectedSquare = 0;
            }

        }
        if (Input.GetKeyUp(KeyCode.KeypadEnter))
        {
            if (AvialableSpaces.Length >= SelectedSquare && AvialableSpaces[SelectedSquare])
            {
                AvialableSpaces[SelectedSquare] = false;
                MySpaces[SelectedSquare] = true;
                EnemyBrain.GetComponent<Go_Logic>().EnemySpaces[SelectedSquare] = true;
                EnemyBrain.GetComponent<Go_Logic>().AvialableSpaces[SelectedSquare] = false;
                GameObject GO = Instantiate(Tile, boardPositions[SelectedSquare].transform.position, Quaternion.identity);
                //MyTiles.Add(GO);

                MyTurn = false;
                EnemyBrain.GetComponent<Go_Logic>().MoveMade(SelectedSquare);

                Debug.Log("SelectedSquare: " + SelectedSquare);
                int tempint = SelectedSquare;
                InfoLabel.text += " Selected: " + tempint + "\n";
                flag = false;
                EnemyBrain.GetComponent<Go_Logic>().MyTurn = true;
                //check if captured

            }
        }
        /*
                    if (!AI)
                {
                    InputNumber.ActivateInputField();
                }*/



        if (Input.GetKeyUp(KeyCode.KeypadEnter) && !AI)
        {
            if (AvialableSpaces.Length >= SelectedSquare && AvialableSpaces[SelectedSquare])
            {
                AvialableSpaces[SelectedSquare] = false;
                MySpaces[SelectedSquare] = true;
                EnemyBrain.GetComponent<Go_Logic>().EnemySpaces[SelectedSquare] = true;
                EnemyBrain.GetComponent<Go_Logic>().AvialableSpaces[SelectedSquare] = false;
                GameObject GO = Instantiate(Tile, boardPositions[SelectedSquare].transform.position, Quaternion.identity);
                //MyTiles.Add(GO);

                MyTurn = false;
                EnemyBrain.GetComponent<Go_Logic>().MoveMade(SelectedSquare);

                Debug.Log("SelectedSquare: " + SelectedSquare);
                int tempint = SelectedSquare;
                InfoLabel.text += " Selected: " + tempint + "\n";
                flag = false;
                EnemyBrain.GetComponent<Go_Logic>().MyTurn = true;
                //check if captured

            }

        }

    }
    public void AddScore(float score)
    {
        AddReward(score);
        //MyScore += score;
        //Debug.Log("Captured: " + score + " Tiles");
        InfoLabel.text += "Score: " + score + "\n";
    }

    public void MoveMade(int _enemyselection)
    {

        EnemySelection = _enemyselection;
        Debug.Log("Enemy of " + MyTeam + " went " + EnemySelection);
        /*m_Tiles = GameObject.FindGameObjectsWithTag(MyTeam);
        foreach (GameObject GO in m_Tiles)
        {
            GO.GetComponent<TileLogic>().MoveMade();
        }
        flag = true;*/
    }
    public void TextChanged()
    {
        if (InputNumber.text != "")
        {


            var temp = int.Parse(InputNumber.text);
            if (temp <= 81 && temp >= 0)
            {
                boardPositions[SelectedSquare].GetComponent<MeshRenderer>().material = boardPosMats[0];
                SelectedSquare = temp;
                boardPositions[SelectedSquare].GetComponent<MeshRenderer>().material = boardPosMats[1];


            }
        }
    }
    public void LightUpSelection()
    {
        if (InputNumber.text == "")
        {

        }
        else
        {
            var temp = int.Parse(InputNumber.text) - 1;
            if (temp <= 81 && temp >= 0)
            {
                if (previousNum != temp)
                {
                    boardPositions[previousNum].GetComponent<MeshRenderer>().material = boardPosMats[0];
                    previousNum = temp;
                    boardPositions[previousNum].GetComponent<MeshRenderer>().material = boardPosMats[1];
                }



            }
        }
    }
    public void LightUp()
    {








    }
    
    
    /*--------------------------------------
    Code for playing with two human users
    */-------------------------------------
    /*
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

    }*/
}
/*
if (!AI) { 
    //tens
    if (Input.GetKeyUp(KeyCode.Alpha0))
    {
        SelectedSquareTens = 0;
    }
    if (Input.GetKeyUp(KeyCode.Alpha1))
    {
        SelectedSquareTens = 1;
    }
    if (Input.GetKeyUp(KeyCode.Alpha2))
    {
        SelectedSquareTens = 2;
    }
    if (Input.GetKeyUp(KeyCode.Alpha3))
    {
        SelectedSquareTens = 3;
    }
    if (Input.GetKeyUp(KeyCode.Alpha4))
    {
        SelectedSquareTens = 4;
    }
    if (Input.GetKeyUp(KeyCode.Alpha5))
    {
        SelectedSquareTens = 5;
    }
    if (Input.GetKeyUp(KeyCode.Alpha6))
    {
        SelectedSquareTens = 6;
    }
    if (Input.GetKeyUp(KeyCode.Alpha7))
    {
        SelectedSquareTens = 7;
    }
    if (Input.GetKeyUp(KeyCode.Alpha8))
    {
        SelectedSquareTens =8;
    }
    if (Input.GetKeyUp(KeyCode.Alpha9))
    {
        SelectedSquareTens = 9;
    }

    //ones
    if (Input.GetKeyUp(KeyCode.Keypad0))
    {
        SelectedSquareOnes = 0;
    }
    if (Input.GetKeyUp(KeyCode.Keypad1))
    {
        SelectedSquareOnes = 1;
    }
    if (Input.GetKeyUp(KeyCode.Keypad2))
    {
        SelectedSquareOnes = 2;
    }
    if (Input.GetKeyUp(KeyCode.Keypad3))
    {
        SelectedSquareOnes = 3;
    }
    if (Input.GetKeyUp(KeyCode.Keypad4))
    {
        SelectedSquareOnes = 4;
    }
    if (Input.GetKeyUp(KeyCode.Keypad5))
    {
        SelectedSquareOnes = 5;
    }
    if (Input.GetKeyUp(KeyCode.Keypad6))
    {
        SelectedSquareOnes = 6;
    }
    if (Input.GetKeyUp(KeyCode.Keypad7))
    {
        SelectedSquareOnes = 7;
    }
    if (Input.GetKeyUp(KeyCode.Keypad8))
    {
        SelectedSquareOnes = 8;
    }
    if (Input.GetKeyUp(KeyCode.Keypad9))
    {
        SelectedSquareOnes = 9;
    }
    SelectedSquare = SelectedSquareOnes + SelectedSquareTens * 10;

}*/
/* var temp = int.Parse(InputNumber.text);
 if (SelectedSquare != temp)
 {

     boardPositions[SelectedSquare].GetComponent<MeshRenderer>().material = boardPosMats[0];
     SelectedSquare = temp;
     boardPositions[SelectedSquare].GetComponent<MeshRenderer>().material = boardPosMats[1];

     InfoLabel.text += "Player: " + MyTeam + " Selected: "+ SelectedSquare;

 }*/
