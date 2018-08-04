using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MLAgents;

public class GOAgent : Agent
{

    public enum Team
    {
        white, black
    }

    public string MyTeam = "white";
    public float MyScore = 0;
    public float MLScore = 0;
    public float TotalScore = 0;
    public int wins = 0;
    public float Moves = 0;
    public int Rounds = 1;
    public int pre_Pos = -1;
    public float MaxInput = 323; //zero is counted so 18*18=324 so MaxInput=323
    public int AvialableSpacesCount = 0;
    public bool[] AvialableSpaces;
    public bool[] MySpaces;//not used
    public bool[] EnemySpaces;//not used
    public int SelectedSquare;
    public int SelectedSquareTens = -1;//not used
    public int SelectedSquareOnes = -1;//not used
    public bool MyTurn = true;

    public List<GameObject> MyTiles;
    public GameObject[] m_Tiles;
    public GameObject Tile;
    public GameObject EnemyBrain;
    public bool AI = false;//not used
    public float boardOffset = 4f;
    public GameObject[] boardPositions;
    public Text InfoLabel;
    public Text ScoreLabel;
    //public InputField InputNumber;
    public Material[] boardPosMats;
    public int EnemySelection;
    public int previousNum;
    public bool flag = false;
    // Use this for initialization
    void Start()
    {
        boardPositions = GameObject.FindGameObjectsWithTag("boardpos");
        for (int i = 0; i < AvialableSpaces.Length; i++)
        {
            AvialableSpaces[i] = true;
        }
    }
    void Update()
    {

    }
    public bool areAllFalse()
    {
        foreach (bool b in AvialableSpaces) if (b) return false;
        return true;
    }



    public override void InitializeAgent()
    {
        //Debug.Log("InitializeAgent");
        boardPositions = GameObject.FindGameObjectsWithTag("boardpos");
        for (int i = 0; i < AvialableSpaces.Length; i++)
        {
            AvialableSpaces[i] = true;
        }

    }
    public override void CollectObservations()
    {
        //Debug.Log("CollectObservations");
        AddVectorObs(EnemySelection);//observe enemy selection
        AddVectorObs(SelectedSquare);
        // AddVectorObs();//observe turn?
    }
    public override void AgentAction(float[] vectorAction, string textAction)
    {

        if (!MyTurn)
            return;

        if (areAllFalse())
        {
            AgentReset();
            EnemyBrain.GetComponent<GOAgent>().AgentReset();
        }
        if (MLScore <= -30)//forfiet game
        {
            AddScore(-50);
            //AgentReset();
            //EnemyBrain.GetComponent<GOAgent>().AgentReset();
        }

        if (vectorAction[0] < 0)
        {
            // AddScore(-0.05f);
            return;
        }
        else
        {
            //AddScore(0.05f);
        }
        var key1 = (int)(Mathf.Clamp(vectorAction[0], 0, MaxInput / 1000) * 1000);//0-324
        if (MaxInput < 100)
            key1 = (int)(Mathf.Clamp(vectorAction[0], 0, MaxInput / 100) * 100);//0-80

        int postion = 0;
        postion = key1;
        //new method
        if (true)
        {
            postion = (int)vectorAction[0] * 1;// + (int)vectorAction[1] * 2 + (int)vectorAction[2] * 4 + (int)vectorAction[3] * 8 + (int)vectorAction[4] * 16 + (int)vectorAction[5] * 32 + (int)vectorAction[6] * 64 + (int)vectorAction[7] * 128 + (int)vectorAction[8] * 256;
        }


        Debug.Log(vectorAction[0] + MyTeam + postion);
        if (postion > MaxInput || !AvialableSpaces[postion])//invalid input
        {
            AddScore(-0.05f);
            return;
        }
        else
        {
            AddScore(0.2f);

        }
        if (pre_Pos == postion)
        {
            AddScore(-0.2f);
        }
        else
        {
            AddScore(0.05f);
        }



        boardPositions[SelectedSquare].GetComponent<MeshRenderer>().material = boardPosMats[0];
        boardPositions[postion].GetComponent<MeshRenderer>().material = boardPosMats[1];
        SelectedSquare = postion;

        if (true)
        {
            if (AvialableSpaces[postion])
            {
                //AddScore(0.5f * AvialableSpacesCount/100);
                AvialableSpaces[postion] = false;
                if (gameObject.name.Contains("white"))
                {
                    //GameObject EnemyBrain = GameObject.Find("blackAgent");
                    EnemyBrain.GetComponent<GOAgent>().AvialableSpaces[postion] = false;
                    EnemyBrain.GetComponent<GOAgent>().EnemySelection = postion;
                    EnemyBrain.GetComponent<GOAgent>().MyTurn = true;

                }
                else
                {
                    //GameObject EnemyBrain = GameObject.FindGameObjectWithTag("whiteAgent");
                    EnemyBrain.GetComponent<GOAgent>().AvialableSpaces[postion] = false;
                    EnemyBrain.GetComponent<GOAgent>().EnemySelection = postion;
                    EnemyBrain.GetComponent<GOAgent>().MyTurn = true;
                }

                AvialableSpacesCount += 2;
                GameObject GO = Instantiate(Tile, boardPositions[postion].transform.position, Quaternion.identity);
                GO.GetComponent<TileLogic>().MyBrain = gameObject;
                GO.GetComponent<TileLogic>().EnemyBrain = EnemyBrain;
                GO.GetComponent<TileLogic>().MyPosition = postion;
                MyTurn = false;
                Moves++;
                Debug.Log("SelectedSquare: " + postion);
                int tempint = postion;
                if (InfoLabel.text.Length <= 600)
                {
                    InfoLabel.text += "Selected: " + tempint + "\n";
                }
                else
                {
                    InfoLabel.text = "Selected: " + tempint + "\n";
                }

                flag = false;



            }
            else //selected a used space
            {
                // AddScore(-0.5f/AvialableSpacesCount);
            }
        }
    }
    public void AddScore(float score)
    {

        AddReward(score);
        if (score >= 1)
        {
            if (InfoLabel.text.Length <= 400)
            {
                InfoLabel.text += "Scored: " + score + " points" + "\n";
            }
            else
            {
                InfoLabel.text = "Scored: " + score + " points" + "\n";
            }
            MyScore += score;
        }

        MLScore += score;
        ScoreLabel.text = MyTeam + "\n" + " ML Score: " + MLScore + "\n" + " Game Score: " + MyScore + "\n" + " Avg Score: " + TotalScore / Rounds + "\n" + " Wins: " + wins + "\n" + " Moves: " + Moves;
    }
    private static GameObject GetEnemyBrain(GameObject EnemyBrain)
    {
        return EnemyBrain;
    }


    public override void AgentReset()
    {
        m_Tiles = GameObject.FindGameObjectsWithTag(MyTeam);
        foreach (GameObject GO in m_Tiles)
        {
            Destroy(GO);
        }
        Rounds++;
        TotalScore += MyScore;
        if (MyScore >= EnemyBrain.GetComponent<GOAgent>().MyScore)
        {
            wins++;
        }
        MyScore = 0;
        MLScore = 0;
        Moves = 0;
        for (int i = 0; i < AvialableSpaces.Length; i++)
        {
            AvialableSpaces[i] = true;
        }

    }

}
