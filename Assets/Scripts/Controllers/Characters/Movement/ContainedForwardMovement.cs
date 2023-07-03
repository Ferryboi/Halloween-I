using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainedForwardMovement : BasicMovement
{
    private LevelManager lManager;

    public static float StaticScale = 1f;

    private const float ROUND_SCALE = 0.25f;

    private void Awake()
    {
        lManager = LevelManager.Instance;

        SetDirection(transform.forward);
        SetSpeed(GetSpeed() + LevelManager.Instance.RoundNum * ROUND_SCALE * StaticScale);
    }

    private void Update()
    {
        Move();

        //If the entity has moved past the right border of the map and will continue to do so
        if(transform.position.x > lManager.LevelHalfWidth && GetDirection().x > 0)
        {
            SetDirection(new Vector3(-GetDirection().x, GetDirection().y, GetDirection().z));
        }
        //If the entity has moved past the left border of the map and will continue to do so
        else if (transform.position.x < -lManager.LevelHalfWidth && GetDirection().x < 0)
        {
            SetDirection(new Vector3(-GetDirection().x, GetDirection().y, GetDirection().z));
        }

        //If the entity has moved past the top border of the map and will continue to do so
        if (transform.position.z > lManager.LevelHalfHeight && GetDirection().z > 0)
        {
            SetDirection(new Vector3(GetDirection().x, GetDirection().y, -GetDirection().z));
        }
        //If the entity has moved past the bottom border of the map and will continue to do so
        else if (transform.position.z < -lManager.LevelHalfHeight && GetDirection().z < 0)
        {
            SetDirection(new Vector3(GetDirection().x, GetDirection().y, -GetDirection().z));
        }
    }
}

