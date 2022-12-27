using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IInteraction
{
    public string Name { get; }                 // �̸�
    public void OnInteract(Transform order);    // ����
}
public class InteractionUI : MonoBehaviour
{
    public static InteractionUI Instance { get; private set; }
    [SerializeField] GameObject panel;
    [SerializeField] Text hotKeyText;
    [SerializeField] Text interactionText;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        panel.SetActive(false);         // ó���� start���� ����� ���ش�.
    }

    public void Setup(IInteraction target)
    {
        hotKeyText.text = "F";
        interactionText.text = target.Name;
        panel.SetActive(true);
    }
    public void Close()                 // �г� ����.
    {
        panel.SetActive(false);
    }


}
