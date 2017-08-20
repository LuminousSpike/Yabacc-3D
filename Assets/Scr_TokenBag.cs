using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_TokenBag : Scr_TokenCollection {

    public Transform token_prefab;
    public Material mat_gray, mat_blue, mat_green, mat_yellow, mat_red;


    private void Awake()
    {
        base.Awake();

        CreateTokens(13, Suite.Red, mat_red);
        CreateTokens(11, Suite.Yellow, mat_yellow);
        CreateTokens(9, Suite.Green, mat_green);
        CreateTokens(7, Suite.Blue, mat_blue);
        CreateTokens(5, Suite.Gray, mat_gray);
    }

    // Use this for initialization
    override protected void Start () {
        base.Start();

        setLayout(CollectionLayout.Grid);
        setSpacing(0.5f);
        setOffsetY(0.3f);
        setColumnCount(8);
        setRowCount(8);
        setCentered(true);
	}
	
	// Update is called once per frame
	override protected void Update () {
        base.Update();
	}

    private void CreateTokens (int amount, Suite suite, Material mat_suite)
    {
        for (int i = 0; i < amount; i++)
        {
            Transform token = Instantiate(token_prefab, this.transform.position, Quaternion.Euler(90, 0, 0), transform);
            token.GetComponent<Scr_Token>().Suite = suite;
            token.GetComponent<Renderer>().material = mat_suite;
            Add(token);
        }
    }
}
