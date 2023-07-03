using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TombstoneManager : Singleton<TombstoneManager>
{
    [SerializeField] private GameObject tombstonePrefab;
    [SerializeField] private List<TombstonePlacement> placements;
    private List<Tombstone> tombstones;

    private void Awake()
    {
        tombstones = new List<Tombstone>();
        RespawnTombstones();
    }

    public void RespawnTombstones()
    {
        for(int i = tombstones.Count - 1; i >= 0; i--)
        {
            Destroy(tombstones[i].gameObject);
            tombstones.RemoveAt(i);
        }

        for(int i = 0; i < placements.Count; i++)
        {
            Vector3 topPos = placements[i].GetPosInRange();
            Tombstone newTombstone = Instantiate(tombstonePrefab, topPos, Quaternion.identity).GetComponent<Tombstone>();
            tombstones.Add(newTombstone);
        }
    }

    public int FindActiveTombstoneCount()
    {
        int count = 0;
        for(int i = 0; i < tombstones.Count; i++)
        {
            if (!tombstones[i].Destroyed) count++;
        }
        return count;
    }

    public List<Tombstone> FindAllActiveTombstones()
    {
        List<Tombstone> activeTombstones = new List<Tombstone>();

        for(int i = 0; i < tombstones.Count; i++)
        {
            if (!tombstones[i].Destroyed) activeTombstones.Add(tombstones[i]);
        }

        return activeTombstones;
    }

    public Tombstone FindActiveTombstone()
    {
        List<Tombstone> activeTombstones = tombstones.GetRange(0, tombstones.Count);

        while (activeTombstones.Count > 0)
        {
            int index = UnityEngine.Random.Range(0, activeTombstones.Count);
            if (!activeTombstones[index].Destroyed) return activeTombstones[index];
            else activeTombstones.RemoveAt(index);
        }

        return null;
    }

    public Tombstone FindDestroyedTombstone()
    {
        List<Tombstone> destroyedTombstones = tombstones.GetRange(0, tombstones.Count);

        while (destroyedTombstones.Count > 0)
        {
            int index = UnityEngine.Random.Range(0, destroyedTombstones.Count);
            if (destroyedTombstones[index].Destroyed) return destroyedTombstones[index];
            else destroyedTombstones.RemoveAt(index);
        }

        return null;
    }

    public Tombstone FindZombielessTombstone()
    {
        List<Tombstone> zombielessTombstones = tombstones.GetRange(0, tombstones.Count);

        while (zombielessTombstones.Count > 0)
        {
            int index = UnityEngine.Random.Range(0, zombielessTombstones.Count);

            if (zombielessTombstones[index].AssociatedZombie == null) return zombielessTombstones[index];
            else zombielessTombstones.RemoveAt(index);
        }

        return null;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        for(int i = 0; i < placements.Count; i++)
        {
            TombstonePlacement p = placements[i];
            float y = p.CenterPos.y;
            Gizmos.DrawLine(p.CenterPos + new Vector3(-p.Width, y, -p.Length), p.CenterPos + new Vector3(-p.Width, y, p.Length));
            Gizmos.DrawLine(p.CenterPos + new Vector3(-p.Width, y, p.Length), p.CenterPos + new Vector3(p.Width, y, p.Length));
            Gizmos.DrawLine(p.CenterPos + new Vector3(p.Width, y, p.Length), p.CenterPos + new Vector3(p.Width, y, -p.Length));
            Gizmos.DrawLine(p.CenterPos + new Vector3(p.Width, y, -p.Length), p.CenterPos + new Vector3(-p.Width, y, -p.Length));
        }
    }
}

[Serializable]
public class TombstonePlacement
{
    public Vector3 CenterPos => centerPos;
    [SerializeField] private Vector3 centerPos;

    public float Width => width;
    [SerializeField] private float width;
    public float Length => length;
    [SerializeField] private float length;

    public Vector3 GetPosInRange()
    {
        float xOffset = UnityEngine.Random.Range(-width, width);
        float zOffset = UnityEngine.Random.Range(-length, length);

        return new Vector3(centerPos.x + xOffset, centerPos.y, centerPos.z + zOffset);
    }
}
