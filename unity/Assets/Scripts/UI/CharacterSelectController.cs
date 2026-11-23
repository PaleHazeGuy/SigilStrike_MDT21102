using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSelectController : MonoBehaviour
{
    [SerializeField] private CharacterSelectSlot[] slots;
    [Tooltip("Dimmed while a wizard is hovered")]
    [SerializeField] private Graphic[] dimmedExtras;
    [SerializeField] private RectTransform cursor;
    [SerializeField] private float hoverScale = 1.3f;
    [SerializeField] private Color dimColor = new Color(0.6f, 0.6f, 0.6f, 1f);
    [SerializeField] private float smoothing = 12f;
    [Tooltip("Scene to load on pick")]
    [SerializeField] private string nextScene;

    private IWandInput input;
    private CharacterSelectSlot hovered;

    private void Awake()
    {
        input = GetComponent<IWandInput>();
    }

    private void OnEnable()
    {
        Cursor.visible = false;
    }

    private void OnDisable()
    {
        Cursor.visible = true;
    }

    private void Update()
    {
        Vector2 pointer = input.PointerScreenPos;
        cursor.position = pointer;

        hovered = FindHovered(pointer);
        UpdateVisuals();

        if (hovered != null && input.DrawPressedThisFrame)
        {
            Select(hovered);
        }
    }

    private CharacterSelectSlot FindHovered(Vector2 pointer)
    {
        foreach (CharacterSelectSlot slot in slots)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(slot.HitArea, pointer, null))
            {
                return slot;
            }
        }

        return null;
    }

    private void UpdateVisuals()
    {
        float blend = 1f - Mathf.Exp(-smoothing * Time.deltaTime);
        bool anyHovered = hovered != null;

        foreach (CharacterSelectSlot slot in slots)
        {
            bool isHovered = slot == hovered;
            float scale = isHovered ? hoverScale : 1f;
            Color tint = anyHovered && !isHovered ? dimColor : Color.white;
            slot.Tick(scale, tint, isHovered ? 1f : 0f, blend);
        }

        Color extraTint = anyHovered ? dimColor : Color.white;
        foreach (Graphic extra in dimmedExtras)
        {
            extra.color = Color.Lerp(extra.color, extraTint, blend);
        }
    }

    private void Select(CharacterSelectSlot slot)
    {
        SelectedCharacter.Id = slot.CharacterId;

        if (!string.IsNullOrEmpty(nextScene))
        {
            SceneManager.LoadScene(nextScene);
        }
    }
}
