using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shakeable : MonoBehaviour {
    
    private Dictionary<string, Vector3> movers = new Dictionary<string, Vector3>();
    private AudioSource audioSource;
    private float jiggle = 0.0f;
    [SerializeField]
    private float jiggle_speed = 5.0f;
    [SerializeField]
    private float jiggle_factor = 5.0f;
    private Vector3 start_scale;


	public bool AddTriggerMeshCollider = false;
	private MeshCollider MCol;


    public virtual void Start()
    {

		
			MCol = gameObject.AddComponent<MeshCollider>();

			MCol.convex = true;
			MCol.isTrigger = true;

		if(AddTriggerMeshCollider)
		{
			MCol.inflateMesh = true;

			MCol.skinWidth = 1.0f;

			MCol = gameObject.AddComponent<MeshCollider>();
			MCol.convex = true;

		}
			

		audioSource = GetComponent<AudioSource>();

		if(audioSource == null)
		{
			audioSource = gameObject.AddComponent<AudioSource>();
			audioSource.clip = (AudioClip)Resources.Load("Wobble",typeof(AudioClip));
		}

        start_scale = transform.localScale;
    }

    public void OnTriggerStay(Collider other)
    {
		if(other.gameObject.layer == LayerMask.NameToLayer("Shakeable"))
			return;

        // if its not in the list make a list entry for it
        //print( other.name );
        if( !movers.ContainsKey( other.name ) )
            movers[other.name] = other.transform.position;

        // check to see if its changed.
        if( movers[other.name] != other.transform.position )
        {
            movers[other.name] = other.transform.position;
            print( "Shakeable::OnTriggerEnter");
            EnvObjectSound();
            EnvObjectAnimate();
        }
    }
    
    protected virtual void EnvObjectSound()
    {
        print( "TestShaker::Audio" + audioSource );
        if (audioSource && !audioSource.isPlaying)
            audioSource.PlayOneShot(audioSource.clip);
    }
    protected virtual void EnvObjectAnimate()
    {
        print( "TestShaker::Anim" );
        jiggle += jiggle_speed * Time.deltaTime;
        if( jiggle > 2.0f * Mathf.PI ) jiggle = 0.0f;
        float jiggle_modulation = 1.0f + (Mathf.Sin(jiggle) / jiggle_factor);
        transform.localScale = new Vector3( start_scale.x * jiggle_modulation,
                                            start_scale.y,
                                            start_scale.z * jiggle_modulation );
        //transform.Rotate( 0.0f, 180.0f * Time.deltaTime, 0.0f );
    }
}
