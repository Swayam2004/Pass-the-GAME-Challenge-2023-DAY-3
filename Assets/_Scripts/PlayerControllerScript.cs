using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerControllerScript : MonoBehaviour
{
    [SerializeField]
    private float speed;

    //The sprite from the GFX child
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Tilemap _groundTilemap;
    [SerializeField] private Tilemap _colliderObjectTilemap;
    [SerializeField] private float _playerSpeed;

    private Rigidbody2D _rb;
    private Animator _animator;
    private Vector2 _moveDirection;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        //Input logic
        //if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        //{
        //    holdingUp = true;
        //}
        //if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        //{
        //    holdingDown = true;
        //}
        //if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        //{
        //    holdingLeft = true;
        //}
        //if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        //{
        //    holdingRight = true;
        //}
        //if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
        //{
        //    holdingUp = false;
        //}
        //if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow))
        //{
        //    holdingDown = false;
        //}
        //if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
        //{
        //    holdingLeft = false;
        //}
        //if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
        //{
        //    holdingRight = false;
        //}

        _moveDirection = GameInput.Instance.GetPlayerMovement() ;
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.PlayerCanMove && CanMove(_moveDirection))
        {
            HandleMovement(_moveDirection * _playerSpeed);
        }
        else
        {
            HandleMovement(Vector2.zero);
        }

        _animator.SetBool("isWalking", _rb.velocity != Vector2.zero);
    }

    //Turning inputs into movement
    private void HandleMovement(Vector2 moveDirection)
    {
        if (moveDirection.x < 0)
        {
            sprite.flipX = true;
        }
        else if (moveDirection.x > 0)
        {
            sprite.flipX = false;
        }

        _rb.velocity = moveDirection;
    }

    private bool CanMove(Vector2 direction)
    {
        Vector3Int gridPosition = _groundTilemap.WorldToCell(transform.position + (Vector3)direction);

        if (!_groundTilemap.HasTile(gridPosition) || _colliderObjectTilemap.HasTile(gridPosition))
        {
            return false;
        }
        return true;
    }
}
