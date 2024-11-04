using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private RectTransform menuUI;
    [SerializeField] private VideoPlayer gameBackStoryVideo;
    [SerializeField] private Button playButton;

    private void Awake()
    {
        playButton.onClick.AddListener(() =>
        {
            PlayButtonAction();
        });
    }

    private void PlayButtonAction()
    {
        menuUI.gameObject.SetActive(false);
        gameBackStoryVideo.Play();
        StartCoroutine(IE_StartGame());
    }

    private IEnumerator IE_StartGame()
    {
        yield return new WaitForSeconds((float)(gameBackStoryVideo.length + 0.5f));
        SceneManager.LoadScene(1);
    }
}
