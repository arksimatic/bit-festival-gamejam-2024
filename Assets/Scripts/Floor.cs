using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;
public class Floor : MonoBehaviour
{
    public List<Vector2> DoorPlaceholders;
    public Int32 DoorsToGenerate;
    public List<Door> Doors = new();
    public void Awake()
    {
        Doors = this.GetComponentsInChildren<Door>().ToList();
    }
    public void Start()
    {
        GenerateDoors(4);
        Doors = this.GetComponentsInChildren<Door>().ToList();
    }
    public void GenerateDoors(Int32 doorAmount)
    {
        for (int i = 0; i < doorAmount; i++)
            GenerateDoorAtEmptyPlaceholder();
    }
    public void GenerateDoorAtEmptyPlaceholder()
    {
        Doors = this.GetComponentsInChildren<Door>().ToList();
        //List<Vector2> 
    }
}
