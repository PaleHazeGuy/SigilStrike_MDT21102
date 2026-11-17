using UnityEngine;

public class SpriteTestController : MonoBehaviour
{
    [SerializeField] private SpriteSheetAnimator animator;
    [SerializeField] private string hitClip = "hit";

    private IWandInput input;

    private void Awake()
    {
        input = GetComponent<IWandInput>();
    }

    private void Update()
    {
        if (input.PatternPressedThisFrame)
        {
            animator.Play(hitClip);
        }
    }
}
