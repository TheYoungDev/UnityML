//Detect when the orange block has touched the goal. 
//Detect when the orange block has touched an obstacle. 
//Put this script onto the orange block. There's nothing you need to set in the editor.
//Make sure the goal is tagged with "goal" in the editor.

using UnityEngine;

public class GoalDetect : MonoBehaviour
{
    //[HideInInspector]
    /// <summary>
    /// The associated agent.
    /// This will be set by the agent script on Initialization. 
    /// Don't need to manually set.
    /// </summary>
	public PushAgentBasic agent;  //

    public Transform[] CheckPoints;
    public Transform CurrentCheckPoint;
    public float speed = 1;


    void OnCollisionEnter(Collision col)
    {
        // Touched goal.
        /*if (col.gameObject.CompareTag("goal"))
        {
            agent.IScoredAGoal();
        }*/
       /* if (col.gameObject.CompareTag("enemy"))
        {
            agent.ILoseAGoal();
        }*/
    }
    public void Update()
    {
       /* if (Vector3.Distance(transform.position, CurrentCheckPoint.position) <= 1f)
            UpdateCheckPoint();
        else
        {
            // The step size is equal to speed times frame time.
            float step = speed * Time.deltaTime;

            // Move our position a step closer to the target.
            transform.position = Vector3.MoveTowards(transform.position, CurrentCheckPoint.position, step);
        }*/
            


    }
    public void UpdateCheckPoint()
    {
        var index = Random.Range(0, CheckPoints.Length);
        CurrentCheckPoint = CheckPoints[index];
    }
    private void Start()
    {
       // UpdateCheckPoint();
    }
}
