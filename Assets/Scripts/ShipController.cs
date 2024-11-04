using DG.Tweening;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    private Tweener shipUpDownTweener;
    private void Start()
    {
        shipUpDownTweener = transform.DOMoveY(Random.Range(-2.0f, -2.9f), Random.Range(1.0f, 1.25f))
                            .SetEase(Ease.InOutSine)
                            .SetLoops(-1, LoopType.Yoyo);
    }

    private void OnDestroy()
    {
        shipUpDownTweener.Kill();
    }
}
