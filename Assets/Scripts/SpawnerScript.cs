using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    [SerializeField] private GameObject pipePrefab;
    [SerializeField] private GameObject heartPrefab; // Префаб сердечка

    //private HeartsScript heartsScript; // Посилання на HeartsScript для перевірки

    private float spawnPeriod = 5f;   // кожні 5 сек
    private float timeLeft;

    void Start()
    {
        timeLeft = 0f;  // = spawnPeriod
    }

    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0f)
        {
            timeLeft = spawnPeriod;
            GameObject pipe = SpawnPipe();
            
            // Додавання сердечка між трубами
            if (CountHearts() < 1)
            {
                Debug.Log("HEART is Spawned!");
                SpawnHeart(pipe.transform);
            }
        }
    }

    private GameObject SpawnPipe()
    {
        var pipe = Instantiate(pipePrefab);
        pipe.transform.position = transform.position + Vector3.up * Random.Range(-1f, 1f);
        return pipe; // Повертаємо створений об'єкт
    }

   
    private void SpawnHeart(Transform parent)
    {
        var heart = Instantiate(heartPrefab, parent);
        heart.transform.localPosition = Vector3.up * Random.Range(-1f, 1f);
        heart.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f); //  масштаб 
    }

    private int CountHearts()
    {
        // Підрахунок кількості сердечок в сцені
        var hearts = GameObject.FindGameObjectsWithTag("Heart");
        return hearts.Length;
    }
}

