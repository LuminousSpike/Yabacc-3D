using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DeckController : NetworkBehaviour
{

    public Collection CardCollection;
    public Transform CardPrefab;

    public const int CARDAMOUNT = 13;

    // Use this for initialization
    void Start()
    {
        if (isServer)
        {
            CardCollection = GetComponent<Collection>();
            CardCollection.layout = CollectionLayout.Stacked;
            CardCollection.collectionContainer.Spacing = 0.02f;
            CmdCreateCards();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    [Command]
    public void CmdCreateCards()
    {
        CreateCards(13, Suite.Red);
        CreateCards(11, Suite.Yellow);
        CreateCards(9, Suite.Green);
        CreateCards(7, Suite.Blue);
        CreateCards(5, Suite.Gray);
    }

    private void CreateCards(int amount, Suite suite)
    {
        for (int i = 1; i <= amount; i++)
        {
            Transform card = Instantiate(CardPrefab, this.transform.position, Quaternion.Euler(90, 0, 0), transform);
            CardController cardController = card.GetComponent<CardController>();

            cardController.number = ValueOfCard(i, amount);
            cardController.Suite = suite;
            CardCollection.Add(card);
            NetworkServer.Spawn(card.gameObject);
        }
    }

    private int StepValue(int value, int limit)
    {
        return value > limit ? 1 : 0;
    }

    private int ValueOfCard(int card, int cardsInSuite)
    {
        int value = card;
        switch (cardsInSuite)
        {
            case 11:
                value += StepValue(card, 3);
                value += StepValue(card, 8);
                break;
            case 9:
                value += StepValue(card, 2);
                value += StepValue(card, 3);
                value += StepValue(card, 6);
                value += StepValue(card, 7);
                break;
            case 7:
                return card > 1 ? 2 * card - 1 : value;
            case 5:
                return 3 * card - 2;
        }
        return value;
    }
}
