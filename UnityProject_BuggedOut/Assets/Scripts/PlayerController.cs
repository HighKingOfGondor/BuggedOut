using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent((typeof(Rigidbody2D)))]
public class PlayerController : MonoBehaviour
{

    Rigidbody2D rb;
    public AudioClip powerUpAudio;
    public float speedBase = 0.1f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (LevelManager.instance.isPlaying)
        {
            //rb.MovePosition(rb.position + new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * speedBase);
            rb.velocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * speedBase;
        }        
    }

    void OnTriggerEnter2D (Collider2D other) {
        if (other.gameObject.tag == "EnergyDrink") {
            speedBase = 20f;
            AudioManager.instance.PlayClipLocalSpace(powerUpAudio);
            StartCoroutine (PowerUpSpeed());
            Destroy(other.gameObject);
        } else if (other.gameObject.tag == "Chips") {
            if (LevelManager.instance.healthCurrent == 1) {
                LevelManager.instance.healthCurrent++;
                AudioManager.instance.PlayClipLocalSpace(powerUpAudio);
                Destroy(other.gameObject);
            }
        } else if (other.gameObject.tag == "Pills") {
            AudioManager.instance.PlayClipLocalSpace(powerUpAudio);
            Destroy(other.gameObject);
        }
    }

    IEnumerator PowerUpSpeed () {
        yield return new WaitForSeconds (2f);
        speedBase = 10f;
    }
}
