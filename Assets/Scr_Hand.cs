using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Hand : Scr_GenericCollection
{
    private const int CARD_AMOUNT = 8;
    private Scr_Deck _deck;

    public Transform card_prefab, deck_prefab;

    // Use this for initialization
    protected override void Start ()
    {
        base.Start();
        _deck = deck_prefab.GetComponent<Scr_Deck>();
        for (int i = 0; i < CARD_AMOUNT; i++)
        {
            PickupCard();
        }
        setCentered(true);
        setLayout(CollectionLayout.HorizontalOnly);
    }

    public void PickupCard ()
    {
            Transform card = _deck.Pop();
            Add(card);
    }
}
