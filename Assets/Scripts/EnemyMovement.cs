using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemyMovement : MonoBehaviour{
    

    [SerializeField] float moveSpeed = 1f;
    Transform myTransform;
    Rigidbody2D myRigidbody;

    
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myTransform = GetComponent<Transform>();
    }

    
    void Update()
{
        myRigidbody.velocity = new Vector2 (moveSpeed,0f);
    }

    void OnTriggerEnter2D(Collider2D other) {

        if(other.tag != "Player"){
            myTransform.transform.localScale *= new Vector2 (-1,1);
            moveSpeed = -moveSpeed;
        }
    }
}
