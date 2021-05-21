using System;
using System.Collections.Generic;
using DG.Tweening;
using Garaj.GLog;
using Garaj.Utility;
using UnityEngine;
using Color = UnityEngine.Color;

namespace Garaj.Zoom
{
    public class GZoom : SingletonPersistentNewGameobject<GZoom>
    {
        [SerializeField] private bool perspective;
        [SerializeField] private bool elastic;
        [Header("Values")]
        [SerializeField] private Vector2 boarderOffset = Vector2.one;
        [SerializeField] private float zoomIn = 2;
        [SerializeField] private float zoomOut = 0;
        [SerializeField] private float zoomElasticity = 0.5f;
        [SerializeField] private float threshold = 0.1f;
        [SerializeField] private float slideDamper = 0.9f;

        [Space]
        [SerializeField] private SpriteRenderer background;


        #region Initialize

        internal void Init(GZoomProfileKey profile, SpriteRenderer bgSprite)
        {
            GZoomProfile.SetActiveProfile(profile);
            background = bgSprite;
            SetValues();
            //background = backgroundComponent.GetSprite();

            _camera = Camera.main;

            Dragging = false;
            _drift = Vector3.zero;

            SetFinalLimits();
            SetElasticLimits();

            if (!(_camera is null))
            {
                _camera.orthographic = !perspective;
                if (!perspective)
                {
                    if (!elastic)
                        zoomOut = _bgSize.y / 2f - (_maxFinalLimit - _maxElasticLimit).y - IsElastic(threshold);
                    else if (zoomOut <= 0)
                        zoomOut = Math.Min(
                            _bgSize.y / 2f - (_maxFinalLimit - _maxElasticLimit).y,
                            _bgSize.x / _camera.aspect / 2f - (_maxFinalLimit - _maxElasticLimit).x 
                             );
                    if (!elastic)
                        _camera.orthographicSize = zoomOut;
                    if (_loadOnCenterOfImage)
                        _camera.transform.position = background.transform.position.ChangeZ(-_distanceFromBackground, true);
                }
                else
                {
                    _camera.fieldOfView = _fieldOfView;
                    var tanY = Mathf.Tan(AngleInRadian(_fieldOfView) / 2);
                    var tanX = Mathf.Tan(AngleInRadian(Camera.VerticalToHorizontalFieldOfView(_fieldOfView, _camera.aspect)) / 2);
                    var z = Mathf.Min(_bgSize.y / 2f / tanY, _bgSize.x / 2f / tanX);
                    _camera.transform.position = background.transform.position.ChangeZ(-z, true);
                }
            }

            if (!_lockPosition)
                LockDrag = false;
            _initialized = true;
        }

        private void SetValues()
        {
            var profile = GZoomProfile.GetActiveProfile();
            perspective = profile.perspective;
            _lockPosition = profile.lockPosition;
            _fieldOfView = profile.fieldOfView;
            _loadOnCenterOfImage = profile.loadOnCenterOfImage;
            _distanceFromBackground = profile.distanceFromBackground;
            elastic = profile.elastic;
            boarderOffset = profile.boarderOffset;
            zoomIn = profile.zoomIn;
            zoomOut = profile.zoomOut;
            zoomElasticity = profile.zoomElasticity;
            threshold = profile.threshold;
            slideDamper = profile.slideDamper;

            _bgSize = background.size * background.transform.localScale;

            if (!(Camera.main is null))
                Camera.main.transform.position = profile.CameraPosition;
        }

        private void SetFinalLimits()
        {
            var backgroundPosition = background.transform.position;
            _maxFinalLimit = new Vector2(backgroundPosition.x + _bgSize.x / 2f,
                backgroundPosition.y + _bgSize.y / 2f);
            _minFinalLimit = new Vector2(backgroundPosition.x - _bgSize.x / 2f,
                backgroundPosition.y - _bgSize.y / 2f);
        }

