using UnityEngine;

public interface IWandInput
{
    Vector2 PointerScreenPos { get; }
    bool DrawHeld { get; }
    bool DrawPressedThisFrame { get; }
    bool DrawReleasedThisFrame { get; }
    bool PatternPressedThisFrame { get; }
}
