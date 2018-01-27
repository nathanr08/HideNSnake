using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandleNames
{
    #region PublicMembers
    public int PlayerNumber;
    public string HorizontalAxis;
    public string VerticalAxis;
    public string Action;
    public string Submit;
    public string Cancel;
    public string Menu;
    #endregion

    #region PublicMethods
    public InputHandleNames(int playernum)
    {
        SetPlayerInputNumber(playernum);
    }

    public void SetPlayerInputNumber(int playernum)
    {
        PlayerNumber = playernum;
        HorizontalAxis = playernum + "JoyHorizontal";
        VerticalAxis = playernum + "JoyVertical";
        Action = playernum + "Action";
        Submit = playernum + "Submit";
        Cancel = playernum + "Cancel";
        Menu = playernum + "Menu";
    }
    #endregion
}

public class BaseControllable : MonoBehaviour
{

    #region ProtectedMembers
    [SerializeField]
    protected int PlayerNumber;
    #endregion

    public InputHandleNames InputHandles;

    #region PublicMethods
    // Use this for initialization
    public virtual void Start()
    {
        InputHandles = new InputHandleNames(PlayerNumber);
    }

    // Set the string names for input
    protected void SetPlayerInputNumber(int playerNum)
    {
        this.InputHandles.SetPlayerInputNumber(playerNum);
    }
    #endregion
}
