using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_TokenCollection : Scr_GenericCollection
{


    protected override void Awake()
    {
        base.Awake();
    }

    // Use this for initialization
    override protected void Start()
    {
        base.Start();

        setLayout(CollectionLayout.Grid);
        setSpacing(0.5f);
        setOffsetY(0.2f);
        setColumnCount(6);
        setRowCount(4);
    }

    // Update is called once per frame
    override protected void Update()
    {
        base.Update();
    }

    public List<Scr_Token> getTokens ()
    {
        List<Transform> children = getChildren();
        List<Scr_Token> tokens = new List<Scr_Token>();

        foreach (Transform obj in children)
        {
            Scr_Token token = obj.GetComponent<Scr_Token>();
            tokens.Add(token);
        }

        return tokens;
    }

    public int CountTokensOfSuite(Suite suite)
    {
        List<Scr_Token> tokens = getTokens();

        return getTokensOfSuite(tokens, suite).Count;
    }

    private List<Scr_Token> getTokensOfSuite (List<Scr_Token> tokens, Suite suite)
    {
        List<Scr_Token> tokensOfSuite = new List<Scr_Token>();

        foreach (Scr_Token token in tokens)
        {
            if (token.Suite == suite)
            {
                tokensOfSuite.Add(token);
            }
        }
        return tokensOfSuite;
    }

    public List<Scr_Token> hasTokensOfSuite (int amount, Suite suite)
    {
        List<Scr_Token> tokens = getTokensOfSuite(getTokens(), suite);

        int have = tokens.Count;
        int needed = amount - have;

        if (needed > 0)
        {
            List<Scr_Token> substitutes = TokenSubstituteThreeForOne(needed, suite, tokens);
            int substituteCount = substitutes.Count / 3;
            needed -= substituteCount;

            if (needed == 0)
            {
                tokens.AddRange(substitutes);
            }
        }
        
        if (needed == 0)
        {
            return tokens;
        }
        
        return null;
    }

    private List<Scr_Token> TokenSubstituteThreeForOne(int amount, Suite excludedSuite, List<Scr_Token> tokens)
    {
        HashSet<Suite> suites = new HashSet<Suite>();
        List<Scr_Token> tokenSet = new List<Scr_Token>();
        List<Scr_Token> tokenSubstitutes = new List<Scr_Token>();

        foreach (Scr_Token token in tokens)
        {
            if (token.Suite != excludedSuite)
                suites.Add(token.Suite);
        }

        foreach (Suite suite in suites)
        {
            foreach (Scr_Token token in tokens)
            {
                if (amount <= 0)
                    break;

                if (token.Suite == suite)
                {
                    if (tokenSet.Count < 3)
                        tokenSet.Add(token);

                    if (tokenSet.Count == 3)
                    {
                        amount--;
                        tokenSubstitutes.AddRange(tokenSet);
                        tokenSet.Clear();
                    }
                }
            }
        }

        return tokenSubstitutes;
    }
}
