using UnityEngine;

public class SceneCanvas : MonoBehaviour
{
    private static SceneCanvas _ins;
    public static SceneCanvas Ins => _ins;

    private void Awake()
    {
        if (_ins == null)
        {
            _ins = this;
        }
        else if (!Equals(_ins, this))
        {
            Destroy(gameObject);
        }
    }
}