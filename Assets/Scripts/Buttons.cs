using UnityEngine;

public class Buttons : MonoBehaviour
{
    [SerializeField] private GameObject buttonStart;

    private Bus bus;
    private CarCreator carCreator;
    private LvlChanger lvlChange;

    void Start()
    {
        buttonStart.SetActive(true);

        lvlChange = FindObjectOfType<LvlChanger>();
        bus = FindObjectOfType<Bus>();
        carCreator = FindObjectOfType<CarCreator>();
    }

    public void StartGame()
    {
        bus.StartBus();
        carCreator.StartAction();
        buttonStart.SetActive(false);
    }

    public void NextLVL()
    {

    }

    public void Restart()
    {

    }
}
