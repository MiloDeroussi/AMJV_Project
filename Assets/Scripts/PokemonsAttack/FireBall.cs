using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FireBall : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] bool active;
    private Vector3 spawn;
    [SerializeField] GameObject[] fires;
    [SerializeField] GameObject fire;
    private int i;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        active = false;
        i = 0;
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
            if (other.gameObject.layer == 10)
            {
                fire = fires[i];
                i = (i+1) % fires.Length;
                fire.transform.position = this.transform.position;
                fire.SetActive(true);
                active = false;
                this.gameObject.SetActive(false);
            }
    }
}
