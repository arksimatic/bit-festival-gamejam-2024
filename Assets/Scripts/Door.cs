using System;
using Unity.VisualScripting;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Boolean IsAssignedTo = false;
    public Boolean IsAssignedFrom => TargetDoor != null;
    public Door TargetDoor = null;
}
