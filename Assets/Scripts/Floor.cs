using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;
public class Floor : MonoBehaviour
{
    public List<Vector2> DoorPlaceholders; // if using placeholders
    public Single DoorsOffset; // if using offset
    public List<Door> Doors => this.GetComponentsInChildren<Door>().ToList();
}
