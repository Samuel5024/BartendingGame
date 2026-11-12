using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // bool levelCompleted = false;
    public GameObject completeLevelUI;
    
    public void FinishLevel()
    {
        completeLevelUI.SetActive(true);
    }
}
