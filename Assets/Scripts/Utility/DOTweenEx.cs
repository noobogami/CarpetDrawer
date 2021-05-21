using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace Garaj.Utility
{
    // ReSharper disable InconsistentNaming
    public static class DOTweenEx
    {
        public static TweenerCore<Vector3, Vector3, VectorOptions> DOMoveYIncrease(this Transform target, float increaseAmount, float time) => target.DOMoveY(target.transform.position.y + increaseAmount, time);
        public static TweenerCore<Vector3, Vector3, VectorOptions> DOMoveXIncrease(this Transform target, float increaseAmount, float time) => target.DOMoveX(target.transform.position.x + increaseAmount, time);
        public static TweenerCore<Vector3, Vector3, VectorOptions> DOMoveZIncrease(this Transform target, float increaseAmount, float time) => target.DOMoveZ(target.transform.position.z + increaseAmount, time);
        public static TweenerCore<Vector3, Vector3, VectorOptions> DOLocalMoveYIncrease(this Transform target, float increaseAmount, float time) => target.DOLocalMoveY(target.transform.localPosition.y + increaseAmount, time);

        public static TweenerCore<Vector3, Vector3, VectorOptions> DOLocalMoveXIncrease(this Transform target, float increaseAmount, float time) => target.DOLocalMoveX(target.transform.localPosition.x + increaseAmount, time);
        public static TweenerCore<Vector3, Vector3, VectorOptions> DOLocalMoveZIncrease(this Transform target, float increaseAmount, float time) => target.DOLocalMoveZ(target.transform.localPosition.z + increaseAmount, time);
    }
}
