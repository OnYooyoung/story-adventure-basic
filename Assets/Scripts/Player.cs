using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
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
        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            moveDirection.Set(move.x, move.y); // 현재 방향을 기억
            moveDirection.Normalize(); // 길이 1로 정규화
        }

        animator.SetFloat("Look X", moveDirection.x);
        animator.SetFloat("Look Y", moveDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        // Scene 뷰에서만 빨간 선으로 레이저 사거리가 보입니다.
        Debug.DrawRay(rigidbody2d.position + Vector2.up * 0.2f, moveDirection * 1.5f, Color.red);

        if (InteractAction.WasPressedThisFrame())
        {
            // 1. 레이저를 쏠 지점(캐릭터 위치)과 방향(바라보는 방향), 거리(1.5f) 설정
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, moveDirection, 1.5f, LayerMask.GetMask("Interactable"));

            // 2. 무언가 맞았다면?
            if (hit.collider != null)
            {
                Debug.Log(hit.collider.gameObject.name + "을(를) 조사했습니다!");

                // 여기에 상자 열기나 대화창 띄우기 코드를 넣으면 됩니다.
                // 예: hit.collider.GetComponent<Chest>()?.Open();
            }
        }
    }

    // FixedUpdate has the same call rate as the physics system
    void FixedUpdate()
    {
        Vector2 position = (Vector2)rigidbody2d.position + move * speed * Time.fixedDeltaTime;
        rigidbody2d.MovePosition(position);
    }
}
