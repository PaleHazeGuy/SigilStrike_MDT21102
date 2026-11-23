using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class PlayerPortrait : MonoBehaviour
{
    [SerializeField] private WizardProfile[] wizards;
    [Tooltip("Editor preview, and if none was picked")]
    [SerializeField] private string defaultId = "bell";
    [SerializeField] private Image portraitImage;
    [SerializeField] private Text nameText;
    [SerializeField] private StatBar healthBar;
    [Tooltip("Hurt portrait at or below this")]
    [SerializeField, Range(0f, 1f)] private float lowHealthThreshold = 0.3f;
    [Tooltip("Ring hole width")]
    [SerializeField] private float ringDiameter = 172f;
    [Tooltip("Ring hole centre")]
    [SerializeField] private Vector2 ringCenter;
    [Tooltip("Gap under the ring's top edge")]
    [SerializeField] private float topInset = 10f;

    private WizardProfile current;
    private bool hurt;

    private void OnEnable()
    {
        if (healthBar != null)
        {
            healthBar.Changed += OnHealthChanged;
        }

        if (!Application.isPlaying)
        {
            Preview();
        }
    }

    private void OnDisable()
    {
        if (healthBar != null)
        {
            healthBar.Changed -= OnHealthChanged;
        }
    }

    private void Start()
    {
        if (!Application.isPlaying)
        {
            return;
        }

        Select(string.IsNullOrEmpty(SelectedCharacter.Id) ? defaultId : SelectedCharacter.Id);
        hurt = IsHurt();
        Refresh();
    }

    private void OnHealthChanged(float amount)
    {
        if (current == null)
        {
            return;
        }

        bool isHurt = amount <= lowHealthThreshold;
        if (isHurt != hurt)
        {
            hurt = isHurt;
            Refresh();
        }
    }

#if UNITY_EDITOR
    // Inspector edits: redraw on the next editor tick so changes show live
    private void OnValidate()
    {
        UnityEditor.EditorApplication.delayCall += () =>
        {
            if (this == null)
            {
                return;
            }

            if (Application.isPlaying)
            {
                if (current != null)
                {
                    Refresh();
                }
            }
            else
            {
                Preview();
            }
        };
    }
#endif

    private bool IsHurt()
    {
        return healthBar != null && healthBar.Amount <= lowHealthThreshold;
    }

    private void Preview()
    {
        Select(defaultId);
        if (current == null)
        {
            return;
        }

        hurt = IsHurt();
        Refresh();
    }

    private void Select(string id)
    {
        foreach (WizardProfile wizard in wizards)
        {
            if (wizard.Id == id)
            {
                current = wizard;
                if (nameText != null && nameText.text != wizard.DisplayName)
                {
                    nameText.text = wizard.DisplayName;
                }

                return;
            }
        }

        current = null;
        if (Application.isPlaying)
        {
            Debug.LogWarning($"No wizard profile with id '{id}'", this);
        }
    }

    private void Refresh()
    {
        Sprite sprite = hurt ? current.PortraitHurt : current.Portrait;
        if (sprite == null)
        {
            return;
        }

        // Same width for every portrait, top-aligned, so heads fill the ring evenly
        float aspect = sprite.rect.width / sprite.rect.height;
        float width = ringDiameter * current.PortraitScale;
        float height = width / aspect;
        float top = ringCenter.y + ringDiameter * 0.5f - topInset;
        var size = new Vector2(width, height);
        var position = new Vector2(ringCenter.x, top - height * 0.5f) + current.PortraitOffset;

        if (portraitImage.sprite != sprite)
        {
            portraitImage.sprite = sprite;
        }

        RectTransform rect = portraitImage.rectTransform;
        if (rect.sizeDelta != size)
        {
            rect.sizeDelta = size;
        }

        if (rect.anchoredPosition != position)
        {
            rect.anchoredPosition = position;
        }
    }
}
