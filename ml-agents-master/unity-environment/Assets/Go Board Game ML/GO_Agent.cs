using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MLAgents;
public class Go_Agent : MonoBehaviour
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
    void Start()
    {

    }

}