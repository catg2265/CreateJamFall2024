using UnityEngine;
using UnityEngine.InputSystem;

public class movement : MonoBehaviour
{

    [SerializeField] private PlayerInput input;
    public GameObject EButton;
    public GameObject DialogueBox;

    Vector2 movevector;
    public float moveSpeed = 5f;
    [SerializeField] private Rigidbody2D rb;
    public bool canMove = true;
    bool isFacingRight;

    bool hintRange = false;
    bool corpseRange = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EButton.SetActive(false);
        DialogueBox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (input.actions["Interact"].WasPressedThisFrame())
        {
            if (hintRange && !corpseRange && !DialogueBox.activeSelf)
            {
                //Activate hint
                print("hint");
            }
            else if (!hintRange && corpseRange && !DialogueBox.activeSelf)
            {
                //Activate specific corpse data
            }
                

        }

        Flip();
    }
    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(movevector.x * moveSpeed , movevector.y*moveSpeed);
    }
    void OnMove(InputValue input)
    {
        if (canMove) 
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
