using DG.Tweening;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance { get; private set; }
    private Camera mainCamera;
    [SerializeField]
    Ease ease;
    private void Awake()
    {
        Instance = this;
        mainCamera = Camera.main;
    }

    Tweener cameraTweener;
    public void ZoomOut()
    {
        cameraTweener ??= mainCamera.DOFieldOfView(68, 1)
        .SetEase(Ease.OutCirc)
        .OnComplete(() =>
        {
            cameraTweener = null;
            
        });
    }

    public void ZoomIn()
    {
        cameraTweener ??= mainCamera.DOFieldOfView(60, 0.5f)
        .SetEase(ease)
        .OnComplete(() => { cameraTweener = null; });
    }
}
