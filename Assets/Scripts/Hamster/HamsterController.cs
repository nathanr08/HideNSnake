using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HamsterController : StateController
{

    public float walkSpeed = 50.0f;
    public float runSpeed = 100.0f;
    public float runTime = 5.0f;
    public float runCooldownTime = 10.0f;
    public float stunSpeed = 0.0f;
    public float initialVisibilityTime = 5.0f;

    public bool isVisible = true;

    public float appearDurration = 0.2f;
    public float disappearDurration = 1.0f;
    [SerializeField]
    private float fadeTimer = -1.0f;
    private List<Material> startColors = new List<Material>();


    private int health = 1;
    private float runCooldownTimer = 0.0f;
    [SerializeField]
    private float visibilityTimer = 0.0f;

    private float currVisibilityTime = 0.0f;

    private Rigidbody rBody;
    private MeshRenderer meshRenderer;
    private BaseControllable baseControllable;

    [SerializeField]
    public AudioSource WalkAudio;
    public AudioSource RunAudio;
    public AudioSource RunAudioEnd;
    public AudioSource WalkFastAudio;
    public AudioSource FreezeAudio;

    #region animVars
    public static string animHealth = "playerHealth";
    public static string animMoveSpeed = "moveSpeed";
    public static string animStunTrigger = "stun";
    public static string animStunTimer = "stunTimer";
    public static string animRunTrigger = "run";
    #endregion

    // Use this for initialization
    public override void Start()
    {
        base.Start();

        rBody = GetComponent<Rigidbody>();
        meshRenderer = GetComponent<MeshRenderer>();
        baseControllable = GetComponent<BaseControllable>();
        MeshRenderer[] renderers = GetComponentsInChildren<MeshRenderer>();
        //foreach (MeshRenderer renderer in renderers)
        foreach( Renderer renderer in GetComponentsInChildren<Renderer>() )
        {
            //renderer.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            //renderer.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            //renderer.material.renderQueue = 3000;
            startColors.Add( renderer.material );
        }
        print( startColors.Count );

        // init animator vals
        this.animator.SetInteger(animHealth, health);

        runCooldownTimer = runCooldownTime;
        SetVisibility(true);
        currVisibilityTime = initialVisibilityTime;
    }

    public void Update()
    {
        ///////////////////////////////////////////////
        // Visability                                //
        ///////////////////////////////////////////////

        // Hack, if the game has not started, bail
        if (GameManager.Instance == null || !GameManager.Instance.gameInProgress)
        {
            return;
        }

        if (visibilityTimer < currVisibilityTime)
        {
            visibilityTimer += Time.deltaTime;

            if (visibilityTimer >= currVisibilityTime &&
                currState.GetType() != typeof(HamsterRunBehavior))
                SetVisibility(false);
        }

        if (0.0f < fadeTimer)
        {
            //print( "Fade Timer: " + fadeTimer );
            fadeTimer -= Time.deltaTime;
            float alpha = fadeTimer;
            if (isVisible)
            {
                alpha /= appearDurration;
                alpha = 1.0f - alpha;
                //print( "alpha: " + alpha + "fade: " + fadeTimer );
            }
            else
            {
                alpha /= disappearDurration;
            }
            alpha = Mathf.Max( 0.0f, alpha );
            alpha = Mathf.Min( 1.0f, alpha );
            foreach (Material m in startColors)
            {
                if (0.0f < alpha)
                {
                    Color c = m.color;
                    m.color = new Color(c.r, c.g, c.b, alpha);
                }
                else
                {
                    GetComponent<Renderer>().enabled = false;
                }
            }
        }
        ///////////////////////////////////////////////
        // End Visability                            //
        ///////////////////////////////////////////////

        if (runCooldownTimer < runCooldownTime)
            runCooldownTimer += Time.deltaTime;
    }

    public void DoMovement(float moveSpeed)
    {
        float xInput = Input.GetAxis(baseControllable.InputHandles.HorizontalAxis);
        float zInput = Input.GetAxis(baseControllable.InputHandles.VerticalAxis);
        Vector3 movementPerSecond = new Vector3(xInput * moveSpeed, 0.0f, zInput * moveSpeed);
        rBody.MovePosition(transform.position + movementPerSecond * Time.deltaTime);

        this.animator.SetFloat(animMoveSpeed, movementPerSecond.magnitude);

        Vector3 lookDir = movementPerSecond.normalized;
        if (lookDir.magnitude != 0.0f)
            rBody.MoveRotation(Quaternion.LookRotation(lookDir));
    }

    public void SetVisibility(bool visibility, float durration = 0.0f)
    {
        print( "set visability " + visibility + " duration " + durration );
        isVisible = visibility;
        if( isVisible )
        {
            visibilityTimer = 0.0f;
            currVisibilityTime = durration;
            fadeTimer = appearDurration;
            GetComponent<Renderer>().enabled = true;
        }
        else
        {
            fadeTimer = disappearDurration;
        }
        //MeshRenderer[] renderers = GetComponentsInChildren<MeshRenderer>();
        //foreach (MeshRenderer renderer in renderers)
        //{
        //    renderer.enabled = visibility;
        //}
    }

    public void CheckRun()
    {
        if (Input.GetButtonDown(baseControllable.InputHandles.Action))
        {
            if (CanRun())
            {
                animator.SetTrigger(animRunTrigger);
            }
            else
            {
                // we tried to run, but CD wouldnt let us. trigger some effect
            }
        }
    }

    public bool CanRun()
    {
        bool canRun = false;
        if (runCooldownTimer >= runCooldownTime)
            canRun = true;

        return canRun;
    }

    public void StartRunCooldown()
    {
        runCooldownTimer = 0.0f;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        animator.SetInteger(animHealth, health);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("SnakeHead"))
        {
            TakeDamage(1);
        }
    }

    public void Freeze( )
    {
        animator.SetTrigger(animStunTrigger);
    }
}
