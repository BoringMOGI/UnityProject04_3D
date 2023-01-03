using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Movement3D))]
[RequireComponent(typeof(Inventory))]
public class Player : MonoBehaviour
{
    public static bool isLockControl;       // 유저의 입력 기능을 잠근다.

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
        // 유저의 입력이 막혀있지 않고 인벤토리도 열려 있지 않을 경우.
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
            target = null;         // 검색을 했을 때 주변에 NPC가 없으면 NPC를 Null로 대입한다.
                                    // 그렇지 않을 경우 멀리있는 NPC까지 대화가 가능.
        }
    }
    void Interaction()  // 상호작용
    {
        if (target == null)        // npc가 null이라면 함수 실행 X
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
        // 이동.
        movement.Movement(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));

        // 점프.
        if (Input.GetKeyDown(KeyCode.Space))
            movement.Jump();

        // 시점 회전.
        cameraLook.Rotate(new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")));

        // 확대 & 축소.
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
