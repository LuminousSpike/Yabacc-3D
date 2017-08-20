using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_TileSide : Scr_GenericCollection
{
    public Transform _parent;
    public float offset, offsetY;
    public bool flipped;

    private Scr_Tile _tileParent;
    private Scr_GenericCollection _discardPile;
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
        _discardPile = _tileParent.prefab_system.GetComponent<Scr_System>().DiscardPile;
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
        bool result = false;
        Scr_Card card = child.GetComponent<Scr_Card>();

        if (wantedCard(card))
        {
            result = base.Add(child);

            if (_wantedSuites.Count == 0)
            {
                _tileParent.setScore(getCardValues(), flipped);
            }
        }

        return result;
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

    private int getCardValues ()
    {
        List<Transform> children = getChildren();
        int result = 0;

        foreach (Transform obj in children)
        {
            result += obj.GetComponent<Scr_Card>().number;
        }

        return result;
    }

    public void DiscardCards ()
    {
        List<Transform> children = getChildren();
        _discardPile.AddAll(children);
        RemoveAll(children);
        _wantedSuites.AddRange(_tileParent.getTokenSuites());
    }
}
