using System;
using Garaj.Zoom;
using UnityEngine;

namespace DefaultNamespace
{
    public class Background : MonoBehaviour
    {
        private void Start()
        {
            var sprite = GetComponent<SpriteRenderer>();
            transform.localPosition = new Vector3(sprite.size.x * transform.localScale.x / 2,
                sprite.size.y * transform.localScale.y / 2);
            GZoom.Ins.Init(GZoomProfileKey.Main, sprite);
        }
    }
}