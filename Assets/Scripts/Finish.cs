using UnityEngine;

public class Finish : MonoBehaviour
{
    private GameManager gm;
    private Bus bus;
    private CarCreator carCreator;
    private const string busTag = "Bus";

    [SerializeField] private GameObject finishPanel;
    [SerializeField] private GameObject buttonNextLVL;

    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        bus = FindObjectOfType<Bus>();
        carCreator = FindObjectOfType<CarCreator>();

        finishPanel.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(busTag))
        {
            bus.StopBus();
            carCreator.StopAction();

            finishPanel.SetActive(true);

            if (gm.CompletedLVL())
            {
                buttonNextLVL.SetActive(true);
            }
        }
    }
}
