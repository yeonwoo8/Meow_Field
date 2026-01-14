using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class LogoTween : MonoBehaviour
{
    void Start()
    {
        // 좌우로 살짝 흔들리는 효과 (바람 느낌)
        transform.DORotate(new Vector3(0, 0, 3f), 2f)
                 .SetLoops(-1, LoopType.Yoyo)
                 .SetEase(Ease.InOutSine);

        // 크기도 살짝 변하게 해서 더 자연스러움
        transform.DOScale(new Vector3(1.02f, 0.98f, 1f), 2f)
                 .SetLoops(-1, LoopType.Yoyo)
                 .SetEase(Ease.InOutSine);

        Image logoImage = GetComponent<Image>();
        if (logoImage != null)
        {
            logoImage.DOFade(0.8f, 1f) // 1초에 Alpha 0.8로 감소
                     .SetLoops(-1, LoopType.Yoyo)
                     .SetEase(Ease.InOutSine);
        }
    }
}
