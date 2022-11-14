using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private float inputAxis;

    private Rigidbody2D rg2D;

    void Awake()
    {
        rg2D = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if(inputAxis != 0)
        {
            rg2D.MovePosition(transform.position + inputAxis * speed * transform.right * Time.fixedDeltaTime);
        }   
        
    }

    public void AddMovementInput(float inputAxis)
    {
        this.inputAxis = inputAxis;

    }
    public bool IsMoving()
    {
        return rg2D.velocity.x != 0;
    }

}
