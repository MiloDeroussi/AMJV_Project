using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class PiegeDeRoc : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] bool active;
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
            active = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 11)
        {
            float damage = other.GetComponent<NavMeshAgent>().velocity.magnitude * 0.3f;
            other.GetComponent<Health>().damage(damage);
            active = false;
        }
    }
}
