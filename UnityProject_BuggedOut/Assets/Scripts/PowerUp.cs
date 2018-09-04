using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {

    PlayerController player;

	// Use this for initialization
	void Awake () {
        player = GetComponent<PlayerController>();
	}

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject == player) {
            Debug.Log("Here");
            Destroy(gameObject);
            Effect();
        }
    }

    IEnumerator Effect () {
        player.speedBase = 20;
        yield return new WaitForSeconds(4);
        player.speedBase = 10;
    }
}

