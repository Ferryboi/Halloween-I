using UnityEngine;

public abstract class RoundCard : MonoBehaviour, IInteractable
{
    public string Description => _description;
    [SerializeField] protected string _description;

    public int Cost => _cost;
    [SerializeField] protected int _cost;

    protected static int _numOfPurchases;

    protected virtual void Awake()
    {
        if (LevelManager.Instance.RoundNum <= 1) _numOfPurchases = 0;
        _cost += _numOfPurchases;
    }

    public Interaction GetInteraction()
    {
        return null;
    }

    public void OnInteract()
    {
        if (!CanPurchase()) return;
        ScoreManager.Instance.AddScore(-_cost);

        _numOfPurchases++;
        ActivateCard();
    }

    protected virtual bool CanPurchase()
    {
        if (ScoreManager.Instance.Score >= _cost) return true;
        else return false;
    }

    protected abstract void ActivateCard();
    public virtual void RemoveCard() { Destroy(gameObject); }
}
