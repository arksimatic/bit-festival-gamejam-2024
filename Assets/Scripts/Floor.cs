using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Floor : MonoBehaviour
{
    public List<Vector2> DoorPlaceholders;
    public int DoorsToGenerate;
    public List<Door> Doors = new();
    public int PlayerCount;


    public void Awake()
    {
        Doors = this.GetComponentsInChildren<Door>().ToList();

    }
    public void Start()
    {
        GenerateDoors(4);
        Doors = this.GetComponentsInChildren<Door>().ToList();

        foreach (var door in Doors)
        {
            door.Floor = this;
        }

    }

    private void ShuffleDoor()
    {

        var first = Doors.GetRandomElement();
        var second = Doors.GetRandomElement();

        while (first == second)
        {
            second = Doors.GetRandomElement();
        }

        var firstPos = first.transform.position;
        var secondPos = second.transform.position;
        LeanTween.move(first.gameObject, secondPos, 1f).setEaseInOutSine();
        LeanTween.move(second.gameObject, firstPos, 1f).setEaseInOutSine();

    }
    public void GenerateDoors(Int32 doorAmount)
    {
        for (int i = 0; i < doorAmount; i++)
            GenerateDoorAtEmptyPlaceholder();
    }
    public void GenerateDoorAtEmptyPlaceholder()
    {
        Doors = this.GetComponentsInChildren<Door>().ToList();
    }

    public void StartShuffle()
    {
        if (Random.Range(0f, 1f) <= 0.5f)
        {
            var randTime = Random.Range(1, 10);
            Invoke(nameof(ShuffleDoor), randTime);
        }
    }
}