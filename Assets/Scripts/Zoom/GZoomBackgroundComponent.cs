using Garaj.Utility;
using UnityEngine;

namespace Garaj.Zoom
{
    public class GZoomBackgroundComponent : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer sprite;
        [SerializeField] private GZoomProfileKey profile;

        [SerializeField] private ZoomProfileStructure profileProperties; //TODO: read profile from here
        private SpriteRenderer Sprite
        {
            get
            {
                if(sprite is null)
                    sprite = GetComponent<SpriteRenderer>();
                return sprite;
            }
        }

        internal void Init()
        {
            GZoom.Ins.Init(profile, Sprite);
        }
        internal void Init(GZoomProfileKey newProfile)
        {
            profile = newProfile;
            Init();
        }
        
        
        
#if UNITY_EDITOR
        
        private float zoomIn = 2;
        private float zoomOut = 6;
        private float threshold = 0.1f;
        private Vector3 _minFinalLimit = Vector3.zero;
        private Vector3 _maxFinalLimit = Vector3.one;
        private Vector3 _minElasticLimitWide = Vector3.zero;
        private Vector3 _maxElasticLimitWide = Vector3.zero;
        private Vector3 _minElasticLimitSquare = Vector3.zero;
        private Vector3 _maxElasticLimitSquare = Vector3.zero;

        internal Vector3 LimitSize => _maxFinalLimit - _minFinalLimit;
        private void OnDrawGizmos()
        {
            SetFinalLimits();
            SetElasticLimitsWide();
            SetElasticLimitsSquare();
            var bgPos = transform.position;
            
            /*Gizmos.color = Color.grey;
            Gizmos.DrawWireCube(camPos, new Vector3(zoomOut * Camera.main.aspect * 2, zoomOut * 2));*/
            
            /*Gizmos.color = Color.grey;
            Gizmos.DrawWireCube(camPos, new Vector3(zoomIn * Camera.main.aspect * 2, zoomIn * 2));*/
            
            Gizmos.color = Color.yellow; //boarder limit
            Gizmos.DrawWireCube(bgPos, _maxElasticLimitWide - _minElasticLimitWide);
            
            Gizmos.color = Color.green; //boarder limit
            Gizmos.DrawWireCube(bgPos, _maxElasticLimitSquare - _minElasticLimitSquare);

            /*Gizmos.color = Color.green; //boarder limit with threshold
            Gizmos.DrawWireCube(bgPos, _maxElasticLimit - _minElasticLimit - (Vector3.one * (5 * threshold)));*/

            /*if(!GZoomProfile.GetProfile(profile).LockPosition)
            {
                Gizmos.color = Color.cyan; //Camera position limit rect 
                Gizmos.DrawWireCube(bgPos, _maxElasticLimitWide - _minElasticLimitWide - 2 * ScreenDiameter);
            }*/

            Gizmos.color = Color.red; //Final Boarder
            Gizmos.DrawWireCube(bgPos, _maxFinalLimit - _minFinalLimit);
        }
        
        private Vector3 ScreenDiameter => new Vector3(CameraWidth, CameraHeight);
        private float CameraWidth => Camera.main.orthographicSize * Camera.main.aspect;
        private float CameraHeight => Camera.main.orthographicSize;
        private Vector2 IsElastic(Vector2 value) => GZoomProfile.GetProfile(profile).elastic? value : Vector2.zero;
        
        private void SetElasticLimitsWide()
        {
            var aspect = 19.5f / 9;
            var bgSize = sprite.size * sprite.transform.localScale;
            _maxElasticLimitWide = _maxFinalLimit - (Vector3) IsElastic(GZoomProfile.GetProfile(profile).boarderOffset);
            _minElasticLimitWide = _minFinalLimit + (Vector3) IsElastic(GZoomProfile.GetProfile(profile).boarderOffset);

            if (GZoomProfile.GetProfile(profile).elastic) return;
            if (bgSize.x / bgSize.y > aspect)
            {
                _maxElasticLimitWide = _maxElasticLimitWide.ChangeX((bgSize.x - bgSize.y * aspect) / -2, true);
                _minElasticLimitWide = _minElasticLimitWide.ChangeX((bgSize.x - bgSize.y * aspect) / 2, true);
            }
            else
            {
                _maxElasticLimitWide = _maxElasticLimitWide.ChangeY((bgSize.y - bgSize.x / aspect) / -2, true);
                _minElasticLimitWide = _minElasticLimitWide.ChangeY((bgSize.y - bgSize.x / aspect) / 2, true);
            }
        }
        private void SetElasticLimitsSquare()
        {
            var aspect = 4 / 3f;
            var bgSize = sprite.size * sprite.transform.localScale;;
            _maxElasticLimitSquare = _maxFinalLimit - (Vector3) IsElastic(GZoomProfile.GetProfile(profile).boarderOffset);
            _minElasticLimitSquare = _minFinalLimit + (Vector3) IsElastic(GZoomProfile.GetProfile(profile).boarderOffset);

            if (GZoomProfile.GetProfile(profile).elastic) return;
            if (bgSize.x / bgSize.y > aspect)
            {
                _maxElasticLimitSquare = _maxElasticLimitSquare.ChangeX((bgSize.x - bgSize.y * aspect) / -2, true);
                _minElasticLimitSquare = _minElasticLimitSquare.ChangeX((bgSize.x - bgSize.y * aspect) / 2, true);
            }
            else
            {
                _maxElasticLimitSquare = _maxElasticLimitSquare.ChangeY((bgSize.y - bgSize.x / aspect) / -2, true);
                _minElasticLimitSquare = _minElasticLimitSquare.ChangeY((bgSize.y - bgSize.x / aspect) / 2, true);
            }
        }
        private void SetFinalLimits()
        {
            var backgroundPosition = transform.position;
            var backgroundSize = sprite.size * sprite.transform.localScale;;
            _maxFinalLimit = new Vector2(backgroundPosition.x + backgroundSize.x / 2f,
                backgroundPosition.y + backgroundSize.y / 2f);
            _minFinalLimit = new Vector2(backgroundPosition.x - backgroundSize.x / 2f,
                backgroundPosition.y - backgroundSize.y / 2f);
        }
#endif
    }
}
