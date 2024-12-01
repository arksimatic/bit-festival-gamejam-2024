using System;
using System.Collections;
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
    public void OpenClose()
    {
        StartCoroutine(nameof(OpenWithDelayedClose));
    }
    public IEnumerator OpenWithDelayedClose()
    {
        this.GetComponentInChildren<SpriteRenderer>().sprite = Opened;
        yield return new WaitForSeconds(1.3f);
        this.GetComponentInChildren<SpriteRenderer>().sprite = Closed;
    }
}