using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectSlot : MonoBehaviour
{
    [SerializeField] private string characterId;
    [Tooltip("Invisible rect used for hover")]
    [SerializeField] private RectTransform hitArea;
    [Tooltip("Images that get dimmed")]
    [SerializeField] private Graphic[] tinted;
    [Tooltip("Fades in on hover")]
    [SerializeField] private Graphic glow;

    public string CharacterId => characterId;
    public RectTransform HitArea => hitArea;

    public void Tick(float targetScale, Color targetTint, float targetGlow, float blend)
    {
        float scale = Mathf.Lerp(transform.localScale.x, targetScale, blend);
        transform.localScale = new Vector3(scale, scale, 1f);

        foreach (Graphic graphic in tinted)
        {
            graphic.color = Color.Lerp(graphic.color, targetTint, blend);
        }

        Color glowColor = glow.color;
        glowColor.a = Mathf.Lerp(glowColor.a, targetGlow, blend);
        glow.color = glowColor;
    }
}
