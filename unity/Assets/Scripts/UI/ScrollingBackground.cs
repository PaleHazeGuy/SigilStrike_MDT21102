using UnityEngine;
using UnityEngine.UI;

public class ScrollingBackground : MonoBehaviour
{
    [SerializeField] private RawImage image;
    [Tooltip("Canvas units per second")]
    [SerializeField] private Vector2 speed = new Vector2(-25f, 18f);
    [Tooltip("One tile in canvas units")]
    [SerializeField] private float tileSize = 417f;

    private Vector2 offset;

    private void Update()
    {
        offset += speed * Time.deltaTime / tileSize;
        offset.x = Mathf.Repeat(offset.x, 1f);
        offset.y = Mathf.Repeat(offset.y, 1f);

        Rect rect = image.rectTransform.rect;
        image.uvRect = new Rect(offset.x, offset.y, rect.width / tileSize, rect.height / tileSize);
    }
}
