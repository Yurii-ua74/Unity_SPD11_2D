using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdScript : MonoBehaviour
{
    // Посилання на компонент того ж ГО, на якому скріпт
    private Rigidbody2D rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("BirdScript Start");
        // одержуємо посилання
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            rigidBody.AddForce(new Vector2(0, 500));
        }
    }
}
