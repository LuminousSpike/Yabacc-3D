using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SystemDroplets : NetworkBehaviour
{

    public DeckController deckController;
    public PlayerController player1, player2;
    bool startup = true;

    void Awake()
    {
        GetDeckController();
    }

    // Use this for initialization
    private void Start()
    {
    }
    void GiveTheCards()
    {
        for (int i = 0; i < 8; i++)
        {
            if (isServer)
                CmdAddCard();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (player1 != null && player2 != null)
        {
            if (startup)
            {
                GiveTheCards();
                startup = false;
            }
        }
        else
        {
            PlayerController[] temp = FindObjectsOfType<PlayerController>();
            if (temp.Length == 2)
            {
                player1 = temp[0];
                player2 = temp[1];
            }
        }
    }

    private void GetDeckController()
    {
        deckController = GetComponentInChildren<DeckController>();
    }

    [Command]
    void CmdAddCard()
    {
        Transform card = deckController.GetRandom();
        Transform card2 = deckController.GetRandom();
        player1.AddCard(card);
        player2.AddCard(card2);
        card.GetComponent<NetworkIdentity>().AssignClientAuthority(player1.connectionToClient);
        card2.GetComponent<NetworkIdentity>().AssignClientAuthority(player2.connectionToClient);
    }
}
