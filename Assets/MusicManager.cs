using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

    [SerializeField]
    public AudioSource MenuMusic;

    [SerializeField]
    public AudioSource GameMusic;

    [SerializeField]
    public AudioSource Fanfare;

    private static MusicManager m_Instance;
    public static MusicManager GetInstance() { return m_Instance; }

    void Awake()
    {
        m_Instance = this;
    }

    void OnDestroy()
    {
        m_Instance = null;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
