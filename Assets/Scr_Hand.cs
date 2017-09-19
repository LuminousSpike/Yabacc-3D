using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Hand : Scr_GenericCollection
{
    private const int CARD_AMOUNT = 8;
    private Scr_Deck _deck;

    public Transform card_prefab, deck_prefab;
    public bool Active;
    public event Action OnTurnEnd;

    public void setActive(bool active)
    {
        Active = active;
        List<Transform> children = getChildren();

        foreach (Transform t in children)
        {
            Scr_Card card = t.GetComponent<Scr_Card>();
            card.setActive(active);
        }
    }


    private void OnPlayed (Scr_Card card)
    {
        card.OnPlayed -= OnPlayed;

        setActive(false);

        if (OnTurnEnd != null)
        {
            OnTurnEnd();
        }
    }

    protected override void Awake()
    {
        base.Awake();
    }

    // Use this for initialization
    protected override void Start ()
    {
        base.Start();
        _deck = deck_prefab.GetComponent<Scr_Deck>();
        for (int i = 0; i < CARD_AMOUNT; i++)
        {
            PickupCard();
        }
        setLayout(CollectionLayout.HorizontalOnly);
        setOffsetY(0.01f);
        setCentered(true);
    }

    public void PickupCard ()
    {
        Transform card = _deck.getRandom();
        card.GetComponent<Scr_Card>().OnPlayed += OnPlayed;
        card.GetComponent<Scr_Card> ().setActive (Active);
        Add(card);
    }
}
