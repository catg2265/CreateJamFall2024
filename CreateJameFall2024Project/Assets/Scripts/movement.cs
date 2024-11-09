using UnityEngine;
using UnityEngine.InputSystem;

public class movement : MonoBehaviour
{

    [SerializeField] private PlayerInput input;
    Vector2 movevector;
    public float moveSpeed = 5f;
    [SerializeField] private Rigidbody2D rb;
    public bool canMove = true;
    bool isFacingRight;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


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
        }
    }
}
