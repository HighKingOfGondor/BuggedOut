using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

    public float attackSpace = 0.8f;
    GameObject player;
    health playerHealth;
    bool playerInRange;
    float timer;

	// Use this for initialization
	void Awake () {
        player = GameObject.FindGameObjectWithTag("player");
        playerHealth = playerHealth.GetComponent<health> ();
        playerInRange = false;
	}

    void OnTriggerEnter (Collider obj) {
        if (obj == player) {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider obj) {
        if (obj == player) {
            playerInRange = false;
        }
    }

    // Update is called once per frame
    void Update () {
        timer = Time.deltaTime;
        if (timer >= attackSpace && playerInRange) {
            Attack();
        }
        //animation stuff needed
	}

    void Attack () {
        timer = 0f;
        playerHealth.takeDamage();
    }
}
