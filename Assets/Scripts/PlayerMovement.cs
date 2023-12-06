using System.Timers;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class PlayerMovement : MonoBehaviour{
    

    [Header("Speed")]
    [SerializeField] float runSpeed = 7f;
    [SerializeField] float jumpSpeed = 20f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] float defaultGravity = 6f;
    [SerializeField] float zeroGravity = 0f;
    [SerializeField] Vector2 deathKick = new Vector2 (20f,20f);

    [Header("Bullet")]
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;
    [SerializeField] AudioClip gunShotSFX;
    [SerializeField] GameObject playerPos;
    
    
    Vector2 moveInput;
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;

    
    bool isAlive = true;


    void Start(){

        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        
    }

    
    void Update(){
        
        if(!isAlive){
            return;
        }   
        
        Run();
        FlipSprite();
        ClimbLadder();
        Die();
    }


    void OnFire(InputValue Value){

        if(!isAlive){
            return;
        }
        Instantiate(bullet, gun.position, transform.rotation);
        AudioSource.PlayClipAtPoint(gunShotSFX,playerPos.transform.position);
    }


    void OnMove(InputValue value){
        
        if(!isAlive){
            return;
        }

        moveInput = value.Get<Vector2>();
        //Debug.Log(moveInput);
    }


    void OnJump(InputValue value){
    
        if(!isAlive){
            return;
        }

        if(!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))){

            return;
        }

        if(value.isPressed){

            myRigidbody.velocity += new Vector2(0f,jumpSpeed);
        }
    }


    void Run(){

        Vector2 playerVelocity = new Vector2(moveInput.x*runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("isRunning",playerHasHorizontalSpeed);
    }


    void FlipSprite(){

        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;

        if(playerHasHorizontalSpeed){
            
            transform.localScale = new Vector2 (Mathf.Sign(myRigidbody.velocity.x), 1f);
        }
    }


    void ClimbLadder(){
        
        
        if(!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing"))){
            
            myAnimator.SetBool("isClimbing",false);
            myRigidbody.gravityScale = defaultGravity;
            return;
        }

        
        Vector2 climbVelocity = new Vector2 (myRigidbody.velocity.x, moveInput.y*climbSpeed);
        myRigidbody.velocity = climbVelocity; 
        myRigidbody.gravityScale = zeroGravity;
        bool playerHasVerticalSpeed = Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("isClimbing",playerHasVerticalSpeed);
    }


    void Die(){
    
        if(myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemies","Hazards"))){
            
            isAlive = false;
            myAnimator.SetTrigger("Dying");
            myRigidbody.velocity = deathKick;
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }
}  
