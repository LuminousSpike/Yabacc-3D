using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public enum CollectionLayout
{
    HorizontalOnly,
    VerticalOnly,
    Stacked,
    Grid
}

public class Collection : NetworkBehaviour
{

    public List<Transform> _children;
    public CollectionLayout _layout;

    public float _spacing = 2.5f, _offsetX, _offsetY, _offsetZ;
    public Transform _transform;
    public int _size, _columnCount, _rowCount;
    public bool _centered;
    public bool _flipped;
    public bool _reposition;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }


    public int Remove(Transform child)
    {
        int index = _children.IndexOf(child);
        if (_children.Remove(child))
        {
            _size--;
        }
        return index;
    }

    public void RemoveAll(List<Transform> children)
    {
        Transform[] remove = children.ToArray();
        int count = children.Count;

        for (int i = 0; i < count; i++)
        {
            Remove(remove[i]);
        }
    }

    public Transform Pop()
    {
        if (_children.Count == 0)
        {
            return null;
        }

        Transform child = _children[_children.Count - 1];
        Remove(child);
        return child;
    }

    public Transform getRandom()
    {
        int count = _children.Count;

        if (count == 0)
        {
            return null;
        }

        Transform child = _children[Random.Range(0, count)];
        Remove(child);
        return child;
    }

    public virtual bool Add(Transform child)
    {
        if (child == null)
        {
            return false;
        }

        //_children.Add(child);
        //child.parent = transform;
        _size++;

        return true;
    }

    public virtual bool AddAll(List<Transform> children)
    {
        foreach (Transform child in children)
        {
            if (Add(child) == false)
            {
                return false;
            }
        }

        return true;
    }

    public void Insert(int index, Transform child)
    {
        _children.Insert(index, child);
        _size++;
    }
}