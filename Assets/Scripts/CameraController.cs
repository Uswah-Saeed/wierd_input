using DG.Tweening;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera mainCamera;
    private void Awake()
    {
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
            ZoomIn();
        });
    }

    public void ZoomIn()
    {
        cameraTweener ??= mainCamera.DOFieldOfView(60, 0.2f)
        .SetEase(Ease.OutBack)
        .OnComplete(() => { cameraTweener = null; });
    }
}
