using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Tile : Scr_TokenCollection {

    public Transform prefab_TokenBag, prefab_system;
    public Transform prefab_LeftTileSide, prefab_RightTileSide;
    public int token_count;
    public bool flipped;

    List<Transform> _tokens;
    Scr_TokenBag _tokenBag;
    Scr_TileSide _leftTileSide, _rightTileSide;
    Scr_System _system;

    private Suite[] _tokenSuites;
    int _leftSideScore, _rightSideScore;

    protected override void Awake()
    {
        base.Awake();

        _tokenSuites = new Suite[token_count];
    }

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
        _leftTileSide = prefab_LeftTileSide.GetComponent<Scr_TileSide>();
        _rightTileSide = prefab_RightTileSide.GetComponent<Scr_TileSide>();
        _system = prefab_system.GetComponent<Scr_System>();

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

    public void setScore(int score, bool leftSide)
    {
        System.Console.WriteLine("Set score: " + score);
        if (leftSide)
            _leftSideScore = score;
        else
            _rightSideScore = score;

        if (_leftSideScore != 0 && _rightSideScore != 0)
        {
            CalculateWinner();
        }
    }

    private void CalculateWinner ()
    {
        List<Transform> children = getChildren();
        System.Console.WriteLine("Winner!");

        if (_leftSideScore == _rightSideScore)
        {
            // It's a draw.
            _system.ActivePlayer.TokenCollection.AddAll(children);
        }
        else if (flipped)
        {
            if (_leftSideScore > _rightSideScore)
            {
                // player 1 wins.
            _system.PrefabPlayer1.TokenCollection.AddAll(children);
            }
            else
            {
                // player 2 wins.
            _system.PrefabPlayer2.TokenCollection.AddAll(children);
            }
        }
        else
        {
            if (_leftSideScore < _rightSideScore)
            {

            _system.PrefabPlayer1.TokenCollection.AddAll(children);
            }
            else
            {
                // player 2 wins.
            _system.PrefabPlayer2.TokenCollection.AddAll(children);
            }
        }
        
        RemoveAll(children);
        _leftSideScore = 0;
        _rightSideScore = 0;

        getTokensFromBag();
        _leftTileSide.DiscardCards();
        _rightTileSide.DiscardCards();
    }
}
