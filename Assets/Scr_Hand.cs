using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Hand : Scr_GenericCollection
{
    private const int CARD_AMOUNT = 8;

    public Transform card_prefab;

    // Use this for initialization
    protected override void Start ()
    {
        base.Start();
        for (int i = 0; i < CARD_AMOUNT; i++)
        {
            Add(Instantiate(card_prefab, this.transform.position, Quaternion.Euler(90, 0, 0), transform));
        }
        setCentered(true);
    }
}
