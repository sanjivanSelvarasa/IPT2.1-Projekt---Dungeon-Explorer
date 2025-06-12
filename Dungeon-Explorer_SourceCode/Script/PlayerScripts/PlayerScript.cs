using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float playerSpeed = 5;
    private Rigidbody2D rb;

    [HideInInspector] public float xAxis;
    [HideInInspector] public float yAxis;

    // Animation Image
    public Animator anim;

    // Player Health
    public HealthScript healthScript;
    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        healthScript.SetMaxHealth();
    }

    void Update()
    {
        // get inputs from player
        xAxis = Input.GetAxis("Horizontal");
        yAxis = Input.GetAxis("Vertical");

        MoveSwitchImage();
        Attack();
    }

    public void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(xAxis * playerSpeed, yAxis * playerSpeed);
    }

    private void MoveSwitchImage()
    {
        if (Input.GetKey(KeyCode.W))
        {
            anim.SetBool("IsButtonU", true);
            anim.SetBool("IsButtonD", false);
            anim.SetBool("IsButtonS", false);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            anim.SetBool("IsButtonD", true);
            anim.SetBool("IsButtonU", false);
            anim.SetBool("IsButtonS", false);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            anim.SetBool("IsButtonS", true);
            transform.localScale = new Vector3(1, 1, 1);
            anim.SetBool("IsButtonU", false);
            anim.SetBool("IsButtonD", false);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            anim.SetBool("IsButtonS", true);
            transform.localScale = new Vector3(-1, 1, 1);
            anim.SetBool("IsButtonU", false);
            anim.SetBool("IsButtonD", false);
        }
        else { 
            anim.SetBool("IsButtonU", false);
            anim.SetBool("IsButtonD", false);
            anim.SetBool("IsButtonS", false);
        }
    }

    private void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetBool("IsAttack", true);
        }else
            anim.SetBool("IsAttack", false);
    }
}
