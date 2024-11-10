using UnityEngine;
using UnityEngine.InputSystem;

public class movement : MonoBehaviour
{

    public PlayerInput input;
    public GameObject EButton;
    public GameObject DialogueBox;
    public GameManager gm;

    Vector2 movevector;
    public float moveSpeed = 5f;
    [SerializeField] private Rigidbody2D rb;
    public bool canMove = false;
    bool isFacingRight;

    bool hintRange = false;
    public bool corpseRange = false;

    public bool InteractPressed = false;

    public int currentCorpse;

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!hintRange && corpseRange && !DialogueBox.activeSelf)
        {
            gm.displayCorpse.Invoke();
            
            //Set currentCorpse value
        }
        if (!hintRange && !gm.intro && !corpseRange )
        {
            DialogueBox.SetActive(false);
        }
        if (input.actions["Interact"].WasPressedThisFrame())
        {
            InteractPressed = true;
            if (hintRange && !corpseRange && !DialogueBox.activeSelf)
            {
                gm.displayHints.Invoke();
            }
            
        }
        else
            InteractPressed = false;

        Flip();
    }
    private void FixedUpdate()
    {
        if (canMove)
            rb.linearVelocity = new Vector2(movevector.x * moveSpeed , movevector.y*moveSpeed);
    }
    void OnMove(InputValue input)
    {
        movevector = input.Get<Vector2>();
    }
    private void Flip()
    {
        if (isFacingRight && movevector.x < 0f || !isFacingRight && movevector.x > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
            EButton.GetComponent<SpriteRenderer>().flipX = isFacingRight;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("Collision Enter");
        if (collision.gameObject.tag == "Hint")
        {
            hintRange = true;
            EButton.SetActive(true);
        }
        if (collision.gameObject.tag == "Corpse")
        {
            corpseRange = true;
            EButton.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        print("Collision exit");
        if (collision.gameObject.tag == "Hint")
        {
            hintRange = false;
            EButton.SetActive(false);
        }
        if (collision.gameObject.tag == "Corpse")
        {
            corpseRange = false;
            EButton.SetActive(false);
        }
    }
}
