using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_GenericCollection : MonoBehaviour
{
    private float _spacing = 2.5f, _offsetX;
    private Transform _transform;
    private int _size;
    private bool _centered;
    private bool _flipped;
    private bool _reposition;

    private List<Transform> _children;

    protected void setReposition (bool reposition)
    {
        _reposition = reposition;
    }

    protected void setFlipped (bool flipped)
    {
        _flipped = flipped;
    }

    protected void setCentered (bool centered)
    {
        _centered = centered;
    }

    protected void setOffsetX (float offset)
    {
        _offsetX = offset;
    }

    protected void setTransform (Transform transform)
    {
        _transform = transform;
    }

    // Use this for initialization
    protected virtual void Start()
    {
        _children = new List<Transform>();
        _transform = transform;
        _reposition = true;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (_reposition)
        {
            PositionChildren();
        }
    }

    void PositionChildren()
    {
        Vector3 currentPos, newPos = _transform.position;

        for (int i = 0; i < _children.Count; i++)
        {
            currentPos = _children[i].position;
            newPos.x = _offsetX + (_spacing * i);
            if (_flipped)
            {
                newPos.x *= -1;
            }
            if (_centered)
            {
                //newPos.x -= (_size - 1);
                newPos.x = _offsetX + (_spacing * (i - ((_size - 1) / 2f)));
            }

            newPos.z = this.transform.position.z;
            _children[i].position = Vector3.Lerp(currentPos, newPos, (Time.deltaTime * 3f));
        }
    }

    public void Remove(Transform child)
    {
        if (_children.Remove(child))
        {
            _size--;
        }
    }

    public void Add(Transform child)
    {
        _children.Add(child);
        _size++;
    }
}
