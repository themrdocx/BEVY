using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private float camaeraTransitionTime = 0.2f;
    [SerializeField] private Toggle toggle;
    private bool cameraLerpActive = false;
    private Vector3 cameraNewPos = new Vector3(0,0,-10);
    private GameObject camera;

    private void Start()
    {
        camera = Camera.main.gameObject;
        Debug.Assert(camera!=null);
    }
    
    public void ChangeCameraPosX(float x)
    {
        cameraLerpActive = true;
        cameraNewPos.x = x;
    }

    private void Update()
    {
        if(!cameraLerpActive)
            return;

        if (camera.transform.position != cameraNewPos)
        {
            camera.transform.position = Vector3.MoveTowards(camera.transform.position, cameraNewPos, camaeraTransitionTime*Time.deltaTime);
        }
        else
        {
            cameraLerpActive = false;
        }
    }

    public void SetFullScreen()
    {
        Screen.fullScreen = toggle.isOn;
    }
}
