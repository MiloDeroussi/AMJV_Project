using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float maxHealth;
    [SerializeField] float currentHealth;
    [SerializeField] GameObject healthBar;
    SpriteRenderer healthFill;
    SpriteRenderer healthVoid;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthFill = GetComponentsInChildren<SpriteRenderer>()[0];
        healthVoid = GetComponentsInChildren<SpriteRenderer>()[1];
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            this.enabled = false;
        }

        healthBar.transform.rotation = Quaternion.Euler(0, 45, 0);
        healthVoid.size = Vector3.right *(1 - currentHealth / maxHealth) * 2f + Vector3.up * 0.5f;
        healthFill.size = Vector3.right * currentHealth / maxHealth * 2f + Vector3.up * 0.5f;
    }

    public void damage(float damage)
    {
        currentHealth = Mathf.Min(currentHealth - damage, 0);
    }

    public void heal(float heal)
    {
        currentHealth = Mathf.Max(currentHealth + heal, maxHealth);
    }
}
