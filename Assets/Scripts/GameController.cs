using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public SpawnManager spManager;
    public int score = 0;
    [SerializeField] TextMeshProUGUI timerText, scoreText;
    [SerializeField] float remainingTime;
    public GameObject OneStars, TwoStars, ThreeStars, GameScene,loseScene;
    public ParticleSystem bom,collectParticles;
    public AudioSource bomAudioSource,WinAudioSource;
    public AudioClip winSound,bomSound,collectSound,losesound;

    void Update()
    {
        timeManager();
        ScoreManager();
    }
    public void ScoreManager()
    {
        scoreText.text = score.ToString() + "/30";
        if (score == 30)
        {
            Debug.Log("win");
            WinAudioSource.clip = winSound;
            WinAudioSource.Play();
            ThreeStars.SetActive(true);
            GameScene.SetActive(false);
        }

    }
    public void timeManager()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
        }


        timerText.text = Mathf.Ceil(remainingTime).ToString(); // Use Mathf.Ceil to round up to the nearest whole number


        if (remainingTime <= 0)
        {
            WinAudioSource.clip = losesound;
            WinAudioSource.Play();
            remainingTime = 0;
            loseScene.SetActive(true);
            GameScene.SetActive(false);
            Debug.Log("you lose");
            //if (score == 6)
            //{
            //    WinAudioSource.clip = winSound;
            //    WinAudioSource.Play();
            //    ThreeStars.SetActive(true);
            //    Debug.Log(" 3 star");
            //}
            //else if( score >= 4)
            //{
            //    WinAudioSource.clip = winSound;
            //    WinAudioSource.Play();
            //    TwoStars.SetActive(true);

            //    Debug.Log("2 stars");

            //}
            //else if( score >= 2)
            //{
            //    WinAudioSource.clip = winSound;
            //    WinAudioSource.Play();
            //    OneStars.SetActive(true);
            //    GameScene.SetActive(false);
            //    Debug.Log("1 stars");

            //}
            //else
            //{
            //loseScene.SetActive(true);
            //GameScene.SetActive(false);

            //Debug.Log("you lose");
            //}

        }
    }
    public void decreaseTime()
    {
        remainingTime -= 2;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bomb"))
        {
            bomAudioSource.clip = bomSound;
            bomAudioSource.Play();
            Debug.Log("bomb");
            decreaseTime();
            bom.Play();
           

        }
        else if (other.CompareTag("logo"))
        {
            score += 1;
            Debug.Log(score);
            WinAudioSource.clip = collectSound;
            WinAudioSource.Play();
            collectParticles.Play();
        }
        else if(other.CompareTag("Letter"))
        {
            score += 1;
            Debug.Log(score);
            WinAudioSource.clip = collectSound;
            WinAudioSource.Play();
            collectParticles.Play();
        }
    }
}
