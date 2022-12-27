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

    Vector3 input;          // ������ �Է� ��.
    Vector3 velocity;       // �߷� �ӵ�.
    bool isGrounded;        // ���� �� �ִ°�?
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
        // �������� �����ϴ� �˰���.
        animator.SetFloat("x", input.x);
        animator.SetFloat("y", input.y);
        animator.SetBool("isRun", input != Vector3.zero);
    }


    private void Movement()
    {
        // �� ���� ����� ���� ��ǥ�� ���� ������ ���ϰ� �ӵ���ŭ �̵��Ѵ�.

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
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * GRAVITY);        // ������ ���������� ���� �ʾƵ� ���߷��� �̿��Ͽ� ����
            //rigid.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
        }
    }
    private void Gravity()
    {
        if (velocity.y < 0 && isGrounded)
            velocity.y = -2f;     // ���߿� �������� ƨ�� �ö��� �� �ٽ� �������� ������ ���� �ణ�� �߷� ���� �����Ѵ�.

        velocity.y += GRAVITY * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, checkGroundRadius);
    }
}