        private void SetElasticLimits()
        {
            _maxElasticLimit = _maxFinalLimit - (Vector3)IsElastic(boarderOffset);
            _minElasticLimit = _minFinalLimit + (Vector3)IsElastic(boarderOffset);

            if (elastic) return;
            if (_bgSize.x / _bgSize.y > _camera.aspect)
            {
                _maxElasticLimit = _maxElasticLimit.ChangeX((_bgSize.x - _bgSize.y * _camera.aspect) / -2, true);
                _minElasticLimit = _minElasticLimit.ChangeX((_bgSize.x - _bgSize.y * _camera.aspect) / 2, true);
            }
            else
            {
                _maxElasticLimit = _maxElasticLimit.ChangeY((_bgSize.y - _bgSize.x / _camera.aspect) / -2, true);
                _minElasticLimit = _minElasticLimit.ChangeY((_bgSize.y - _bgSize.x / _camera.aspect) / 2, true);
            }
        }

        #endregion

        #region External Tool

        public bool Dragging { get; private set; }
        public bool LockDrag { get; set; }
        public bool MultiTouch { get; private set; }
        internal static void Deactivate()
        {
            if (!Ins._initialized) return;
            
            GZoomProfile.SetCameraPosition();
            Ins._initialized = false;
        }

        #endregion

        #region Private Arguments

        private Camera _camera;
        private bool _initialized;
        private float _fieldOfView;
        private bool _lockPosition;
        private bool _loadOnCenterOfImage;
        public int _distanceFromBackground;

        private Vector3 _minFinalLimit = Vector3.zero;
        private Vector3 _maxFinalLimit = Vector3.zero;
        private Vector3 _minElasticLimit = Vector3.zero;
        private Vector3 _maxElasticLimit = Vector3.zero;

        private Vector3 _lastTouchPos;
        private Vector3 _drift;
        private Vector3 _elasticVelocity;

        private Vector2 _bgSize;

        #endregion

        #region Main Functionality

        void LateUpdate()
        {
            if (!_initialized || _lockPosition || LockDrag || perspective) return;

            if (Input.GetMouseButtonUp(0))
            {
                Dragging = false;
                MultiTouch = false;
            }

            if (Input.GetMouseButtonDown(0))
            {
                _lastTouchPos = MousePosition;
                //_lastTouchPos.Print("", "lastTouchPos", "GZoom");
            }

            if (Input.touchCount == 2)
            {
                MultiTouch = true;
                var touchZero = Input.GetTouch(0);
                var touchOne = Input.GetTouch(1);

                var touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                var touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

                var prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                var currentMagnitude = (touchZero.position - touchOne.position).magnitude;

                var difference = currentMagnitude - prevMagnitude;

                Zoom(difference * 0.01f);
                var offset = touchOne.position + touchZero.position - new Vector2(Screen.width, Screen.height);
                offset = (_camera.orthographicSize > zoomOut - threshold || _camera.orthographicSize < zoomIn + threshold) 
                    ? Vector2.zero 
                    : (offset * difference * 0.002f); 
                _drift = -(touchZero.deltaPosition + touchOne.deltaPosition - offset) / 2 * 0.01f;
            }
            else if (Input.GetMouseButton(0) && !MultiTouch)
            {
                _drift = _lastTouchPos - MousePosition;
                MultiTouch = false;

                if (Dragging || _drift.magnitude > threshold)
                {
                    Dragging = true;
                    LerpDrift();
                }
            }

            Zoom(Input.GetAxis("Mouse ScrollWheel") * 5);
            if (Input.touchCount < 1)
                ClampZoom();

            _elasticVelocity = Vector3.zero;
            if (!Dragging)
            {
                _drift *= slideDamper;
                if (_drift.magnitude < 0.03f)
                    _drift = Vector3.zero;

                LerpDrift();
                SetAdditionalVelocity();
            }

            _drift = _drift.ChangeZ(0);
            MoveCamera();
            ClampCameraToFinalLimit();
            if (!elastic)
                ClampCameraToElasticLimit();

            if (Dragging)
                _lastTouchPos = MousePosition;
        }


