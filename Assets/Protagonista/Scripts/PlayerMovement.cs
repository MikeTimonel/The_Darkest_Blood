using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PotionCount potions;
    public float life;
    [SerializeField] float maxHealth;
    private Rigidbody2D rb2D;
    public Animator Protagonista;
    public BoxCollider2D avariciacollider;
    public Avariciascript avariciascript;
    public GameOver GameOverScreen;
    [SerializeField] private int sceneNumber;

    [Header("Ataque")]
    
    [SerializeField] private Transform AtaqueC;
    [SerializeField] private float radioAtaque;
    private bool canAttack = true;
    [SerializeField] private int dañoAtaque;
    private float attackCooldown = 0.09f;
    private bool isAttacking;

    [Header("Movimiento")]

    private float horizontalMovement = 0f;
   
    [SerializeField] public float speedMovement;

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
    private float dashingPower = 26f;
    private float dashingTime = 0.33f;
    private float dashingCooldown = 0.87f;

    [Header("Sonidos")]

    [SerializeField] private AudioSource reproducir;
    [SerializeField] private AudioClip audioCaminar;
    [SerializeField] private AudioClip audioCaminar2;
    [SerializeField] private AudioClip audioAtaque;
    [SerializeField] private AudioClip audioSalto;
    [SerializeField] private AudioClip audioDaño1;
    [SerializeField] private AudioClip audioDaño2;
    [SerializeField] private AudioClip audioDaño3;
    [SerializeField] private AudioClip audioMuerte;
    [SerializeField] private AudioClip audioDash;
    [SerializeField] private AudioClip audioPocion;
    

    // Start is called before the first frame update
    void Start()
    {
        potions = FindObjectOfType<PotionCount>();
        maxHealth = life;
        Protagonista = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        reproducir = GetComponent<AudioSource>();       
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
            reproducir.PlayOneShot(audioDash);
            StartCoroutine(Dash());
            
        }
        if (Input.GetButtonDown("Fire1") && canAttack)
        {
            Protagonista.SetBool("Idle", false);
            reproducir.PlayOneShot(audioAtaque);
            Attack();
            
        }
    }

    void FixedUpdate(){

        if(isDashing){
            return;
        }
        isOnGround = Physics2D.OverlapBox(groundController.position, sizeBox, 0f, typeGround);
        //Moven't
        Move(horizontalMovement * Time.fixedDeltaTime, jump);

        jump = false;
    }

    private void Attack()
    {
        StartCoroutine(Ataques());

        Collider2D[] objetos = Physics2D.OverlapCircleAll(AtaqueC.position, radioAtaque);
        foreach (Collider2D colisionador in objetos)
        {
            if(colisionador.CompareTag("Jefe"))
            {
                colisionador.transform.GetComponent<Avariciascript>().TomarDaño(dañoAtaque);
                if(avariciascript.Vida <= 0)
                {
                    Destroy(avariciacollider);
                    Finalnivel();
                }
            }
            if (colisionador.CompareTag("Hongo")) {
                colisionador.transform.GetComponent<Hongo>().TomarDaño(dañoAtaque);
            }
            if (colisionador.CompareTag("Ojo")) {
                colisionador.transform.GetComponent<Ojo>().TomarDaño(dañoAtaque);
            }
            if (colisionador.CompareTag("Slime"))
            {
                colisionador.transform.GetComponent<Slime>().TomarDaño(dañoAtaque);
            }
        }
    }
    public async void TomarDaño(float daño)
    {
        int sonidoDaño;
        if (isDashing == true)
        {
            daño = 0;
        } else if (life <= 0)
        {
            Protagonista.SetBool("Death", true);
            await Task.Delay(1700);
            GameOverScreen.Setup(sceneNumber);
            reproducir.PlayOneShot(audioMuerte);
        }
        else
        {
            Protagonista.SetBool("Idle", false);
            Protagonista.SetBool("Hit", true);
            sonidoDaño = Random.Range(1,4);
            switch (sonidoDaño) {
                case 1:
                    reproducir.PlayOneShot(audioDaño1);
                    break;
                case 2:
                    reproducir.PlayOneShot(audioDaño2);
                    break;
                case 3:
                    reproducir.PlayOneShot(audioDaño3);
                    break;
            }



            StartCoroutine(Esperar());
        }
        life -= daño;
    }
    private void Finalnivel()
    {
        //Protagonista.SetBool("Final", true);
        speedMovement = 0;
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
            reproducir.PlayOneShot(audioSalto);

        }
        if (isOnGround == true && move ==0 && isAttacking ==false)
        {
            Protagonista.SetBool("Fall", false);
            Protagonista.SetBool("Run", false);
            Protagonista.SetBool("Idle", true);
            Protagonista.SetBool("Jump", false);
            


        }
        else if (isOnGround == true && move != 0)
        {
            Protagonista.SetBool("Run", true);
            Protagonista.SetBool("Idle", false);
            Protagonista.SetBool("Jump", false);
            Protagonista.SetBool("Fall", false);
            
            
            
        }
        else if(isOnGround == false)
        {
            Protagonista.SetBool("Run", false);
            Protagonista.SetBool("Fall", true);
            


        }
    }
    private void Healing()
    {
        if (Input.GetKeyDown(KeyCode.Q) && potions.potions > 0 && life < maxHealth)
        {
            life += 25;
            potions.potions--;
            reproducir.PlayOneShot(audioPocion);

        }
    }
    private void Rotate(){
        isRightMove = !isRightMove;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale; 
    }

    private void OnDrawGizmos(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(AtaqueC.position, radioAtaque);
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
        Protagonista.SetBool("Dash", false);
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
    private IEnumerator Ataques()
    {
        canAttack = false;
        isAttacking = true;
        Protagonista.SetBool("Idle", false);
        Protagonista.SetBool("Attack", true);
        yield return new WaitForSeconds(0.4f);
        Protagonista.SetBool("Attack", false);
        isAttacking = false;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }
    private IEnumerator Esperar()
    {
        yield return new WaitForSeconds(0.09f);
        Protagonista.SetBool("Hit", false);
    }

    public void reproducirCaminata1() {
        reproducir.PlayOneShot(audioCaminar);
    }
    public void reproducirCaminata2() {
        reproducir.PlayOneShot(audioCaminar2);
    }
    
}
