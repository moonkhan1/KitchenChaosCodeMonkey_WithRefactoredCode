using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public interface IInputReader
{
    Vector3 Direction { get; }
    bool IsMoving { get; }
    event System.EventHandler OnInteraction;
    event System.EventHandler OnInteractionAlternative;
}
