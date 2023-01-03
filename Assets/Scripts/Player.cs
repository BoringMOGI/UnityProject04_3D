using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Movement3D))]
[RequireComponent(typeof(Inventory))]
public class Player : MonoBehaviour
{
    public static bool isLockControl;       // ������ �Է� ����� ��ٴ�.

    [SerializeField] float interactionRadius;
    [SerializeField] CameraLook cameraLook;

    Movement3D movement;
    Inventory inventory;

    IInteraction target;

    private void Start()
    {
        movement = GetComponent<Movement3D>();
        inventory = GetComponent<Inventory>();
    }

    void Update()
    {
        // ������ �Է��� �������� �ʰ� �κ��丮�� ���� ���� ���� ���.
        if (!isLockControl && !InventoryUI.Instance.isOpenInven)
        {
            SearchInteraction();
            Interaction();
            InputMovement();
        }

        InputMenu();
    }  

    void SearchInteraction()
    {
        Collider[] targets = Physics.OverlapSphere(transform.position, interactionRadius);
        if (targets.Length > 0)
        {
            var find = from target in targets
                       orderby Vector3.Distance(transform.position, target.transform.position)
                       where target.GetComponent<IInteraction>() != null
                       select target.GetComponent<IInteraction>();

            if (find.Count() > 0)            
                target = find.First();            
            else
                target = null;           
        }
        else
        {
            target = null;         // �˻��� ���� �� �ֺ��� NPC�� ������ NPC�� Null�� �����Ѵ�.
                                    // �׷��� ���� ��� �ָ��ִ� NPC���� ��ȭ�� ����.
        }
    }
    void Interaction()  // ��ȣ�ۿ�
    {
        if (target == null)        // npc�� null�̶�� �Լ� ���� X
        {
            interactionUI.Instance.Close();
            return;
        }

        interactionUI.Instance.Setup(target);
        if (Input.GetKeyDown(KeyCode.F))
            target.OnInteract(transform);
    }
    void InputMovement()
    {
        // �̵�.
        movement.Movement(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));

        // ����.
        if (Input.GetKeyDown(KeyCode.Space))
            movement.Jump();

        // ���� ȸ��.
        cameraLook.Rotate(new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")));

        // Ȯ�� & ���.
        float wheel = Input.GetAxis("Mouse ScrollWheel");
        if (wheel != 0)
            cameraLook.Zoom(wheel < 0);
    }
    void InputMenu()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            InventoryUI.Instance.Switch(inventory.itemList);
        }
    }
    public void OnAddItem(Item item)
    {
        inventory.AddItem(item);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, interactionRadius);
    }
}
