using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Tile : Scr_TokenCollection {

    public Transform prefab_TokenBag;
    public int token_count;

    List<Transform> _tokens;
    Scr_TokenBag _tokenBag;

    private Suite[] _tokenSuites;

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
        _tokenSuites = new Suite[token_count];

        getTokensFromBag();
	}
	
	// Update is called once per frame
	override protected void Update () {
        base.Update();
	}

    private void getTokensFromBag ()
    {
        for (int i = 0; i < token_count; i++)
        {
            Transform token = _tokenBag.getRandom();
            _tokenSuites[i] = token.GetComponent<Scr_Token>().Suite;
            Add(token);
        }
    }

    public Suite[] getTokenSuites ()
    {
        return _tokenSuites;
    }
}
