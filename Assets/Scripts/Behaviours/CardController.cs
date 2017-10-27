using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public enum Suite
{
    Gray,
    Blue,
    Green,
    Yellow,
    Red
}

public class CardController : NetworkBehaviour 
{

    [SyncVar]
    public int number;
    [SyncVar]
    public Suite Suite;

    public Material MaterialGray, MaterialBlue, MaterialGreen, MaterialYellow, MaterialRed;

    //public event Action<Scr_Card> OnPlayed;

    bool _played;
    Transform _other;
    int _lastIndex;
    TextMesh[] _numberText;
    MouseDraggable _draggable;
    bool _active;

    // Use this for initialization
    void Start()
    {
        _numberText = GetComponentsInChildren<TextMesh>();
        SetNumberMeshes();
        GetComponent<Renderer>().material = GetMaterial();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private Material GetMaterial()
    {
        switch(Suite)
        {
            case Suite.Blue:
                return MaterialBlue;
            case Suite.Green:
                return MaterialGreen;
            case Suite.Yellow:
                return MaterialYellow;
            case Suite.Red:
                return MaterialRed;
            default:
                return MaterialGray;
        }
    }

    private void SetNumberMeshes()
    {
        foreach (TextMesh textMesh in _numberText)
        {
            textMesh.text = number.ToString();
        }
    }
}
