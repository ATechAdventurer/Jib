using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonController : MonoBehaviour {
    const int MAX_HEALTH = 3;
    const int MIN_HUNGER = 3;
    const int MIN_THIRST = 3;
    public enum PersonState
    {
        Healthy,
        Dead
    }
    public enum NeedType
    {
        Satisfied,
        Food,
        Water
    }

    public PersonState State;
    public float Hunger;
    public float Thirst;
    public float Health;

	// Use this for initialization
	void Start () {
        Hunger = 0;
        Thirst = 0;
        Health = MAX_HEALTH;
	}
	
	// Update is called once per frame
	void Update () {
        if (Health == 0 || Hunger == MIN_HUNGER || Thirst == MIN_THIRST || State == PersonState.Dead)
        {
            Destroy(this, .01f);
        }
        
	}

    void TakeDamage()
    {
        Health -= 1;
    }

    void Heal()
    {
        Health = Health >= MAX_HEALTH - 1 ? MAX_HEALTH : Health + 1;
    }

    void FullHeal()
    {
        Health = MAX_HEALTH;
    }

    void TakeHunger()
    {
        Hunger++;
    }

    void Kill()
    {
        State = PersonState.Dead;
    }

    NeedType GetPriority()
    {
        return (Hunger > Thirst) ? NeedType.Food : (Thirst > Hunger) ? NeedType.Water : (Hunger == 0 && Thirst == 0) ? NeedType.Satisfied : (Hunger == Thirst && Thirst != 0) ? NeedType.Food : NeedType.Water; 
    }

}
