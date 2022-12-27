using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IInteraction
{
    public string Name { get; }                 // 이름
    public void OnInteract(Transform order);    // 동작
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
        panel.SetActive(false);         // 처음에 start에서 페널을 꺼준다.
    }

    public void Setup(IInteraction target)
    {
        hotKeyText.text = "F";
        interactionText.text = target.Name;
        panel.SetActive(true);
    }
    public void Close()                 // 패널 끄기.
    {
        panel.SetActive(false);
    }


}
