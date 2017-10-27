using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{

    public HandController handController;

    // Use this for initialization
    void Awake()
    {
        handController = GetComponentInChildren<HandController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddCard(Transform card)
    {
        if (hasAuthority)
            handController.Add(card);
    }
}
