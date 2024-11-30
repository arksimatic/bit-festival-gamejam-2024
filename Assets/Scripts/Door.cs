using System;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Boolean IsAssignedTo = false;
    public Door TargetDoor = null;
    public Boolean IsAssignedFrom => TargetDoor != null;
}