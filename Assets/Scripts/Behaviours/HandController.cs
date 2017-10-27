using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{

    private const int CARD_AMOUNT = 8;
    public Collection collection;

    // Use this for initialization
    void Awake()
    {
        collection = GetComponent<Collection>();
        collection.collectionContainer.Spacing = 2.25f;
        collection.collectionContainer.Centred = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Add (Transform child)
    {
        collection.Add(child);
    }
}
