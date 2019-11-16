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

    private float startSpeed = 8f;
    private float maxSpeed = 25f;
    private float deltaSpeed = 3f;

    private Vector3 startPosition;
    private float rangeToBus = 25f;

    void Start()
    {
        bus = FindObjectOfType<Bus>();
        gm = FindObjectOfType<GameManager>();
        nav = GetComponent<NavMeshAgent>();
        nav.speed = startSpeed;

        hitAtCar = false;
        startPosition = transform.position;
    }

    void Update()
    {
        if (!hitAtCar)
        {
            if (bus.IsStoped() && Vector3.Distance(transform.position, bus.transform.position) < rangeToBus)
            {
                nav.SetDestination(bus.transform.position);
                nav.speed = Mathf.Min(nav.speed + deltaSpeed, maxSpeed);
            }
            else
            {
                nav.SetDestination(startPosition);
            }

            if (afterHit != null)
            {
                afterHit = null;
                StopCoroutine(AfterHit());
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

            if (afterHit == null)
            {
                afterHit = StartCoroutine(AfterHit());
            }
        }
    }

    private IEnumerator AfterHit()
    {
        yield return new WaitForSeconds(timeToGo);

        nav.isStopped = false;
        nav.speed = startSpeed;
        hitAtCar = false;
    }
}
