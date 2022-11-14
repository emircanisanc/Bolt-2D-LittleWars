using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private float inputAxis;
    private Vector3 direction;

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
        if(inputAxis != 0)
        {
            rg2D.MovePosition(transform.position + direction * inputAxis * speed);
        }    
    }

    public void AddMovementInput(Vector3 direction, float inputAxis)
    {
        this.inputAxis = inputAxis;
        this.direction = direction;
    }

}
