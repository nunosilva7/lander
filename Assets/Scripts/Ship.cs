using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Ship : MonoBehaviour
{

    [SerializeField]
    float maxRelativeVelocity = 2f;

    [SerializeField]
    float maxRotation = 10f;

    [SerializeField]
    float thurstForce = 150f;

    [SerializeField]
    float torqueForce = 25f;

    [SerializeField]
    float fuel = 500f; // quantidade de combustivel

    [SerializeField]
    float fuelSpentThurst = 10f; //combustivel gasto 

    [SerializeField]
    float fuelSpentTorque = 5f; // combustivel gasto com torque;


    [SerializeField] TextMeshProUGUI showFuel;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            GetComponent<Rigidbody2D>().AddForce(thurstForce * transform.up * Time.deltaTime);

            fuel -= fuelSpentThurst * Time.deltaTime;
        }
        



        if (Input.GetKey(KeyCode.LeftArrow) && fuel > 0)
        {
            GetComponent<Rigidbody2D>().AddTorque(torqueForce * Time.deltaTime);

            fuel -= fuelSpentTorque * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.RightArrow) && fuel >0)
        {
            GetComponent<Rigidbody2D>().AddTorque(-torqueForce * Time.deltaTime);

            fuel -= fuelSpentTorque * Time.deltaTime;

        }

        showFuel.text = "Fuel " + Mathf.RoundToInt(fuel).ToString();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if(collision.gameObject.tag =="Platform")
        {
            //bati na plataforma
            Debug.Log("Aterrei na plataforma");

            SceneManager.LoadScene("Win");
            if(collision.relativeVelocity.magnitude > maxRelativeVelocity || Mathf.Abs(transform.localEulerAngles.z) > maxRotation)
            {
                Debug.Log("Mas arrebentei");
                SceneManager.LoadScene("GameOver");
            }
        }
        else
        {
            Debug.Log("Bati na lua");
            SceneManager.LoadScene("GameOver");
        }
    }
    
}
