using System;
using TMPro;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Sprite Closed;
    public Sprite Opened;
    public TextMeshPro DoorDebugText;
    public Boolean IsAssignedTo = false;
    public Door TargetDoor = null;
    public Floor Floor;
    public Boolean IsAssignedFrom => TargetDoor != null;
    public void Start()
    {
        DoorDebugText.GetComponent<TextMeshPro>().text = "test";
    }
}