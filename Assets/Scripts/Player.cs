using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float interactionRadius;

    IInteraction target;

    void Update()
    {
        SearchInteraction();
        Interaction();
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, interactionRadius);
    }
}
