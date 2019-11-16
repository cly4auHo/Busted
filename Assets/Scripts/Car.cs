using UnityEngine;

public class Car : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float speed = 1000f;
    private const string busTag = "Bus";

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        rb.velocity = transform.forward.normalized * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(busTag))
        {
            Destroy(gameObject);
        }
    }
}
