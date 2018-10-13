using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    public enum EnemyType
    {
        Bear,
        Bunny
    }

    public enum EnemyState
    {
        Healthy,
        Hit,
        Dead
    }
    public int health;
    public EnemyState State;
    public EnemyType Type;
    public Transform Target;
    // Use this for initialization
	void Start () {

	}
	
    void SetType(EnemyType type)
    {
        Type = type;
    }

    // Update is called once per frame
    void Update () {
        switch(State){
            case EnemyState.Healthy:
                //Do Normal Things
                break;
            case EnemyState.Hit:
                //Run DeathCourotine
                //Rigidbody force rand x z 
                // Y +

                State = EnemyState.Dead;
                break;
            case EnemyState.Dead:
                Destroy(this, .03f);
                break;
        }
	}

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "God Item")
        {
            State = EnemyState.Hit;
        }
    }



}
