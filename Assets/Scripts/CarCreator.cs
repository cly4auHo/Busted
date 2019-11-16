using UnityEngine;

public class CarCreator : MonoBehaviour
{
    private float xLeftRoad = -10f;
    private float xRightRoad = 10f;
    private float yHeight = 1f;
    private float zTop;
    private float zBot;

    [SerializeField] private GameObject carPrefab;
    private const string cameraTag = "MainCamera";
    private Bus bus;

    [SerializeField] private float timeSpawn = 5f;
    private float timer;
    private float timeOfSpawn;

    void Start()
    {
        bus = FindObjectOfType<Bus>();

        timer = Time.timeSinceLevelLoad;
        timeOfSpawn = Mathf.Infinity;
    }

    void Update()
    {
        if (Time.timeSinceLevelLoad - timer > timeOfSpawn)
        {
            // instatiate car
            ChangeTime();
        }
    }

    public void StartAction()
    {
        timeOfSpawn = timeSpawn;
        ChangeTime();
    }

    public void StopAction()
    {
        timeOfSpawn = Mathf.Infinity;
    }

    private void ChangeTime()
    {
        timer = Time.timeSinceLevelLoad;
    }
}
