using System;
using UnityEngine;

public class HeroMoving : MonoBehaviour
{
    [SerializeField]
    private float speed = 1f;
    [SerializeField]
    private Rigidbody2D heroRigidbody;

	private Vector2 _movingDirection;
	public Vector2 MovingDirection => _movingDirection;

    private bool _isMoving = false;
    public bool IsMoving => _isMoving;

    HeroInputs heroInputs;

	public void Init(HeroInputs heroInputs)
    {
        this.heroInputs = heroInputs;

		heroInputs.OnStartMoving += StartMove;
		heroInputs.OnMoving += SetDirection;
		heroInputs.OnEndMoving += EndMove;
	}

    ~HeroMoving()
    {
		heroInputs.OnStartMoving -= StartMove;
		heroInputs.OnMoving -= SetDirection;
		heroInputs.OnEndMoving -= EndMove;
	}

    private void StartMove(Vector2 moveDirection)
    {
        _isMoving = true;

	}
	private void EndMove(Vector2 moveDirection)
	{
        _isMoving = false;
	}

    private void SetDirection(Vector2 newDirection)
    {
        _movingDirection = newDirection;
    }

	private void FixedUpdate()
	{
        if(_isMoving)
            Move(_movingDirection);
	}

	private void Move(Vector2 moveDirection)
    {
        //Debug.Log($"Debug Hero Move direction = {moveDirection}");
        transform.position += new Vector3(moveDirection.x, moveDirection.y, 0) * Time.deltaTime * speed;
    }
}
