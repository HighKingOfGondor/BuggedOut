using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    float nextPath;

    public float delayAttack = 1f;
    float nextAttack = 0f;

    public float speedMove = 1f;

    public State stateCurrent;

    public List<Vector3> pathCurrent = new List<Vector3>();

    Rigidbody2D rb;

    public bool playerInRange;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (LevelManager.instance.isPlaying)
        {
            DoActions();
            CheckTransitions();
            Attack();
        }
    }

    void Attack()
    {
        if (playerInRange && Time.time > nextAttack)
        {
            nextAttack = Time.time + delayAttack;
            LevelManager.instance.healthCurrent--;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.GetComponentInParent<PlayerController>() != null)
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.transform.GetComponentInParent<PlayerController>() != null)
        {
            playerInRange = false;
        }
    }

    void DoActions()
    {
        foreach (var i in stateCurrent.actions)
        {
            i.DoAction(this);
        }
    }

    void CheckTransitions()
    {
        foreach (var i in stateCurrent.transitions)
        {
            foreach (var j in i.conditions)
            {
                if (j.CheckCondition(this))
                {
                    stateCurrent = i.state;
                    return;
                }
            }
        }
    }


    public void UpdatePath(float pathDelay,Vector3 target)
    {
        if (Time.time > nextPath)
        {
            nextPath = Time.time + pathDelay;
            pathCurrent = PathfindingManager.instance.GetPath(Vector3Int.RoundToInt(this.transform.position), Vector3Int.RoundToInt(target));
            pathCurrent[0] = transform.position;

            // TODO make this settable
            if (pathCurrent.Count > 1)
            {
                pathCurrent.Insert(1, transform.position + (pathCurrent[1] - transform.position) * (0.5f * (pathCurrent[1] - transform.position).magnitude));
            }            
        }
    }

    public void FollowPath()
    {
        if (pathCurrent.Count > 0)
        {            
            if (Vector3.Distance(transform.position, pathCurrent[0]) < 0.01f)
            {
                pathCurrent.RemoveAt(0);
            }

            if (pathCurrent.Count > 0)
            {
                rb.MovePosition(Vector2.MoveTowards(rb.position, new Vector2(pathCurrent[0].x, pathCurrent[0].y), speedMove));
            }            
        }
        for (var i = 1; i < pathCurrent.Count; i++)
        {
            Debug.DrawLine(pathCurrent[i - 1], pathCurrent[i], Color.blue);
        }
    }
}
