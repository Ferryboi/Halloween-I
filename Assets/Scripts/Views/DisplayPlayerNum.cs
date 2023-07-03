using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayPlayerNum : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private TextMeshProUGUI text;

    private void Start()
    {
        text.text = "Player " + (player.GetPlayerNum() + 1);
    }

    private void Update()
    {
        if(LevelManager.Instance.LevelActive)
        {
            Destroy(gameObject);
        }
    }
}
