using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Bullet")]
    [SerializeField] float bulletVerticalSpeed = 10f;
    [SerializeField] float bulletHorizontalSpeed = 0f;
    

    PlayerMovement player;
    Rigidbody2D myRigidbody;
    Transform transforms;

    float xSpeed;
    
    void Start(){

        myRigidbody = GetComponent<Rigidbody2D>();
        transforms = GetComponent<Transform>();
        player = FindObjectOfType<PlayerMovement>();
        xSpeed = player.transform.localScale.x * bulletVerticalSpeed;
    }


    void Update(){
        
        transforms.localScale = new Vector2 (player.transform.localScale.x/2,0.5f);
        myRigidbody.velocity = new Vector2 (xSpeed,bulletHorizontalSpeed);
    }


    void OnTriggerEnter2D(Collider2D other) {

        if(other.tag == "Enemy"){

            Destroy(other.gameObject);
        }
        Destroy(gameObject);
    }


    void OnCollisionEnter2D(Collision2D other) {

        Destroy(gameObject);    
    }
}
