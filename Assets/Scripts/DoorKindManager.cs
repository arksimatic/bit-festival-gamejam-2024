using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class DoorKindManager : MonoBehaviour
{
    public Sprite Door1Closed;
    public Sprite Door2Closed;
    public Sprite Door3Closed;
    public Sprite Door1Open;
    public Sprite Door2Open;
    public Sprite Door3Open;
    public List<Sprite> Door1ClosedOther;
    public List<Sprite> Door2ClosedOther;
    public List<Sprite> Door3ClosedOther;

    public List<DoorKind> GetDoorKinds()
    {
        return new List<DoorKind>()
        {
            new DoorKind()
            {
                DoorClosed = Door1Closed,
                DoorOpen = Door1Open,
                DoorClosedOthers = Door1ClosedOther
            },
            new DoorKind()
            {
                DoorClosed = Door2Closed,
                DoorOpen = Door2Open,
                DoorClosedOthers = Door2ClosedOther
            },
            new DoorKind()
            {
                DoorClosed = Door3Closed,
                DoorOpen = Door3Open,
                DoorClosedOthers = Door3ClosedOther
            },
        };
    }
}
