using UnityEngine;

public class Bus : MonoBehaviour
{
    private Rigidbody rb;
    private bool canRun;

    private float speed;
    private float minSpeed = 0f;
    private float maxSpeed = 1000f;
    private float deltaSpeed = 25f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        canRun = false;
        speed = minSpeed;
    }

    void Update()
    {
        rb.velocity = transform.forward.normalized * speed * Time.deltaTime;

        if (!canRun || Input.GetMouseButton(0))
        {
            speed = Mathf.Max(minSpeed, speed - deltaSpeed);
        }
        else
        {
            speed = Mathf.Min(speed + deltaSpeed, maxSpeed);
        }
    }

    public bool IsStoped()
    {
        return speed <= minSpeed;
    }

    public void StartBus()
    {
        canRun = true;
    }

    public void StopBus()
    {
        canRun = false;
    }
}
