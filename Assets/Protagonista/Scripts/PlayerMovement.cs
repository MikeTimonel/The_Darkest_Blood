using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //private PotionCount potions;
    public float life;
    [SerializeField] float maxHealth;
    private Rigidbody2D rb2D;
    public Animator Protagonista;
    
    [Header("Movimiento")]

    private float horizontalMovement = 0f;
   
    [SerializeField] private float speedMovement;

    [Range(0.0f, 0.3f)][SerializeField] private float smoothingMoving;

    [SerializeField] private  Vector3 speed = Vector3.zero;

    private bool isRightMove = true;


    [Header("Salto")]
    [SerializeField] private float jumpForce;
    [SerializeField] private LayerMask typeGround;
    [SerializeField] private Transform groundController;
    [SerializeField] private Vector3 sizeBox;
    [SerializeField] private bool isOnGround;

    private bool jump = false;

    [Header("Dash")]

    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 24f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;



    // Start is called before the first frame update
    void Start()
    {
        //potions = FindObjectOfType<PotionCount>();
        //maxHealth = life;
        Protagonista = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
    }
 
    // Update is called once per frame
    void Update()
    {
        Healing();
        if(isDashing){
            return;
        }

        horizontalMovement = Input.GetAxisRaw("Horizontal") * speedMovement;

        if(Input.GetButtonDown("Jump")){
            jump = true;
        }

        if(Input.GetKeyDown(KeyCode.LeftShift) && canDash){
            StartCoroutine(Dash());
        }
    }

    void FixedUpdate(){

        if(isDashing){
            return;
        }
        isOnGround = Physics2D.OverlapBox(groundController.position, sizeBox, 0f, typeGround);
        //Movent
        Move(horizontalMovement * Time.fixedDeltaTime, jump);

        jump = false;
    }

    private void Move(float move, bool jumped) {
        Vector3 currentSpeed = new Vector2(move, rb2D.velocity.y);
        rb2D.velocity = Vector3.SmoothDamp(rb2D.velocity, currentSpeed, ref speed, smoothingMoving);

        if (move > 0 && !isRightMove)
        {
            Rotate();
            Protagonista.SetBool("Run", true);
            Protagonista.SetBool("Idle", false);
            Protagonista.SetBool("Fall", false);
            Protagonista.SetBool("Jump", false);
        }
        else if (move < 0 && isRightMove)
        {
            Protagonista.SetBool("Idle", false);
            Protagonista.SetBool("Jump", false);
            Protagonista.SetBool("Fall", false);
            Protagonista.SetBool("Run", true);
            Rotate();
        }


        if (isOnGround && jumped){
            isOnGround = false;
            Protagonista.SetBool("Run", false);
            Protagonista.SetBool("Jump", true);
            Protagonista.SetBool("Idle", false);
            rb2D.AddForce(new Vector2(0f, jumpForce));
        }
        if (isOnGround == true && move ==0)
        {
            Protagonista.SetBool("Fall", false);
            Protagonista.SetBool("Run", false);
            Protagonista.SetBool("Idle", true);
            Protagonista.SetBool("Jump", false);
        }else if (isOnGround == true && move != 0)
        {
            Protagonista.SetBool("Run", true);
            Protagonista.SetBool("Idle", false);
            Protagonista.SetBool("Jump", false);
            Protagonista.SetBool("Fall", false);
        }
        //else if(isOnGround == false)
        //{
        //    Protagonista.SetBool("Run", false);
        //    Protagonista.SetBool("Fall", true);
        //}
    }
    private void Healing()
    {
        //if (Input.GetKeyDown("Q") && potions.potions > 0 && life < maxHealth)
        //{
        //    life += 40;
        //    potions.potions--;

        //}
    }
    private void Rotate(){
        isRightMove = !isRightMove;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale; 
    }

    private void OnDrawGizmos(){
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(groundController.position, sizeBox);
    }
    private IEnumerator Dash(){
        canDash = false;
        isDashing = true;
        Protagonista.SetBool("Dash", true);
        Protagonista.SetBool("Idle", false);
        Protagonista.SetBool("Run", false);
        Protagonista.SetBool("Jump", false);
        Protagonista.SetBool("Fall", false);
        float originalGravity = rb2D.gravityScale;
        rb2D.gravityScale = 0f;
        rb2D.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        yield return new WaitForSeconds(dashingTime);
        rb2D.gravityScale = originalGravity;
        isDashing = false;
        Protagonista.SetBool("Dash", false);
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}
