using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Inventory inventory;
    [SerializeField] float interactionRadius;

    IInteraction target;

    private void Awake()
    {
        inventory.Initialize();
    }

    void Update()
    {
        SearchInteraction();
        Interaction();
        UserInput();
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
            InteractionUI.Instance.Close();
            return;
        }

        InteractionUI.Instance.Setup(target);
        if (Input.GetKeyDown(KeyCode.F))
            target.OnInteract(transform);
    }
    void UserInput()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            InventoryUI.Instance.Switch();
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
