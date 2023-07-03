using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerJoinedDisplayIcon : MonoBehaviour
{
    public int PlayerNum { get; private set; }

    private const int POSITION_OFFSET = 75;

    [SerializeField] private Image banner;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Color[] playerIconColors;

    public void SetDisplayIcon(int playerNum)
    {
        PlayerNum = playerNum + 1;
        transform.position -= new Vector3(0, POSITION_OFFSET * playerNum);

        banner.color = playerIconColors[playerNum];
        text.text = "Player " + PlayerNum;
    }
}
