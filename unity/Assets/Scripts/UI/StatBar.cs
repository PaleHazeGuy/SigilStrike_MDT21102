using System;
using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class StatBar : MonoBehaviour
{
    [SerializeField] private Image fill;
    [SerializeField, Range(0f, 1f)] private float amount = 1f;
    [Tooltip("Colour the fill by amount")]
    [SerializeField] private bool tintByAmount;
    [Tooltip("Left = empty, right = full")]
    [SerializeField] private Gradient amountColors = new Gradient();

    public event Action<float> Changed;

    public float Amount
    {
        get => amount;
        set
        {
            float clamped = Mathf.Clamp01(value);
            bool unchanged = Mathf.Approximately(clamped, amount) && (fill == null || Mathf.Approximately(fill.fillAmount, amount));
            if (unchanged)
            {
                return;
            }

            amount = clamped;
            Apply();
        }
    }

    private void OnEnable()
    {
        Apply();
    }

    private void Apply()
    {
        if (fill != null)
        {
            fill.fillAmount = amount;
            if (tintByAmount)
            {
                fill.color = amountColors.Evaluate(amount);
            }
        }

        Changed?.Invoke(amount);
    }

#if UNITY_EDITOR
    // Inspector edits: apply on the next editor tick so the bar moves live
    private void OnValidate()
    {
        UnityEditor.EditorApplication.delayCall += () =>
        {
            if (this != null)
            {
                Apply();
            }
        };
    }
#endif
}
