using UnityEngine;

public class Manager : MonoBehaviour
{
    public static Manager instance;
    public GameObject SummaryScreen, BattleScene;
    public GameObject Win, Lose, Health;

    private void Start()
    {
        instance=this;
        SummaryScreen.SetActive(false);

    }
    public void CallummaryMenu(bool isWin)
    {
        SummaryScreen.SetActive(true);
        BattleScene.SetActive(false);
        if (isWin)
        {
            Win.SetActive(true); Lose.SetActive(false);  
        }
        else
        {
            Win.SetActive(false); Lose.SetActive(true);

        }
    }

}
