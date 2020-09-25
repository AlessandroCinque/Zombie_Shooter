using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HitPoints : MonoBehaviour
{
    [SerializeField]private int HealthPoints = 10;
    private int MaxHealth;
    // the events are like notifications and do not need to be in an update function. Saves a lot of polling
    public event Action OnHealthDepleted;
    public event Action OnWounded;
    private void Start()
    {
        MaxHealth = HealthPoints;
    }
    public void AdjustHealth(int adjust)
    {
        int temp_Health = HealthPoints;
        HealthPoints += adjust;

        if (HealthPoints <= 0)
        {
            OnHealthDepleted?.Invoke();
        }
        else if (HealthPoints < temp_Health)
        {
            OnWounded?.Invoke();
        }
    }
    public int GetMaxHP()
    {
        return MaxHealth;
    }
    public int GetCurrHp()
    {
        return HealthPoints;
    }
}
