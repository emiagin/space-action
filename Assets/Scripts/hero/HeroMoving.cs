using System;
using UnityEngine;

public class HeroMoving : MonoBehaviour
{
    [SerializeField]
    private float speed = 1f;
    [SerializeField]
    private Rigidbody2D heroRigidbody;
    [SerializeField]
    private HeroBehaviour heroBehaviour;

    private Vector2 moveDirection;

    private bool _isMoving = false;
    public bool IsMoving
    {
        get => _isMoving;
        private set
        {
            if (value == _isMoving)
                return;
            _isMoving = value;
            if (!_isMoving)
                OnHeroStopMoving?.Invoke(false);
            else
                OnHeroStartMoving?.Invoke(true);
        }
    }

    public Action<Vector2> OnHeroMoving;
    public Action<bool> OnHeroStopMoving;
    public Action<bool> OnHeroStartMoving;

    private void Update()
    {
        ProcessInputs();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void ProcessInputs()
    {
        var moveVector = GetAxesForPlayerMove();
        moveDirection = moveVector.normalized;
    }

    private void Move()
    {
        //Debug.Log($"Debug Hero Move direction = {moveDirection}");
        heroRigidbody.velocity = new Vector2(moveDirection.x * speed, moveDirection.y * speed);
        if (moveDirection != Vector2.zero)
        {
            IsMoving = true;
            OnHeroMoving?.Invoke(moveDirection);
        }
        else
            IsMoving = false;

    }

    private Vector2 GetAxesForPlayerMove()
    {
        bool isKeysRight = Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.UpArrow)
     || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow);

        if (!isKeysRight)
            return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        return Vector2.zero;
    }
}
