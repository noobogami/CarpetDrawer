using System.Linq;
using UnityEngine;

namespace Garaj.Utility
{
    public static class TransformEx
    {
        /// <summary>
        /// Destroy all of children in hierarchy immediately
        /// </summary>
        /// <param name="target"></param>
        public static void DestroyAllChildren(this Transform target)
        {
            while (target.childCount > 0)
                Object.DestroyImmediate(target.GetChild(0).gameObject);
        }

        public static void DestroyAllChildren(this GameObject target) => target.transform.DestroyAllChildren();

        public static Vector3 ChangeZ(this Vector3 target, float newValue, bool add = false) => new Vector3(target.x, target.y, newValue + (add? target.z : 0));
        public static Vector3 ChangeY(this Vector3 target, float newValue, bool add = false) => new Vector3(target.x, newValue + (add? target.y : 0), target.z);
        public static Vector3 ChangeX(this Vector3 target, float newValue, bool add = false) => new Vector3(newValue + (add? target.x : 0), target.y, target.z);
        public static Vector2 ChangeY(this Vector2 target, float newValue, bool add = false) => new Vector2(target.x, newValue + (add? target.y : 0));
        public static Vector2 ChangeX(this Vector2 target, float newValue, bool add = false) => new Vector2(newValue + (add? target.x : 0), target.y);
        public static Vector2Int ChangeY(this Vector2Int target, int newValue, bool add = false) => new Vector2Int(target.x, newValue + (add? target.y : 0));
        public static Vector2Int ChangeX(this Vector2Int target, int newValue, bool add = false) => new Vector2Int(newValue + (add? target.x : 0), target.y);
        public static Vector3 MultiplyX(this Vector3 target, float multiplier) => new Vector3(target.x * multiplier, target.y, target.z);
        public static Vector3 MultiplyY(this Vector3 target, float multiplier) => new Vector3(target.x, target.y * multiplier, target.z);
        public static Vector3 MultiplyZ(this Vector3 target, float multiplier) => new Vector3(target.x, target.y, target.x * multiplier);
        public static Vector2 MultiplyX(this Vector2 target, float multiplier) => new Vector2(target.x * multiplier, target.y);
        public static Vector2 MultiplyY(this Vector2 target, float multiplier) => new Vector2(target.x, target.y * multiplier);

/// <summary>
/// Reset Transform/Rotation/Scale
/// </summary>
/// <param name="target"></param>
        public static void ResetPSR(this Transform target)
        {
            target.localPosition = Vector3.zero;
            target.localRotation = Quaternion.Euler(Vector3.zero);
            target.localScale = Vector3.one;
        }

    public static T GetComponentInChildrenIgnoreParent<T>(this Transform target) where T : MonoBehaviour
    {
        var t = target.GetComponentsInChildren<T>();
        return t.FirstOrDefault(obj => obj.transform != target);
    }

    }
}
