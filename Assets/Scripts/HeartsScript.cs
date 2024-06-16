using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HeartsScript : MonoBehaviour
{
    public Image[] hearts; // Масив зображень сердечок
    public int lives;
    private BirdScript birdScript;


    void Start()
    {
        birdScript = FindObjectOfType<BirdScript>();
        lives = hearts.Length; // Ініціалізація кількості життів
        StartCoroutine(SwingHearts());
        birdScript = FindObjectOfType<BirdScript>(); // Знаходимо BirdScript на сцені
        if (birdScript == null)
        {
            Debug.LogError("BirdScript не знайдено на сцені.");//////////////
        }
        if (!birdScript.gameObject.activeSelf)
        {
            Debug.LogWarning("BirdScript вимкнений на сцені.");//////////////
        }
    }

    IEnumerator SwingHearts()
    {
        while (true)
        {
            foreach (Image heart in hearts)
            {
                // Гойдання сердечок
                if (heart != null)
                {
                    heart.transform.localPosition = new Vector3(
                    heart.transform.localPosition.x,
                    Mathf.PingPong(Time.time * 50, 20) - 10,
                    heart.transform.localPosition.z);
                }
            }
            yield return null;
        }
    }

    public void DecreaseLife()
    {
        if (lives - 1 >= 0)
        {
            Debug.Log("Decrease: " + lives);
            lives--;
            //hearts[lives].enabled = false; // Вимкнення зображення сердечка
            hearts[lives].gameObject.SetActive(false);
            Debug.Log("Heart " + (lives + 1) + " is now disabled.");
        }

        if (lives <= 0)    /////////////////////////////
        {
            if (birdScript != null)
            {
                Debug.Log("GAME OVER!");
                birdScript.ShowAlert("GAME OVER!");
            }
            else
            {
                Debug.LogError("BirdScript не ініціалізовано перед викликом ShowAlert.");
            } 
        }
    }

    public int GetLives()
    {
        return lives;
    }

    public void IncreaseLife()
    {
        if (lives < 3)
        {
            //hearts[lives].enabled = true; // Включення зображення сердечка 
            hearts[lives].gameObject.SetActive(true);
            Debug.Log("Increase_2: " + lives + 1);
            lives++;
        }       
    }
}
