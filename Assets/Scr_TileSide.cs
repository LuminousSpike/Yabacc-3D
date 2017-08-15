using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_TileSide : Scr_GenericCollection
{
    public Transform _parent;
    public float offset;
    public bool flipped;

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        GameObject parent = _parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        setOffsetX(offset);
        setFlipped(flipped);
        base.Update();
    }
}
