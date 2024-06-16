using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class BirdScript : MonoBehaviour
{
    [SerializeField]
    private TMPro.TextMeshProUGUI passedLabel;
    [SerializeField]
    private GameObject alert;
    [SerializeField]
    private TMPro.TextMeshProUGUI alertLabel;

    private Rigidbody2D rigidBody;   // Посилання на компонент того ж ГО, на якому скрипт
    private int score;
    private bool needClear;
    public HeartsScript heartsScript;

    void Start()
    {
        Debug.Log("BirdScript Start");
        // пошук компонента та одержання посилання на нього
        rigidBody = GetComponent<Rigidbody2D>();
        score = 0;
        needClear = false;
        HideAlert();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigidBody.AddForce(new Vector2(0, 300) * Time.timeScale);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (alert.activeSelf)
            {
                HideAlert();
            }
            else
            {
                ShowAlert("Paused");
            }
        }
    }

    /* Подія, що виникає при перетині колайдерів-тригерів */
    private void OnTriggerEnter2D(Collider2D other)
    {
        int lives = heartsScript.lives; // Отримання значення lives
        if (lives == 0)
        {
            Debug.Log("Lives: " + lives);
            ShowAlert("GAME OVER!");

            StartCoroutine(CloseGameWithDelay(7f)); // Затримка 7 секунд перед закриттям гри
            
        }
        else 
        { 
            if (other.gameObject.CompareTag("Pipe"))
            {
                //Debug.Log("Collision!! " + other.gameObject.name);
                heartsScript.DecreaseLife();  // мінус життя
                                              //Debug.Log("Life decreased! 1");
                needClear = true;
                ShowAlert("OOOPS!");
            }
            if (other.gameObject.CompareTag("Heart"))
            {
                Debug.Log("Lives: " + lives);
                Debug.Log("Collected Heart!! " + other.gameObject.name);
                heartsScript.IncreaseLife();  // плюс життя
                Destroy(other.gameObject);
            }
        }
    }

    private IEnumerator CloseGameWithDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);  // Використовуємо WaitForSecondsRealtime для обійдення Time.timeScale = 0
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Зупинити гру в редакторі
        #else
        Application.Quit(); // Закрити гру у збірці
        #endif
    }

    /* Подія, що виникає при роз'єднанні колайдерів-тригерів */
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Pass"))
        {
            //Debug.Log("+1");
            score++;
            passedLabel.text = score.ToString("D3");
        }
    }

    public void ShowAlert(string message)
    {
        Debug.Log("A L E R T !");
        alert.SetActive(true);
        alertLabel.text = message;
        Time.timeScale = 0f;
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void HideAlert()
    {
        alert.SetActive(false);
        Time.timeScale = 1f;
        if (needClear)
        {
            foreach (var pipe in GameObject.FindGameObjectsWithTag("Pass"))
            {
                GameObject.Destroy(pipe);
            }
            needClear = false;
        }
    }
}

  