using System;
using UnityEngine;

public abstract class PlayerInput : MonoBehaviour
{
    public EventHandler OnInteract { get; set; }
    public abstract float ReadHorizontalMovement();
}