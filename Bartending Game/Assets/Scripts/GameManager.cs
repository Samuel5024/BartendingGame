using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool levelCompleted = false;
    
    public void FinishLevel()
    {
        Debug.Log("Level Completed!");
    }
}
