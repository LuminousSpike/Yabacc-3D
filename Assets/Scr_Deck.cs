﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Deck : Scr_GenericCollection {

    private const int CARD_AMOUNT = 13;

    public Transform card_prefab;
    public Material mat_gray, mat_blue, mat_green, mat_yellow, mat_red;

    protected override void Awake()
    {
        base.Awake();

        CreateCards(13, Suite.Red, mat_red);
        CreateCards(11, Suite.Yellow, mat_yellow);
        CreateCards(9, Suite.Green, mat_green);
        CreateCards(7, Suite.Blue, mat_blue);
        CreateCards(5, Suite.Gray, mat_gray);
    }

    // Use this for initialization
    override protected void Start () {
        base.Start();

        setLayout(CollectionLayout.Stacked);
        setSpacing(0.02f);
	}
	
	// Update is called once per frame
	override protected void Update () {
        base.Update();
	}

    private void CreateCards (int amount, Suite suite, Material mat_suite)
    {
        for (int i = 1; i <= amount; i++)
        {
            Transform card = Instantiate(card_prefab, this.transform.position, Quaternion.Euler(90, 0, 0), transform);
            Scr_Card scr_Card = card.GetComponent<Scr_Card>();

            scr_Card.number = ValueOfCard(i, amount);
            scr_Card.Suite = suite;
            card.GetComponent<Renderer>().material = mat_suite;
            Add(card);
        }
    }

    private int StepValue (int value, int limit)
    {
        return value > limit ? 1 : 0;
    }

    private int ValueOfCard (int card, int cardsInSuite)
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
