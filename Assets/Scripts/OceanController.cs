using DG.Tweening;
using UnityEngine;

public class OceanController : MonoBehaviour
{
    [SerializeField] private Transform waterTileTransform;
    [SerializeField] float leftMostPosition = -300;
    [SerializeField] float duration = 5;
    [SerializeField] Ease ease;
    [SerializeField] LoopType loopType;

    private Tweener tweener;

    private void Start() {
       tweener = waterTileTransform.DOMoveX(leftMostPosition, duration).SetEase(ease).SetLoops(-1, loopType);
    }

    private void OnDestroy() {
        tweener.Kill();
    }
}
