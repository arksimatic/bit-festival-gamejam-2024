using UnityEngine;
using System.Collections.Generic;
using System.Linq;
public class Wall : MonoBehaviour
{
    public List<Door> Doors = new();
    public void Awake()
    {
        Doors = this.GetComponentsInChildren<Door>().ToList();
    }
}
