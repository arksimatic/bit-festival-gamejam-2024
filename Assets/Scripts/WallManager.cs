using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WallManager : MonoBehaviour
{
    public List<Wall> Walls = new();
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
        for (int i = 0; i < Walls.Count; i++)
        {
            List<Door> allAvailableDoorsInWall = GetPossibleDoorsFromFromWall(Walls[i]);
            
            Door firstDoorsUp = RandomlySelectDoors(allAvailableDoorsInWall);



            for(int j = 0; j < Walls[i].Doors.Count; j++)
            {
                List<Door> doorsAbove = new List<Door>();
                List<Door> doorsSameFloor = new List<Door>();
                List<Door> doorsUnder = new List<Door>();

                if (i != 0)
                {
                    doorsUnder = Walls[i - 1].Doors.Where(door => !door.IsAssignedTo).ToList();
                }

                if(i != Walls.Count - 1)
                {
                    doorsAbove = Walls[i + 1].Doors.Where(door => !door.IsAssignedTo).ToList();
                }

                doorsSameFloor = Walls[i].Doors.Where(door => !door.IsAssignedTo).ToList();

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

    public List<Door> GetPossibleDoorsToFromWall(Wall wall)
    {
        return wall.Doors.Where(door => !door.IsAssignedTo).ToList();
    }
    public List<Door> GetPossibleDoorsFromFromWall(Wall wall)
    {
        return wall.Doors.Where(door => !door.IsAssignedFrom).ToList();
    }
}