        private void MoveCamera() => _camera.transform.position += _drift + IsElastic(_elasticVelocity);

        private void Zoom(float value) => _camera.orthographicSize = GetZoomValue(value);

        #endregion

        #region Clamping

        private void ClampCameraToFinalLimit()
        {
            var t = _camera.transform;

            if (t.position.x + CameraWidth > _maxFinalLimit.x)
                t.position = t.position.ChangeX(_maxFinalLimit.x - CameraWidth);

            if (t.position.x - CameraWidth < _minFinalLimit.x)
                t.position = t.position.ChangeX(_minFinalLimit.x + CameraWidth);

            if (t.position.y + CameraHeight > _maxFinalLimit.y)
                t.position = t.position.ChangeY(_maxFinalLimit.y - CameraHeight);

            if (t.position.y - CameraHeight < _minFinalLimit.y)
                t.position = t.position.ChangeY(_minFinalLimit.y + CameraHeight);
        }

        private void ClampCameraToElasticLimit()
        {
            var t = _camera.transform;
            if (XReachedElasticLimit())
                t.position = t.position.ChangeX(IsRight() ? MaxCameraLimit.x : MinCameraLimit.x);
            if (YReachedElasticLimit())
                t.position = t.position.ChangeY(IsUp() ? MaxCameraLimit.y : MinCameraLimit.y);
        }

        private void LerpDrift()
        {
            if (CameraLimitCrossPercent() > 0)
                _drift = Vector3.Slerp(_drift, Vector3.zero, CameraLimitCrossPercent());
        }

        private float GetZoomValue(float value)
        {
            var result = _camera.orthographicSize - value;
            if (!elastic)
                return Mathf.Clamp(result, zoomIn, zoomOut);
            if (result > zoomOut || result < zoomIn)
                result = _camera.orthographicSize - value / 5;
            result = Mathf.Clamp(result, zoomIn - zoomElasticity, zoomOut + zoomElasticity);
            return result;
        }
        
        private void ClampZoom()
        {
            if (_camera.orthographicSize > zoomOut)
                Zoom(0.4f);
            if (_camera.orthographicSize < zoomIn)
                Zoom(-0.4f);
        }

        private void SetAdditionalVelocity()
        {
            if (XReachedElasticLimit())
                _elasticVelocity = _elasticVelocity.ChangeX(0.1f * (IsRight() ? -1 : 1));
            if (YReachedElasticLimit())
                _elasticVelocity = _elasticVelocity.ChangeY(0.1f * (IsUp() ? -1 : 1));
        }

        #endregion

        #region Limits

        private bool XReachedElasticLimit(bool withThreshold = false) =>
            XReachedRightElasticLimit(withThreshold) || XReachedLeftElasticLimit(withThreshold);

        private bool XReachedRightElasticLimit(bool withThreshold = false) =>
            BoarderTopRight(withThreshold).x > _maxElasticLimit.x;

        private bool XReachedLeftElasticLimit(bool withThreshold = false) =>
            BoarderBottomLeft(withThreshold).x < _minElasticLimit.x;

        private bool YReachedElasticLimit(bool withThreshold = false) =>
            YReachedTopElasticLimit(withThreshold) || YReachedBottomElasticLimit(withThreshold);

        private bool YReachedTopElasticLimit(bool withThreshold = false) =>
            BoarderTopRight(withThreshold).y > _maxElasticLimit.y;

        private bool YReachedBottomElasticLimit(bool withThreshold = false) =>
            BoarderBottomLeft(withThreshold).y < _minElasticLimit.y;

