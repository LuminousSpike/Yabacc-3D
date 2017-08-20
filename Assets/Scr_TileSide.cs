using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_TileSide : Scr_GenericCollection
{
    public Transform _parent;
    public float offset, offsetY;
    public bool flipped;

    private Scr_Tile _tileParent;
    public List<Suite> _wantedSuites;

    override protected void Awake()
    {
        base.Awake();

        _wantedSuites = new List<Suite>();
    }

    // Use this for initialization
    override protected void Start()
    {
        base.Start();

        _tileParent = _parent.GetComponent<Scr_Tile>();
        _wantedSuites.AddRange(_tileParent.getTokenSuites());

        setOffsetX(offset);
        setOffsetY(offsetY);
        setFlipped(flipped);
        setLayout(CollectionLayout.HorizontalOnly);
    }

    // Update is called once per frame
    override protected void Update()
    {
        base.Update();
    }

    public override bool Add (Transform child)
    {
        Scr_Card card = child.GetComponent<Scr_Card>();
        if (wantedCard(card))
        {
            return base.Add(child);
        }

        return false;
    }

    private bool wantedCard (Scr_Card card)
    {
        if (_wantedSuites.Contains(card.Suite))
        {
            _wantedSuites.Remove(card.Suite);
            return true;
        }

        return false;
    }
}
