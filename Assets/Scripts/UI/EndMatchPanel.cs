using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndMatchPanel : MonoBehaviour {

    BaseControllable controllable;
    public Button backToMenuButton;

	// Use this for initialization
	void Start () {
        controllable = GetComponent<BaseControllable>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButton(controllable.InputHandles.Submit))
        {
            backToMenuButton.onClick.Invoke();
        }
	}
}
