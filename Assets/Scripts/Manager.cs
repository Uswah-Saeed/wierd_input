using DG.Tweening;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public static Manager instance;
    public GameObject SummaryScreen, BattleScene;
    public GameObject Win, Lose, Health;
    [SerializeField]
    GameObject Ship;
    public float bobDuration, swayAngle, swayDuration;
    public Vector3 bobHeight;

    [SerializeField] private Button menuButton;
    [SerializeField] private Button replayButton;


    private void Awake()
    {
        instance = this;

        menuButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.PlayUIButtonSound();
            SceneManager.LoadScene(0);
        });
        replayButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.PlayUIButtonSound();
            SceneManager.LoadScene(1);
        });
    }

    private void Start()
    {
        SummaryScreen.SetActive(false);
        ShipMovement();
    }

    public void CallummaryMenu(bool isWin)
    {
        SummaryScreen.SetActive(true);
        BattleScene.SetActive(false);
        if (isWin)
        {
            Win.SetActive(true); 
            Lose.SetActive(false);
            SoundManager.Instance.PlaySuccessSound();
        }
        else
        {
            Win.SetActive(false); 
            Lose.SetActive(true);
            SoundManager.Instance.PlayFailureSound();
        }
    }

    public void ShipMovement()
    {
        Ship.transform.DOMove(Ship.transform.position + bobHeight, bobDuration)
        .SetLoops(-1, LoopType.Yoyo)
         .SetEase(Ease.InOutSine);
        Ship.transform.DORotate(new Vector3(0, 0, swayAngle), swayDuration)
         .SetLoops(-1, LoopType.Yoyo)
         .SetEase(Ease.InOutSine);
    }
}
