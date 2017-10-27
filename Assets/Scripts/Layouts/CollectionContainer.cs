using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionContainer {

    public float Spacing { get; set; }
    public float OffsetX { get; set; }
    public float OffsetY { get; set; }
    public float OffsetZ { get; set; }
    public int Size { get; set; }
    public int ColumnCount { get; set; }
    public int RowCount { get; set; }
    public bool Centred { get; set; }
    public bool Flipped { get; set; }
    public bool Reposition { get; set; }
    public Transform Transform { get; set; }
    public List<Transform> Children { get; set; }
}
