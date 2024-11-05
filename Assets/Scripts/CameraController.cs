using DG.Tweening;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance { get; private set; }
    private Camera mainCamera;
    [SerializeField] Ease zoomOutEase = Ease.OutCirc;
    [SerializeField] Ease zoomInEase = Ease.OutBounce;
    [Header("Camera Shake")]
    [SerializeField] float duration = 0.1f;
    [SerializeField] Vector3 strength;
    [SerializeField] int vibrato = 10;
    [SerializeField] int randomness = 10;
    [SerializeField] bool fadeOut = true;
    [SerializeField] ShakeRandomnessMode randomnessMode = ShakeRandomnessMode.Harmonic;

    private void Awake()
    {
        Instance = this;
        mainCamera = Camera.main;
    }

    Tweener cameraTweener;
    public void ZoomOut()
    {
        cameraTweener ??= mainCamera.DOFieldOfView(75, 1)
        .SetEase(zoomOutEase)
        .OnComplete(() =>
        {
            cameraTweener = null;

        });
    }

    public void ZoomIn()
    {
        cameraTweener ??= mainCamera.DOFieldOfView(60, 0.5f)
        .SetEase(zoomInEase)
        .OnComplete(() => { cameraTweener = null; });
    }

    [ContextMenu("CameraShake")]
    public void CameraShake()
    {
        mainCamera.DOShakePosition(duration, strength, vibrato, randomness, fadeOut, randomnessMode);
    }
}
