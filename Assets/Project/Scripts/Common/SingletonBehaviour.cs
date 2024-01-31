using UnityEngine;

public abstract class SingletonBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    protected static T m_Instance = null;
    protected bool m_Enabled = false;
    
    public static T Instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = FindObjectOfType<T>();

                if (m_Instance == null)
                {
                    GameObject obj = new GameObject();
                    m_Instance = obj.AddComponent<T>();
                    obj.name = string.Format(typeof(T).Name);
                }
            }

            return m_Instance;
        }
    }

    protected virtual void Awake()
    {
        if (!Application.isPlaying)
        {
            return;
        }

        if (m_Instance == null)
        {
            m_Instance = this as T;
            m_Enabled = true;
        }
        else
        {
            if (this != m_Instance)
            {
                Destroy(this.gameObject);
            }
        }
    }
}