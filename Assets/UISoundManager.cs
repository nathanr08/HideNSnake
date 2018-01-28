using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISoundManager : MonoBehaviour {


    [SerializeField]
    public AudioSource NavigateSound;

    [SerializeField]
    public AudioSource ConfirmSound;

    [SerializeField]
    public AudioSource CharSelectSound;

    [SerializeField]
    public AudioSource HamsterDeath;

    private static UISoundManager m_Instance;
    public static UISoundManager GetInstance() { return m_Instance; }

    void Awake()
    {
        m_Instance = this;
    }

    void OnDestroy()
    {
        m_Instance = null;
    }

}
