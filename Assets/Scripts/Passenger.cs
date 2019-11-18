using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class Passenger : MonoBehaviour
{
    private Bus bus;
    private GameManager gm;
    private NavMeshAgent nav;
    private Rigidbody rb;
    private const string busTag = "Bus";
    private const string carTag = "Car";

    private Coroutine afterHit = null;
    private bool hitAtCar = false;
    private float timeToGo = 1f;

    private float startSpeed = 8f;
    private float maxSpeed = 20f;
    private float deltaSpeed = 2f;

    private Vector3 startPosition;
    private float rangeToBus = 25f;

    private bool folowingBus = false;
    private bool folowingStart = false;
    [SerializeField] private float collisionForce = 50;

    void Start()
    {
        bus = FindObjectOfType<Bus>();
        gm = FindObjectOfType<GameManager>();
        nav = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        nav.speed = startSpeed;
        rb.isKinematic = true;

        startPosition = transform.position;
    }

    void Update()
    {
        if (!hitAtCar && bus.IsStoped() && Vector3.Distance(transform.position, bus.transform.position) < rangeToBus)
        {
            if (!folowingBus)
            {
                nav.SetDestination(bus.transform.position);
                folowingBus = true;
                folowingStart = false;
            }

            nav.speed = Mathf.Min(nav.speed + deltaSpeed, maxSpeed);
        }
        else if (!hitAtCar && !bus.IsStoped() && Vector3.Distance(transform.position, bus.transform.position) >= rangeToBus)
        {
            if (!folowingStart)
            {
                nav.SetDestination(startPosition);
                folowingBus = false;
                folowingStart = true;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(busTag))
        {
            gm.ScoreUP();
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == carTag)
        {
            hitAtCar = true;
            folowingBus = false;
            folowingStart = false;
            nav.enabled = false;

            var forceNormale = collision.contacts[0].normal;

            rb.isKinematic = false;
            rb.AddForce(forceNormale * collisionForce);

            if (afterHit == null)
            {
                afterHit = StartCoroutine(AfterHit());
            }
        }
    }

    private IEnumerator AfterHit()
    {
        yield return new WaitForSeconds(timeToGo);

        rb.isKinematic = true;
        nav.enabled = true;
        nav.speed = startSpeed;
        hitAtCar = false;
    }
}
