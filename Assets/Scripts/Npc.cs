using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour, IInteraction
{
    [SerializeField] string npcName;
    [SerializeField] string context;

    public string Name => npcName;

    public void OnInteract(Transform order)
    {
        Debug.Log(context);
    }
}