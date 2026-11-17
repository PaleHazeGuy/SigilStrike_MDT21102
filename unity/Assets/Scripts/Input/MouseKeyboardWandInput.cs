using UnityEngine;
using UnityEngine.InputSystem;

public class MouseKeyboardWandInput : MonoBehaviour, IWandInput
{
    public Vector2 PointerScreenPos =>
        Mouse.current != null ? Mouse.current.position.ReadValue() : Vector2.zero;

    public bool DrawHeld =>
        Mouse.current != null && Mouse.current.leftButton.isPressed;

    public bool DrawPressedThisFrame =>
        Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame;

    public bool DrawReleasedThisFrame =>
        Mouse.current != null && Mouse.current.leftButton.wasReleasedThisFrame;

    public bool PatternPressedThisFrame =>
        (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame) ||
        (Mouse.current != null && Mouse.current.rightButton.wasPressedThisFrame);
}
