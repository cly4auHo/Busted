using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class Passenger : MonoBehaviour
{
    private Bus bus;
    private GameManager gm;
    private NavMeshAgent nav;
    private const string busTag = "Bus";
    private const string carTag = "Car";

    private Coroutine afterHit = null;
    private bool hitAtCar;
    private float timeToGo = 1f;
    private Rigidbody rigidbody;
    private float startSpeed = 8f;
    private float maxSpeed = 25f;
    private float deltaSpeed = 3f;

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
        nav.speed = startSpeed;
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.isKinematic = true;
        hitAtCar = false;
        startPosition = transform.position;
    }

    void Update()
    {
        if (!hitAtCar)
        {
            if (bus.IsStoped() && Vector3.Distance(transform.position, bus.transform.position) < rangeToBus)
            {
                if (!folowingBus)
                {
                    nav.SetDestination(bus.transform.position);
                    folowingBus = true;
                    folowingStart = false;
                }

                nav.speed = Mathf.Min(nav.speed + deltaSpeed, maxSpeed);
            }
            else
            {
                if (!folowingStart)
                {
                    nav.SetDestination(startPosition);
                    folowingBus = false;
                    folowingStart = true;
                }
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
            nav.isStopped = true;
            hitAtCar = true;

            var forceNormale = collision.contacts[0].normal;

            rigidbody.isKinematic = false;
            nav.enabled = false;
            rigidbody.AddForce(forceNormale * collisionForce);

            if (afterHit == null)
            {
                afterHit = StartCoroutine(AfterHit());
            }
        }
    }

    private IEnumerator AfterHit()
    {
        yield return new WaitForSeconds(timeToGo);
        rigidbody.isKinematic = true;
        nav.enabled = true;
        nav.isStopped = false;
        nav.speed = startSpeed;
        hitAtCar = false;
    }
}
