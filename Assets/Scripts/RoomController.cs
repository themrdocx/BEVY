using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class RoomController : MonoBehaviour
{
   private CinemachineVirtualCamera virtualCamera;
   private StartFlag startFlag;

   private void Awake()
   {
      virtualCamera = GetComponentInChildren<CinemachineVirtualCamera>();
      Debug.Assert(virtualCamera!=null,"NO VIRTUAL CAMERA ADDED TO ROOM : "+gameObject.name);
      virtualCamera.gameObject.SetActive(false);

      startFlag = GetComponentInChildren<StartFlag>();
      Debug.Assert(startFlag!=null,"NO START FLAG ADDED TO ROOM : "+ gameObject.name);
   }

   private void OnTriggerEnter2D(Collider2D other)
   {
      if (other.gameObject.GetComponent<Player>() && !other.isTrigger)
      {
         virtualCamera.gameObject.SetActive(true);
      }
   }
   private void OnTriggerExit2D(Collider2D other)
   {
      if (other.gameObject.GetComponent<Player>() && !other.isTrigger)
      {
         virtualCamera.gameObject.SetActive(false);
      }
   }
}
