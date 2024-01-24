using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class Fire : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] bool active;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        active = false;
        StartCoroutine(firedamage());
    }

    // Update is called once per frame
    void Update()
    {
        if (!active)
        {
            active = true;
        }
    }

    IEnumerator firedamage()
    {
        while (true)
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, 2);
            foreach (Collider c in hits)
            {
                if (c.gameObject.layer == 12)
                {
                    c.GetComponent<Health>().damage(5);
                }
            }
            yield return new WaitForSeconds(1);
        }
    }
}
