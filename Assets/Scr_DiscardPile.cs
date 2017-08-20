using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_DiscardPile : Scr_GenericCollection {

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
}
