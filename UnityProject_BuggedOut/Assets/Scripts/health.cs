using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class health : MonoBehaviour {

    public int startingHealth = 2;
    public int currentHealth;

    bool isDead;
    bool damage;
	// Use this for initialization
	void Awake () {
        currentHealth = startingHealth;
	}
	

	// Update is called once per frame
	void Update () {
        if (damage) {
            //animation stuff, i.e remove health on UI, damage feedback
        }
        damage = false;
	}

    public void takeDamage() {
        damage = true;
        currentHealth = currentHealth - 1;
        if (currentHealth <= 0 && !isDead) {
            death();
        } 
    }

    public void death () {
        isDead = true;
    }
}
