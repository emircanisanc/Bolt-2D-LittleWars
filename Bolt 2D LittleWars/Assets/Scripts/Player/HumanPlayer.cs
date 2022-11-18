using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CMovement))]
[RequireComponent(typeof(InputManager))]
public class HumanPlayer : Player
{
    private CMovement Movement;
    private InputManager _InputManager;
    [SerializeField] private float maxXLocation;
    [SerializeField] private float minXLocation;

    void Start()
    {
        Movement = GetComponent<CMovement>();
        _InputManager = GetComponent<InputManager>();
        UIManager.Instance.CreateMarketUI(_Market, _GoldData);
        _GoldData._OnGoldChanged =UIManager.Instance.UpdateMarketUI;
    }

    void Update()
    {
        var input = _InputManager.GetRightAxis();
        if(input > 0 && transform.position.x >= maxXLocation)
        {
            Movement.AddMovementInput(0);
        }
        else if(input < 0 && transform.position.x <= minXLocation)
        {
            Movement.AddMovementInput(0);
        }
        else
        {
            Movement.AddMovementInput(input);
        }
        
    }
}
