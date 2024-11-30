using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class FloorManager : MonoBehaviour
{
    public List<Floor> Floors = new();
    public List<Door> Doors = new();

    public void Awake() 
    {
        //foreach (var wall in Walls)
        //{
        //    Doors.AddRange(wall.GetComponentsInChildren<Door>());
        //}
    }

    public void Start()
    {
        for (int i = 0; i < Floors.Count; i++)
        {
            List<Door> allAvailableDoorsInWall = GetPossibleDoorsFromFromWall(Floors[i]);
            
            Door firstDoorsUp = RandomlySelectDoors(allAvailableDoorsInWall);



            for(int j = 0; j < Floors[i].Doors.Count; j++)
            {
                List<Door> doorsAbove = new List<Door>();
                List<Door> doorsSameFloor = new List<Door>();
                List<Door> doorsUnder = new List<Door>();

                if (i != 0)
                {
                    doorsUnder = Floors[i - 1].Doors.Where(door => !door.IsAssignedTo).ToList();
                }

                if(i != Floors.Count - 1)
                {
                    doorsAbove = Floors[i + 1].Doors.Where(door => !door.IsAssignedTo).ToList();
                }

                doorsSameFloor = Floors[i].Doors.Where(door => !door.IsAssignedTo).ToList();

                // At least one doors to floor up


                // Randomize rest


            }
        }
    }

    public Door RandomlySelectDoors(List<Door> doors)
    {
        return doors[Random.Range(0, doors.Count)];
    }

    public void AssignDors(Door doorsFrom, Door doorsTo)
    {
        doorsFrom.TargetDoor = doorsTo;
        doorsTo.IsAssignedTo = true;
    }

    public List<Door> GetPossibleDoorsToFromWall(Floor floor)
    {
        return floor.Doors.Where(door => !door.IsAssignedTo).ToList();
    }
    public List<Door> GetPossibleDoorsFromFromWall(Floor floor)
    {
        return floor.Doors.Where(door => !door.IsAssignedFrom).ToList();
    }
}
