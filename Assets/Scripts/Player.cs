using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public GameManager gameManager;

    // Variables related to player character movement
    public InputAction MoveAction;
    Rigidbody2D rigidbody2d;
    Vector2 move;
    public float speed = 3.0f;

    // Variables related to animation
    Animator animator;
    Vector2 moveDirection = new Vector2(1, 0); // (X, Y)

    // search action
    public InputAction InteractAction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MoveAction.Enable();
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        InteractAction.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        move = MoveAction.ReadValue<Vector2>();

        // 플레이어가 움직이고 있다면 (0이 아니라면), 부동소수점문제 해결 위해 approximately 사용
        if (!gameManager.isAction && (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f)))
        {
            moveDirection.Set(move.x, move.y); // 현재 방향을 기억
            moveDirection.Normalize(); // 길이 1로 정규화
        }

        if (!gameManager.isAction)
        {
            animator.SetFloat("Look X", moveDirection.x);
            animator.SetFloat("Look Y", moveDirection.y);
            animator.SetFloat("Speed", move.magnitude);
        }

        Debug.DrawRay(rigidbody2d.position + Vector2.up * 0.2f, moveDirection * 1.5f, Color.green);

        if (InteractAction.WasPressedThisFrame())
        {
            // 1. 레이저를 쏠 지점(캐릭터 위치)과 방향(바라보는 방향), 거리(1.5f) 설정
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, moveDirection, 1.5f, LayerMask.GetMask("Interactable"));

            // 2. 무언가 맞았다면?
            if (hit.collider != null)
            {
                gameManager.Action(hit.collider.GameObject());
            }
        }
    }

    // FixedUpdate has the same call rate as the physics system
    void FixedUpdate()
    {
        if (!gameManager.isAction)
        {
            Vector2 position = (Vector2)rigidbody2d.position + move * speed * Time.fixedDeltaTime;
            rigidbody2d.MovePosition(position);
        }
    }
}
