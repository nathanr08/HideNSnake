using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerIcon : MonoBehaviour {

    public int playerNumber;

    public GameObject iconPanel;
    public Image deadIcon;
    public Image playerIcon;
    public Image background;

    public Sprite hamsterSprite;
    public Sprite snakeSprite;

	// Use this for initialization
	void Start () {
        background.color = GameManager.Instance.GetPlayerColor(playerNumber);
	}
	
	public void SetDead(bool isDead)
    {
        deadIcon.gameObject.SetActive(isDead);
    }

    public void SetPlayerRole(string playerRole)
    {
        if(playerRole == GameManager.snakePlayer)
        {
            playerIcon.sprite = snakeSprite;
            iconPanel.SetActive(true);
        }
        else if(playerRole == GameManager.hamsterPlayer)
        {
            playerIcon.sprite = hamsterSprite;
            iconPanel.SetActive(true);
        }
        else
        {
            iconPanel.SetActive(false);
        }
    }
}
