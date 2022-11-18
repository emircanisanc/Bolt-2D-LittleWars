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

    void Update()
    {
        Move();
    }

    private void Move()
    {
        rg2D.velocity = new Vector2(inputAxis * speed, rg2D.velocity.y);    
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
