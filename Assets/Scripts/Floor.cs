using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Floor : MonoBehaviour
{
    public List<Vector2> DoorPlaceholders;
    public int PlayerCount;
    public List<Door> Doors => GetComponentsInChildren<Door>().ToList();

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


    public void StartShuffle()
    {
        if (Random.Range(0f, 1f) <= 0.5f)
        {
            var randTime = Random.Range(1, 10);
            Invoke(nameof(ShuffleDoor), randTime);
        }
    }
}