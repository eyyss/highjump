using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public float currentPoint;

    private void Awake()
    {
        Instance = this;
    }

    public void RestartGame()
    {

    }

    public void SetPoint(float point)
    {
        currentPoint = point;
    }
    public void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

}
