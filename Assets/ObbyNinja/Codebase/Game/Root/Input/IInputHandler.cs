using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInputHandler 
{
    public Vector2 Movement { get; }

    public event Action<Vector2> OnMouseDeltaChange;

    public event Action OnJump;
}
