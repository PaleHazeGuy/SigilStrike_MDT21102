using UnityEngine;

[ExecuteAlways]
public class FitCameraToDesign : MonoBehaviour
{
  [SerializeField] private Camera targetCamera;
  [Tooltip("World units tall at design ratio")]
  [SerializeField] private float designHeight = 10f;
  [SerializeField] private float designAspect = 16f / 9f;

  private void OnEnable()
  {
    Fit();
  }

  private void Update()
  {
    Fit();
  }

  private void Fit()
  {
    if (targetCamera == null || Screen.height <= 0)
    {
      return;
    }

    float aspect = (float)Screen.width / Screen.height;
    float height = aspect >= designAspect ? designHeight : designHeight * designAspect / aspect;
    float size = height * 0.5f;

    if (!Mathf.Approximately(targetCamera.orthographicSize, size))
    {
      targetCamera.orthographicSize = size;
    }
  }
}
