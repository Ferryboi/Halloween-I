using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealable
{
    public void OnHealed(int health = -1);
}
