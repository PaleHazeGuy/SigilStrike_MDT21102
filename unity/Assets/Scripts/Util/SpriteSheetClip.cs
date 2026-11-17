using System;
using UnityEngine;

[Serializable]
public class SpriteSheetClip
{
    [SerializeField] private string clipName = "idle";
    [SerializeField] private Texture2D sheet;
    [SerializeField] private int columns = 1;
    [SerializeField] private int rows = 1;
    [SerializeField] private int frameCount = 1;
    [SerializeField] private float fps = 12f;
    [SerializeField] private bool loop = true;
    [Tooltip("Clip to play after")]
    [SerializeField] private string nextClip;

    public string ClipName => clipName;
    public float Fps => fps;
    public bool Loop => loop;
    public string NextClip => nextClip;

    public Sprite[] BuildSprites(Vector2 pivotPixels, float pixelsPerUnit)
    {
        int cellWidth = sheet.width / columns;
        int cellHeight = sheet.height / rows;
        var pivot = new Vector2(pivotPixels.x / cellWidth, pivotPixels.y / cellHeight);
        var sprites = new Sprite[frameCount];

        for (int i = 0; i < frameCount; i++)
        {
            int column = i % columns;
            int row = i / columns;
            // Row 0 is the top of the sheet, Unity's origin is bottom-left
            var rect = new Rect(column * cellWidth, sheet.height - (row + 1) * cellHeight, cellWidth, cellHeight);
            sprites[i] = Sprite.Create(sheet, rect, pivot, pixelsPerUnit);
            sprites[i].name = $"{clipName}_{i:000}";
        }

        return sprites;
    }
}
