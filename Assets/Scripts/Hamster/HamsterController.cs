using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HamsterController : BaseControllable {

    public int hp = 1;
    public float walkSpeed = 50.0f;
    public float runSpeed = 100.0f;
    public float runTime = 5.0f;
    public float runCooldownTime = 10.0f;
    public float stunSpeed = 0.0f;
    public float initialVisibilityTime = 5.0f;

    public bool isVisible = true;

    private float runCooldownTimer = 0.0f;
    private float visibilityTimer = 0.0f;

    private Rigidbody rBody;
    private MeshRenderer meshRenderer;

    #region animVars
    public static string animHealth = "playerHealth";
    public static string animMoveSpeed = "moveSpeed";
    public static string animStunTrigger = "stun";
    public static string animStunTimer = "stunTimer";
    public static string animRunTrigger = "run";
    #endregion

    // Use this for initialization
    public override void Start () {
        base.Start();

        rBody = GetComponent<Rigidbody>();
        meshRenderer = GetComponent<MeshRenderer>();

        // init animator vals
        this.animator.SetInteger(animHealth, hp);

        runCooldownTimer = runCooldownTime;
        SetVisibility(true);
    }

    public void Update()
    {
        if (visibilityTimer < initialVisibilityTime)
            visibilityTimer += Time.deltaTime;

        if (visibilityTimer >= initialVisibilityTime)
            SetVisibility(false);

        if (runCooldownTimer < runCooldownTime)
            runCooldownTimer += Time.deltaTime;
    }

    public void DoMovement(float moveSpeed)
    {
        float xInput = Input.GetAxis(InputHandles.HorizontalAxis);
        float zInput = Input.GetAxis(InputHandles.VerticalAxis);
        Vector3 movementPerSecond = new Vector3(xInput * moveSpeed, 0.0f, zInput * moveSpeed);
        rBody.MovePosition(transform.position + movementPerSecond * Time.deltaTime);

        this.animator.SetFloat(animMoveSpeed, movementPerSecond.magnitude);
    }

    public void SetVisibility(bool visibility)
    {
        isVisible = visibility;
        meshRenderer.enabled = isVisible;   
    }

    public void CheckRun()
    {
        if(Input.GetButtonDown(InputHandles.Action))
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
}
