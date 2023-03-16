using UnityEngine;
using UnityEngine.SceneManagement;
public class Restart : MonoBehaviour
{

    void Start()
    {
        gameObject.SetActive(false);
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }


}
