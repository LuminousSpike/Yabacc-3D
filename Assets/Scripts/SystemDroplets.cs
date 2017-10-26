using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SystemDroplets : NetworkBehaviour
{

    public DeckController deckController;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void GetDeckController ()
    {
        deckController = GetComponentInChildren<DeckController>();
    }
}
