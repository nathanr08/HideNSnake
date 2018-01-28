using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharSelect : MonoBehaviour {

    public GameObject readyToStartPanel;
    public List<PlayerSelect> playerSelects;

    private BaseControllable baseControllable;

    private void Start()
    {
        baseControllable = GetComponent<BaseControllable>();
    }

    private void Update()
    {
        if (CheckReadyToStart() == true)
        {
            readyToStartPanel.SetActive(true);
            if (Input.GetButton(baseControllable.InputHandles.Menu))
            {
                // start game logic
                List<string> playerData = new List<string>();
                foreach(PlayerSelect player in playerSelects)
                {
                    playerData.Add(player.GetRole());
                }
                new ChangeMenuEvent("");
                new InitGameEvent(playerData);
            }
        }
        else
        {
            readyToStartPanel.SetActive(false);
        }
    }

    private bool CheckReadyToStart()
    {
        bool ready = true;
        int playersJoined = 0;
        foreach(PlayerSelect playerSelect in playerSelects)
        {
            if (playerSelect.IsReadyToStart() == false)
            {
                ready = false;
            }
            if (playerSelect.HasJoinedGame() == true)
            {
                ++playersJoined;
            }
        }

        if (playersJoined <= 1)
            ready = false;

        return ready;
    }
}
