using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Coin : MonoBehaviour{

    [SerializeField] AudioClip coinPickupSFX;
    [SerializeField] GameObject playerPos;
    [SerializeField] int addScore=100;
    bool wasCollected=false;
    
    
    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag=="Player" && !wasCollected){
            wasCollected=true;
            FindObjectOfType<GameSession>().AddToScore(addScore);
            AudioSource.PlayClipAtPoint(coinPickupSFX,playerPos.transform.position);
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
    
}
