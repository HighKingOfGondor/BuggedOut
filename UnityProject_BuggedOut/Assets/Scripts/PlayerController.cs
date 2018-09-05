using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent((typeof(Rigidbody2D)))]
public class PlayerController : MonoBehaviour
{

    Rigidbody2D rb;
    public AudioClip powerUpAudio;
    public float speedBase = 0.1f;
    public bool invulnerable = false;
    Animator anim;
    SpriteRenderer sprite;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (LevelManager.instance.isPlaying)
        {
            //rb.MovePosition(rb.position + new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * speedBase);
            rb.velocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * speedBase;
            sprite.flipX = rb.velocity.x > 0;
            anim.SetBool("Walking", (rb.velocity.x != 0 || rb.velocity.y != 0));
        }        
    }

    void OnTriggerEnter2D (Collider2D other) {
        if (other.gameObject.tag == "EnergyDrink") {
            speedBase = 20f;
            AudioManager.instance.PlayClipLocalSpace(powerUpAudio);
            StartCoroutine (PowerUpSpeed());
            Destroy(other.gameObject);
        } else if (other.gameObject.tag == "Chips") {
            LevelManager.instance.healthCurrent++;
            AudioManager.instance.PlayClipLocalSpace(powerUpAudio);
            Destroy(other.gameObject);
            sprite.color = Color.yellow;
        } else if (other.gameObject.tag == "Pills") {
            AudioManager.instance.PlayClipLocalSpace(powerUpAudio);
            invulnerable = true;

            StartCoroutine(PowerUpPills());
            Destroy(other.gameObject);
        } else if (other.gameObject.tag == "Web") {
            AudioManager.instance.PlayClipLocalSpace(powerUpAudio);
            this.GetComponent<Collider2D>().enabled = false;
            StartCoroutine(PowerUpPhase());
            Destroy(other.gameObject);
        } else if (other.gameObject.tag == "Code") {
            LevelManager.instance.stabilityCurrent += 0.25f;
            StartCoroutine(PowerUpSpeed());
            Destroy(other.gameObject);
        }
    }

    IEnumerator PowerUpSpeed () {
        yield return new WaitForSeconds (2f);
        speedBase = 5f;
    }
    IEnumerator PowerUpPills() {
        yield return new WaitForSeconds(2f);
        sprite.color = Color.white;
        invulnerable = false;
    }
    IEnumerator PowerUpPhase (){
        yield return new WaitForSeconds(2f);
        this.GetComponent<CircleCollider2D>().enabled = true;
    }
}
