using System;
using UnityEngine;

namespace Assets.Sources.InputService
{
    public interface IInputService
    {
        event Action<Vector2> Moved;
        event Action Stopped;
        event Action<Vector2> Rotated;
        event Action<Vector2> Clicked;
    }
}