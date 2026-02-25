using UnityEngine;

public class HPBarFollow : MonoBehaviour
{
    public Camera cam;
    public Canvas canvas;
    public Transform target;
    public Vector3 worldOffset = new Vector3(0f, 1.4f, 0f);

    RectTransform rt;
    RectTransform canvasRt;

    void Awake()
    {
        rt = GetComponent<RectTransform>();
        if (!cam) cam = Camera.main;
        if (!canvas) canvas = GetComponentInParent<Canvas>();
        canvasRt = canvas.transform as RectTransform;
    }

    void LateUpdate()
    {
        if (!target || !cam || !canvas || !canvasRt) return;

        Vector3 worldPos = target.position + worldOffset;
        Vector3 screenPos = cam.WorldToScreenPoint(worldPos);

        // za kamerou -> schovat
        if (screenPos.z < 0f)
        {
            if (rt.gameObject.activeSelf) rt.gameObject.SetActive(false);
            return;
        }
        if (!rt.gameObject.activeSelf) rt.gameObject.SetActive(true);

        // Správná kamera pro převod:
        Camera uiCam = (canvas.renderMode == RenderMode.ScreenSpaceOverlay) ? null : cam;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRt, screenPos, uiCam, out Vector2 localPoint))
        {
            rt.anchoredPosition = localPoint;
        }
    }
}