        private float CameraLimitCrossPercent()
        {
            var result = Vector2.zero;
            var topRightDistance = BoarderTopRight() - _maxElasticLimit;
            var downLeftDistance = BoarderBottomLeft() - _minElasticLimit;

            if (YReachedBottomElasticLimit(true))
                result = result.ChangeY(downLeftDistance.y);
            if (YReachedTopElasticLimit(true))
                result = result.ChangeY(topRightDistance.y);
            if (XReachedLeftElasticLimit(true))
                result = result.ChangeX(downLeftDistance.x);
            if (XReachedRightElasticLimit(true))
                result = result.ChangeX(topRightDistance.x);
            return result.magnitude /
                   Math.Max((_maxFinalLimit - _maxElasticLimit).y, (_maxFinalLimit - _maxElasticLimit).x);
        }

        private Vector3 MaxCameraLimit => _maxElasticLimit - ScreenDiameter;
        private Vector3 MinCameraLimit => _minElasticLimit + ScreenDiameter;

        #endregion

        #region Utilities and Encapsulated Properties

        private float AngleInRadian(float angleInDegree) => Mathf.PI * angleInDegree / 180;
        private bool IsRight() => _camera.transform.position.x > background.transform.position.x;
        private bool IsUp() => _camera.transform.position.y > background.transform.position.y;
        private float CameraWidth => _camera.orthographicSize * _camera.aspect;
        private float CameraHeight => _camera.orthographicSize;
        private Vector3 MousePosition => _camera.ScreenToWorldPoint(Input.mousePosition);

        private Vector3 BoarderTopRight(bool withThreshold = false) => _camera.transform.position + ScreenDiameter -
                                                                       (withThreshold
                                                                           ? (Vector3.one * (5 * threshold))
                                                                           : Vector3.zero);

        private Vector3 BoarderBottomLeft(bool withThreshold = false) => _camera.transform.position - ScreenDiameter +
                                                                         (withThreshold
                                                                             ? (Vector3.one * (5 * threshold))
                                                                             : Vector3.zero);

        private Vector3 ScreenDiameter => new Vector3(CameraWidth, CameraHeight);
        private float IsElastic(float value) => elastic ? value : 0;
        private Vector3 IsElastic(Vector3 value) => elastic ? value : Vector3.zero;
        private Vector2 IsElastic(Vector2 value) => elastic ? value : Vector2.zero;

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (!_initialized) return;
            var bgPos = background.transform.position;
            var camPos = _camera.transform.position;

            Gizmos.color = Color.grey;
            Gizmos.DrawWireCube(camPos, new Vector3(zoomOut * _camera.aspect * 2, zoomOut * 2));

            Gizmos.color = Color.grey;
            Gizmos.DrawWireCube(camPos, new Vector3(zoomIn * _camera.aspect * 2, zoomIn * 2));

            Gizmos.color = Color.yellow; //boarder limit
            Gizmos.DrawWireCube(bgPos, _maxElasticLimit - _minElasticLimit);

            Gizmos.color = Color.green; //boarder limit with threshold
            Gizmos.DrawWireCube(bgPos, _maxElasticLimit - _minElasticLimit - (Vector3.one * (5 * threshold)));

            Gizmos.color = Color.cyan; //Camera position limit rect 
            Gizmos.DrawWireCube(bgPos, _maxElasticLimit - _minElasticLimit - 2 * ScreenDiameter);

            Gizmos.color = Color.red; //Final Boarder
            Gizmos.DrawWireCube(bgPos, _maxFinalLimit - _minFinalLimit);
        }
#endif

        private enum O //Orientation
        {
            R, //Right
            L, //Left
            D, //Down
            U //Up
        }

        #endregion


        #region Public functions

        public void ZoomOut(Action callBack)
        {
            var currentSize = _camera.orthographicSize;
            DOTween.To((value) => _camera.orthographicSize = value, currentSize, zoomOut, 0.2f).OnComplete(()=>callBack());
        }

        #endregion
    }
}