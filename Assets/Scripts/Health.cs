using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float maxHealth;
    [SerializeField] float currentHealth;
    [SerializeField] GameObject healthBar;
    private float poisonTurn;
    private float poisonDamage;
    SpriteRenderer healthFill;
    SpriteRenderer healthVoid;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthFill = GetComponentsInChildren<SpriteRenderer>()[0];
        healthVoid = GetComponentsInChildren<SpriteRenderer>()[1];

        poisonDamage = 0;
        poisonTurn = 0;

        StartCoroutine(Poison());
    }

    // Update is called once per frame
    void Update()
    {

        healthBar.transform.rotation = Quaternion.Euler(0, 45, 0);
        healthVoid.size = Vector3.right *(1 - currentHealth / maxHealth) * 2f + Vector3.up * 0.5f;
        healthFill.size = Vector3.right * currentHealth / maxHealth * 2f + Vector3.up * 0.5f;
    }

    public void damage(float damage)
    {
        currentHealth = Mathf.Max(currentHealth - damage, 0);
    }

    public void heal(float heal)
    {
        currentHealth = Mathf.Min(currentHealth + heal, maxHealth);
    }

    public float getHealth()
    {
        return currentHealth;
    }

    public void setPoison()
    {
        if (poisonDamage == 0)
        {
            poisonDamage = 1;
        }
        else if (poisonDamage < 2)
        {
            poisonDamage += 0.5f;
        }
        poisonTurn = 3;
    }
    IEnumerator Poison()
    {
        while (true)
        {
            if (poisonTurn > 0)
            {
                currentHealth -= poisonDamage;
                poisonTurn -= 1;
            }
            else if (poisonTurn == 0)
            {
                poisonDamage = 0;
            }
            yield return new WaitForSeconds(1f);
        }

    }
}
