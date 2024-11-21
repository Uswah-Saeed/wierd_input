using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private RectTransform menuUI;
    [SerializeField] private RectTransform videoUI;
    [SerializeField] private VideoPlayer gameBackStoryVideo;
    [SerializeField] private Button playButton;
    [SerializeField] private Button skipButton;
    public AudioSource BackgroundMusic;

    private void Awake()
    {

        StartCoroutine(PlayButtonActive());

    }
    private IEnumerator PlayButtonActive()
    {
        BackgroundMusic.Play();
        videoUI.gameObject.SetActive(false);

        playButton.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        playButton.gameObject.SetActive(true);
        playButton.onClick.AddListener(() =>
        {
            PlayButtonAction();
        });
        skipButton.onClick.AddListener(() =>
        {
            SkipButtonAction();
        });
        yield break;
           

    }

    private void PlayButtonAction()
    {
        menuUI.gameObject.SetActive(false);
        gameBackStoryVideo.Play();
        StartCoroutine(IE_StartGame());
    }


    private IEnumerator IE_StartGame()
    {
        yield return new WaitForSeconds(1);
        videoUI.gameObject.SetActive(true);
        yield return new WaitForSeconds((float)(gameBackStoryVideo.length + 0.5f));
        BackgroundMusic.Stop();
        SceneManager.LoadScene(1);
    }

    private void SkipButtonAction()
    {
        gameBackStoryVideo.Stop();
        SceneManager.LoadScene(1);
    }
}
