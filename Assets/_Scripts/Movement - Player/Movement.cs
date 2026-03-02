using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    //Vars 
    private float horizontalMovement = 5f;
    [SerializeField] private float jumpForce = 5f;
    private float speed = 5f;
    Rigidbody2D rb;
    private PlayerCombat combat;

    //Bools
    private bool movingLeft;
    private bool movingRight;
    [SerializeField] private bool grounded;

    //Grounded Check
    [Header("Grounded Check")]
    public LayerMask groundLayers;
    public Transform groundCheckPoint;
    public Vector2 groundCheckSize = new Vector2(0.5f, 0.1f);

    //Animator
    [Header("Animator")]
    Animator anim;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }



    private void Update()
    {
        //Handle Sprite Direction based on which way player is facing
        //Also handle movement based on horizontalMovement variable. 
        if (movingLeft)
        {
            horizontalMovement = -1f;
            FlipSprite(true);
        }
        else if (movingRight)
        {
            horizontalMovement = 1f;
            FlipSprite(false);
        }
        else
        {
            horizontalMovement = 0f;
        }

        rb.linearVelocity = new Vector2(horizontalMovement * speed, rb.linearVelocity.y);


        //GROUND CHECK
        grounded = Physics2D.OverlapBox(groundCheckPoint.position, groundCheckSize, 0f, groundLayers);

        //Connect animations
        anim.SetFloat("Speed", Mathf.Abs(horizontalMovement));

        anim.SetBool("Grounded", grounded);
    }

    
    public void FlipSprite(bool faceLeft)
    {
        //Get the current scale of the object
        Vector3 scale = transform.localScale;

        //Flip the player based on the direction
        if (faceLeft && scale.x > 0f || !faceLeft && scale.x < 0f)
        {
            //Flip the player horizontally
            scale.x *= -1f;
            transform.localScale = scale;
        }
    }

    public void OnJump()
    {
        //Ensure player is on the floor before jumping 
        if(grounded == true)
        {
            //Play Anim First
            anim.SetTrigger("Jump");

            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            grounded = false; 
        }
    }

    public void OnAttack()
    {
        //This function simply plays the animation. 
        //In the animation theres an animation event that calls the function to give damage. 

        anim.SetTrigger("Attack");
    }

    private void OnLeft(InputValue value)
    {
        movingLeft = value.isPressed;
    }

    private void OnRight(InputValue value)
    {
        movingRight = value.isPressed;
    }

    
    

}
