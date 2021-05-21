using System;
using DG.Tweening;
using UnityEngine;

namespace Garaj.Utility
{
    public class Rotator : MonoBehaviour
    {
        public float speed;

        private void Awake()
        {
            TurnOn();
        }

        public void TurnOn()
        {
            transform.DOLocalRotate(new Vector3(0, 0, Math.Min(speed, 180)), speed < 180? 1: 180 / speed, RotateMode.FastBeyond360)
                .SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Incremental);
        }

        public void TurnOff()
        {
            transform.DOKill();
            transform.rotation = Quaternion.identity;
        }
    }
}