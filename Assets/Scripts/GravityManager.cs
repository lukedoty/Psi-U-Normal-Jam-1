using System.Collections.Generic;
using UnityEngine;

public class GravityManager : MonoBehaviour
{
    private static GravityManager s_instance;
    public static GravityManager Instance { get { return s_instance; } }
    private List<IGravityEmitter> m_gravityEmitters;
    public List<IGravityEmitter> GravityEmitters { get { return m_gravityEmitters; } }

    private void Awake()
    {
        if (s_instance != null && s_instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            s_instance = this;
        }

        m_gravityEmitters = new List<IGravityEmitter>();
    }
}
