using System;
using TMPro;
using UnityEngine;

public class Door : MonoBehaviour
{
    public TextMeshPro DoorDebugText;
    public Boolean IsAssignedTo = false;
    public Door TargetDoor = null;
    public Boolean IsAssignedFrom => TargetDoor != null;
    public void Start()
    {
        DoorDebugText.GetComponent<TextMeshPro>().text = "test";
    }
}