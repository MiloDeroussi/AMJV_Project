using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (other.gameObject.layer == 12 )
        {
            other.GetComponent<Health>().damage(5);
        }
        active = false;
        this.gameObject.SetActive(false);
    }
}
