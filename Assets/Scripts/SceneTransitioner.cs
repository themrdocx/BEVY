using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitioner : MonoBehaviour
{
    public Vector3 pos;
    public void ChangePos()
    {
        Camera.main.transform.position = pos;
    }

    public void TransitionToGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
