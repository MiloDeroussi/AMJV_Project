using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialFleche : MonoBehaviour
{
    private Rigidbody rb;
    private bool active;
    private Vector3 spawn;
    private int hits;
    [SerializeField] float speed;
    [SerializeField] float range;
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
            active = true;
            hits = 0;
            rb.velocity = transform.forward * speed;
        }

        if ((transform.position - spawn).sqrMagnitude > range)
        {
            active = false;
            this.gameObject.SetActive(false);
        }

        if (hits == 3)
        {
            active = false;
            this.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 12)
        {
            other.GetComponent<Health>().damage(5);
        }
    }
}
