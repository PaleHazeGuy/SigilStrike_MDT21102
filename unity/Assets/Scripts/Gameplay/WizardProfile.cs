using System;
using UnityEngine;

[Serializable]
public class WizardProfile
{
    [SerializeField] private string id;
    [SerializeField] private string displayName;
    [SerializeField] private Sprite portrait;
    [SerializeField] private Sprite portraitHurt;
    [Tooltip("1 = fits the ring")]
    [SerializeField] private float portraitScale = 1f;
    [SerializeField] private Vector2 portraitOffset;

    public string Id => id;
    public string DisplayName => displayName;
    public Sprite Portrait => portrait;
    public Sprite PortraitHurt => portraitHurt;
    public float PortraitScale => portraitScale;
    public Vector2 PortraitOffset => portraitOffset;
}
