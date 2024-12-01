using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class FloorManager : MonoBehaviour
{
    public Int32 MinDoorsPerFloor;
    public Int32 MaxDoorsPerFloor;
    public GameObject DoorPrefab;
    public List<Sprite> DoorOpenSprites;
    public List<Sprite> DoorClosedSprites;
    public List<Sprite> FloorSprites;
    public List<Floor> Floors = new();
    public List<Door> Doors = new();
    [SerializeField] private bool GenerateFloors;
    [SerializeField] private int FloorNumber;

    [SerializeField] private GameObject FloorPrefab;

    [SerializeField] private float startPosition;
    [SerializeField] private float floorHeight;
    [SerializeField] private float heightOffset;


    public void Awake()
    {
        if (GenerateFloors)
        {
            for (var i = 0; i < FloorNumber; i++)
            {
                var position = new Vector3(0, startPosition + i * (floorHeight + heightOffset), 0);
                var floor = Instantiate(FloorPrefab, position, Quaternion.identity);
                Floors.Add(floor.GetComponent<Floor>());
            }

            var exitDoor = Floors.Last().gameObject.GetComponentInChildren<ExitDoor>(true);
            exitDoor.gameObject.SetActive(true);
        }
    }

    public void Start()
    {
        AssignFloorSprites();
        GenerateDoors();
        AssignDoors();
    }

    public void AssignFloorSprites()
    {
        foreach (var floor in Floors)
        {
            FloorSprites.Shuffle();
            floor.GetComponentInChildren<SpriteRenderer>().sprite = FloorSprites.First();
        }
    }
    public void GenerateDoors()
    {
        foreach (var floor in Floors)
        {
            Int32 amountOfDoorsToGenerate = Random.Range(MinDoorsPerFloor, MaxDoorsPerFloor + 1);
            GenerateDoors(floor, amountOfDoorsToGenerate);
        }
    }
    public void GenerateDoors(Floor floor, Int32 doorAmount)
    {
        // if using placeholders
        List<Vector2> doorsPossiblePossitions = floor.DoorPlaceholders;
        doorsPossiblePossitions.Shuffle();
        List<Tuple<Sprite, Sprite>> doorSprites = DoorOpenSprites.Zip(DoorClosedSprites, (a, b) => Tuple.Create(a, b)).ToList();
        doorSprites.Shuffle();
        for (int i = 0; i < doorAmount; i++)
            GenerateDoor(floor, doorsPossiblePossitions[i], doorSprites[i].Item1, doorSprites[i].Item2);

        // if using offset
        //Single width = floor.DoorsOffset * doorAmount;
        //for(Single position = - width / 2; position <= width / 2; position += floor.DoorsOffset)
        //{
        //    GenerateDoor(floor, new Vector2(position, 0));
        //}
    }
    public void GenerateDoor(Floor floor, Vector2 position, Sprite open, Sprite closed)
    {
        var doorInstance = Instantiate(DoorPrefab);
        doorInstance.GetComponent<Door>().Floor = floor;
        doorInstance.transform.SetParent(floor.transform);
        doorInstance.transform.localPosition = position;
        doorInstance.GetComponentInChildren<SpriteRenderer>().sprite = closed;
        doorInstance.GetComponent<Door>().Closed = closed;
        doorInstance.GetComponent<Door>().Opened = open;
    }

    public void AssignDoors()
    {
        for (int i = 0; i < Floors.Count; i++)
        {
            // At least one doors up, except last floor (cannot go up, duh!)
            if (i != Floors.Count - 1)
            {
                Door firstDoorsUp = RandomlySelectDoors(GetPossibleDoorsFrom(Floors[i]));
                Door randomDoorsFromUpperFloor = RandomlySelectDoors(GetPossibleDoorsTo(Floors[i + 1]));
                AssignDoors(firstDoorsUp, randomDoorsFromUpperFloor);
            }

            List<Door> doorsSameFloorFrom = GetPossibleDoorsFrom(Floors[i]);

            List<Door> doorsLowerFloor = i != 0 ? GetPossibleDoorsTo(Floors[i - 1]) : new List<Door>();
            List<Door> doorsSameFloorTo = GetPossibleDoorsTo(Floors[i]);
            List<Door> doorsUpperFloor = i != Floors.Count - 1 ? GetPossibleDoorsTo(Floors[i + 1]) : new List<Door>();

            doorsSameFloorFrom.Shuffle();
            doorsLowerFloor.Shuffle();
            doorsSameFloorFrom.Shuffle();
            doorsUpperFloor.Shuffle();

            if (doorsSameFloorFrom.Count < doorsLowerFloor.Count)
                Debug.LogWarning("Potentially shouldn't exist");

            AssignManyDoorsWithDifferentLengths(doorsSameFloorFrom, doorsLowerFloor);

            // update needed
            doorsSameFloorFrom = GetPossibleDoorsFrom(Floors[i]);
            doorsSameFloorFrom.Shuffle();


            List<Door> sameFloorAndUpperFloorCombined = doorsSameFloorFrom.Concat(doorsUpperFloor).ToList();
            sameFloorAndUpperFloorCombined.Shuffle();

            AssignManyDoorsWithDifferentLengths(doorsSameFloorFrom, sameFloorAndUpperFloorCombined);
        }
    }
    public Door RandomlySelectDoors(List<Door> doors)
    {
        return doors[Random.Range(0, doors.Count)];
    }

    public void AssignDoors(Door doorsFrom, Door doorsTo)
    {
        doorsFrom.TargetDoor = doorsTo;
        doorsTo.IsAssignedTo = true;
    }

    public void AssignManyDoors(List<Door> doorsFrom, List<Door> doorsTo)
    {
        if (doorsFrom.Count != doorsTo.Count)
            Debug.LogWarning("List length is not the same!");

        for (int i = 0; i < doorsFrom.Count; i++)
            AssignDoors(doorsFrom[i], doorsTo[i]);
    }
    public void AssignManyDoorsWithDifferentLengths(List<Door> doorsFrom, List<Door> doorsTo)
    {
        Int32 amountOfAssignments = Math.Min(doorsFrom.Count, doorsTo.Count);

        if (amountOfAssignments == 0)
            return;

        if (doorsFrom.Count == 1 && doorsTo.Count == 1)
            Debug.LogWarning("Only one assignment left,  I had to allow it at this point");

        while (IsAnySelfAssignment(doorsFrom, doorsTo))
        {
            doorsFrom.Shuffle();
            doorsTo.Shuffle();
        }

        for (int i = 0; i < amountOfAssignments; i++)
            AssignDoors(doorsFrom[i], doorsTo[i]);
    }
    public Boolean IsAnySelfAssignment(List<Door> doorsFrom, List<Door> doorsTo)
    {
        Int32 amountOfAssignments = Math.Min(doorsFrom.Count, doorsTo.Count);

        for (int i = 0; i < amountOfAssignments; i++)
        {
            if (doorsFrom[i] == doorsTo[i])
                return true;
        }

        return false;
    }
    public List<Door> GetPossibleDoorsTo(Floor floor)
    {
        return floor.Doors.Where(door => !door.IsAssignedTo).ToList();
    }
    public List<Door> GetPossibleDoorsFrom(Floor floor)
    {
        return floor.Doors.Where(door => !door.IsAssignedFrom).ToList();
    }
}