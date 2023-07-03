using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavBar : MonoBehaviour
{
    [SerializeField] private GameObject startingSection;
    [SerializeField] private GameObject[] sections;

    private void OnEnable()
    {
        SwitchTo(startingSection);
    }

    public void SwitchTo(GameObject section)
    {
        for(int i = 0; i < sections.Length; i++)
        {
            if(sections[i] != section)
            {
                sections[i].SetActive(false);
            }
        }

        section.SetActive(true);
    }
}
