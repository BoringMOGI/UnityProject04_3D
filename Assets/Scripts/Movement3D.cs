using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement3D : MonoBehaviour
{
    

    [SerializeField] CharacterController controller;
    [SerializeField] float walkSpeed;
    [SerializeField] float runSpeed;
    [SerializeField] float jumpHeight;    
    [Range(0.1f, 4.0f)]
    [SerializeField] float gravityScale;
    [SerializeField] float checkGroundRadius;
    [SerializeField] LayerMask groundMask;

    public Animator animator;

    float GRAVITY => -9.81f * gravityScale;
    float horizontal;

    Vector3 input;          // 유저의 입력 값.
    Vector3 velocity;       // 중력 속도.
    bool isGrounded;        // 땅에 서 있는가?
    public static bool isLockControl;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        input = Vector3.zero;

        isGrounded = Physics.CheckSphere(transform.position, checkGroundRadius, groundMask);

        if (!isLockControl)
        {
            Movement();
            Jump();
        }

        Gravity();
    }

    private void LateUpdate()
    {
        // 이전값을 저장하는 알고리즘.
        animator.SetFloat("x", input.x);
        animator.SetFloat("y", input.y);
        animator.SetBool("isRun", input != Vector3.zero);
    }


    private void Movement()
    {
        // 내 기준 정면과 우측 좌표를 더해 방향을 구하고 속도만큼 이동한다.

        bool isShift = Input.GetKey(KeyCode.LeftShift);
        float x = Input.GetAxis("Horizontal") * (isShift ? 1.0f : 0.5f);
        float y = Input.GetAxis("Vertical") * (isShift ? 1.0f : 0.5f);
        input = new Vector2(x, y);

        Vector3 dir = (transform.right * x) + (transform.forward * y);
        dir.Normalize();
               
        controller.Move(dir * (isShift ? runSpeed : walkSpeed) * Time.deltaTime);

        
            
        
    }   
    private void Jump()
    {
        if(isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * GRAVITY);        // 점프를 에드포스를 하지 않아도 역중력을 이용하여 점프
            //rigid.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
        }
    }
    private void Gravity()
    {
        if (velocity.y < 0 && isGrounded)
            velocity.y = -2f;     // 도중에 공중으로 튕겨 올랐을 때 다시 지면으로 내리기 위해 약간의 중력 값을 유지한다.

        velocity.y += GRAVITY * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, checkGroundRadius);
    }
}
