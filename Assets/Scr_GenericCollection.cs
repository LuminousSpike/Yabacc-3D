using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_GenericCollection : MonoBehaviour
{
    private float _spacing = 2.5f, _offsetX, _offsetY, _offsetZ;
    private Transform _transform;
    private int _size, _columnCount, _rowCount;
    private bool _centered;
    private bool _flipped;
    private bool _reposition;
    private CollectionLayout _layout;

    private List<Transform> _children;

    public enum CollectionLayout
    {
        HorizontalOnly,
        VerticalOnly,
        Stacked,
        Grid
    }

    public List<Transform> getChildren ()
    {
        return _children;
    }

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

    protected void setOffsetY (float offset)
    {
        _offsetY = offset;
    }

    protected void setOffsetZ (float offset)
    {
        _offsetZ = offset;
    }

    protected void setTransform (Transform transform)
    {
        _transform = transform;
    }

    protected void setLayout (CollectionLayout layout)
    {
        _layout = layout;
    }

    protected void setSpacing (float spacing)
    {
        _spacing = spacing;
    }

    protected void setColumnCount (int count)
    {
        _columnCount = count;
    }

    protected void setRowCount(int count)
    {
        _rowCount = count;
    }

    protected virtual void Awake()
    {
        _children = new List<Transform>();
        _transform = transform;
        _reposition = true;
    }

    // Use this for initialization
    protected virtual void Start()
    {

    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (_reposition)
        {
            if (_layout == CollectionLayout.HorizontalOnly)
            {
                PositionChildrenHorizontally();
            }
            else if (_layout == CollectionLayout.Stacked)
            {
                PositionChildrenStacked();
            }
            else if (_layout == CollectionLayout.Grid)
            {
                PositionChildrenGrid();
            }
        }
    }

    void PositionChildrenHorizontally()
    {
        Vector3 currentPos, newPos = _transform.position;

        for (int i = 0; i < _children.Count; i++)
        {
            currentPos = _children[i].position;

            newPos.z = transform.position.z + _offsetZ;
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

            newPos.y = _offsetY + this.transform.position.y;
            _children[i].position = Vector3.Lerp(currentPos, newPos, (Time.deltaTime * 8f));
        }
    }

    void PositionChildrenStacked()
    {
        Vector3 currentPos, newPos = _transform.position;

        for (int i = 0; i < _children.Count; i++)
        {
            currentPos = _children[i].position;

            if (_centered)
            {
                newPos.y = transform.position.y + _offsetY + (_spacing * (i - ((_size - 1) / 2f)));
            }
            else
            {
                newPos.y = transform.position.y + _offsetY + (_spacing * i);
            }

            if (_flipped)
            {
                newPos.y *= -1;
            }

            _children[i].position = Vector3.Lerp(currentPos, newPos, (Time.deltaTime * 8f));
        }
    }

    void PositionChildrenGrid()
    {
        Vector3 currentPos, newPos = _transform.position;
        int currentRow, currentTier;

        float maxColumnCount = System.Math.Min(_children.Count, _columnCount);
        float maxRowCount = System.Math.Min(_children.Count / _columnCount, _rowCount) + 1;
        float posX, posY, posZ;

        for (int i = 0; i < _children.Count; i++)
        {
            currentPos = _children[i].position;

            currentRow = i / _columnCount;
            currentTier = currentRow / _rowCount;

            posX = _offsetX + (_spacing * (i % _columnCount));
            posY = _offsetY + (_spacing * currentTier);
            posZ = _offsetZ + (_spacing * (currentRow % _rowCount));

            // TODO: Fix this as it doesn't work correctly.
            if (_flipped)
            {
                posX *= -1;
                posY *= -1;
                posZ *= -1;
            }

            if (_centered)
            {
                newPos.x = transform.position.x - ((_columnCount / maxColumnCount) * 1.75f) + posX;
                newPos.y = transform.position.y + posY;
                newPos.z = transform.position.z - (_rowCount / maxRowCount) + posZ;
            }
            else
            {
                newPos.x = transform.position.x + _offsetX + posX;
                newPos.y = transform.position.y + _offsetY + posY;
                newPos.z = transform.position.z + _offsetZ + posZ;
            }

            _children[i].position = Vector3.Lerp(currentPos, newPos, (Time.deltaTime * 8f));
        }
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

    public Transform Pop ()
    {
        if (_children.Count == 0)
        {
            return null;
        }

        Transform child = _children[_children.Count - 1];
        Remove(child);
        return child;
    }

    public Transform getRandom ()
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
        
        _children.Add(child);
        child.parent = transform;
        _size++;

        return true;
    }

	public void Insert(int index, Transform child) {
		_children.Insert (index, child);
		_size++;
	}
}
