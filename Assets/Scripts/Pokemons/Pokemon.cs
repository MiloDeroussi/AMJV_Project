using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pokemon : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    abstract public void Attack();

    abstract public void Capacity();

    abstract public float getAttackCd();

    abstract public float getCooldown();
}
