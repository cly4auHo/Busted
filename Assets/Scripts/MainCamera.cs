using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [SerializeField] private Transform target;
    private Vector3 offset;
    private Vector3 desirePorition;
    private Vector3 smoothPosition;
    private float smoothSpeed = 0.05f;
    private const string busTag = "Bus";

    void Start()
    {
        offset = target.position - transform.position;
    }

    void FixedUpdate()
    {
        desirePorition = target.position - offset;
        smoothPosition = Vector3.Lerp(transform.position, desirePorition, smoothSpeed);

        transform.position = smoothPosition;
        transform.LookAt(target);
    }

    public void NewTarget()
    {
        target = FindObjectOfType<Bus>().transform;
    }
}
