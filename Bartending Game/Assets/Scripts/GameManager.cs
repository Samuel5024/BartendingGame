using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool levelCompleted = false;
    public GameObject completeLevelUI;
    
    
    public void FinishLevel()
    {
        bool levelCompleted = true;
        Time.timeScale = 0f;
        completeLevelUI.SetActive(true);
    }

}
