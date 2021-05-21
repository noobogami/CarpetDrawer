using System;
using System.Linq;
using Garaj.GLog;
using UnityEngine;

namespace Garaj.Zoom
{
    public class GZoomProfile
    {
        private static bool _initialized;
        private static ZoomProfileStructure _activeProfile;
        private static readonly ZoomProfileStructure[] Profile =
        {
            new ZoomProfileStructure{
                key = GZoomProfileKey.Main,
                perspective = false,
                loadOnCenterOfImage = true,
                elastic = true,
                boarderOffset = Vector2.zero,
                zoomIn = 1.5f,
                zoomOut = -1,
                zoomElasticity = 0,
                threshold = 0.3f,
                slideDamper = 0.9f,
                distanceFromBackground = 80}
        };

        public static void SetActiveProfile(GZoomProfileKey profile)
        {
            _activeProfile = Profile.First(p => p.key == profile);
            _initialized = true;
            SetCameraPosition();
        }
        
        internal static void SetCameraPosition()
        {
            if (!_initialized || _activeProfile.perspective || Camera.main is null) return;

            if (_activeProfile.loadPreviousPosition && _activeProfile.PreviousPositionSaved)
                _activeProfile.CameraPosition = Camera.main.transform.position;
            else
                _activeProfile.CameraPosition = _activeProfile.defaultPosition;

            _activeProfile.PreviousPositionSaved = true;
        }

        internal static ZoomProfileStructure GetActiveProfile() => _activeProfile ?? (_activeProfile = Profile[0]);

        internal static ZoomProfileStructure GetProfile(GZoomProfileKey profile) =>
            Profile.First(p => p.key == profile);
    }
    [Serializable]
    internal class ZoomProfileStructure
    {
        public GZoomProfileKey key;
        public bool perspective;
        public Vector3 defaultPosition;
        public bool loadPreviousPosition;
        public bool lockPosition;
        public bool loadOnCenterOfImage = true;
        public float fieldOfView;
        public bool elastic;
        public Vector2 boarderOffset;
        public float zoomIn;
        public float zoomOut;
        public float zoomElasticity;
        public float threshold;
        public float slideDamper;
        public int distanceFromBackground;

        internal bool PreviousPositionSaved;
        internal Vector3 CameraPosition;
    }
}