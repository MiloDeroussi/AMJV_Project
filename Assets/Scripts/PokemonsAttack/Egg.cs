using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Egg : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] bool active;
    private Vector3 spawn;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        active = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!active)
        {
            spawn = transform.position;
            rb.velocity = (transform.forward * 7);
            active = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, 5);
        foreach (Collider c in hits)
        {
            if (c.gameObject.layer == 12)
            {
                c.GetComponent<Health>().damage(5);
            }
            
        }

        active = false;
        this.gameObject.SetActive(false);
    }
}
