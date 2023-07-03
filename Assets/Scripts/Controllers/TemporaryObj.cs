using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryObj : MonoBehaviour
{
    [SerializeField] private float delay;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DelayRemove());
    }

    private IEnumerator DelayRemove()
    {
        yield return new WaitForSeconds(delay);

        Entity entity = GetComponentInParent<Entity>();
        if (entity != null) entity.KillEntity();
        else Destroy(gameObject);
    }
}
