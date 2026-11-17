using System.Collections.Generic;
using UnityEngine;

public class SpriteSheetAnimator : MonoBehaviour
{
    [SerializeField] private SpriteRenderer target;
    [SerializeField] private SpriteSheetClip[] clips;
    [SerializeField] private string startClip = "idle";
    [SerializeField] private float pixelsPerUnit = 100f;
    [Tooltip("Pivot in sheet pixels")]
    [SerializeField] private Vector2 pivotPixels = new Vector2(130f, 0f);

    private readonly Dictionary<string, SpriteSheetClip> clipsByName = new Dictionary<string, SpriteSheetClip>();
    private readonly Dictionary<string, Sprite[]> spritesByName = new Dictionary<string, Sprite[]>();
    private SpriteSheetClip current;
    private Sprite[] currentSprites;
    private float frameTime;

    private void Awake()
    {
        if (target == null)
        {
            target = GetComponent<SpriteRenderer>();
        }

        foreach (SpriteSheetClip clip in clips)
        {
            clipsByName[clip.ClipName] = clip;
            spritesByName[clip.ClipName] = clip.BuildSprites(pivotPixels, pixelsPerUnit);
        }

        Play(startClip);
    }

    private void Update()
    {
        if (current == null)
        {
            return;
        }

        frameTime += Time.deltaTime * current.Fps;
        int frame = (int)frameTime;

        if (frame >= currentSprites.Length)
        {
            if (current.Loop)
            {
                frameTime %= currentSprites.Length;
                frame = (int)frameTime;
            }
            else
            {
                Play(string.IsNullOrEmpty(current.NextClip) ? startClip : current.NextClip);
                return;
            }
        }

        target.sprite = currentSprites[frame];
    }

    public void Play(string clipName)
    {
        if (!clipsByName.TryGetValue(clipName, out SpriteSheetClip clip))
        {
            Debug.LogWarning($"No sprite clip named '{clipName}'", this);
            return;
        }

        current = clip;
        currentSprites = spritesByName[clipName];
        frameTime = 0f;
        target.sprite = currentSprites[0];
    }
}
