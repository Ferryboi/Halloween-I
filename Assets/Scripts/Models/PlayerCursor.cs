using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCursor : MonoBehaviour
{
    [SerializeField] private Image img;
    public int PlayerNum { get; private set; }

    public void SetPlayerNum(int playerNum)
    {
        PlayerNum = playerNum;
    }

    public void SetColor(Color color)
    {
        img.color = color;
    }
}
