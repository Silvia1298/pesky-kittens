using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    protected PlayerController player;

    protected virtual void Awake()
    {
        player = GetComponent<PlayerController>();
    }

    public abstract void Activate();
}