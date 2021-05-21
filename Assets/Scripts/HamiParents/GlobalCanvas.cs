using UnityEngine;

public class GlobalCanvas : MonoBehaviour
{
    private static GlobalCanvas _ins;

    public static GlobalCanvas Ins
    {
        get
        {
            if (_ins == null)
            {
                _ins = Instantiate(Resources.Load<GameObject>("Prefabs/GlobalCanvas")).GetComponent<GlobalCanvas>();
            }

            return _ins;
        }
    }

    protected virtual void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}