using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Tile : Scr_TokenCollection {

    public Transform prefab_TokenBag;
    public int token_count;

    List<Transform> _tokens;
    Scr_TokenBag _tokenBag;

	// Use this for initialization
	override protected void Start () {
        base.Start();

        setLayout(CollectionLayout.HorizontalOnly);
        setSpacing(0.5f);
        setOffsetZ(-1f);
        setOffsetY(0.2f);
        setCentered(true);
        _tokens = getChildren();
        _tokenBag = prefab_TokenBag.GetComponent<Scr_TokenBag>();
        getTokensFromBag();
	}
	
	// Update is called once per frame
	override protected void Update () {
        base.Update();
	}

    private void getTokensFromBag ()
    {
        while (_tokens.Count < token_count)
        {
            Add(_tokenBag.getRandom());
        }
    }
}
