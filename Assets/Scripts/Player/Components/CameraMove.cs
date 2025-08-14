using System.Collections;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform defoultTransform;
    public Transform SlideTransform;

    public Camera gameCamera;

    public float transitionDuration = 2f; // Тривалість переходу

    private void Start()
    {
        defoultCameraSet();
    }

    public void slideCameraSet()
    {
        StartCoroutine(SmoothTransition(gameCamera.transform.localPosition, gameCamera.transform.localRotation, SlideTransform.localPosition, SlideTransform.localRotation));
    }

    public void defoultCameraSet()
    {
        StartCoroutine(SmoothTransition(gameCamera.transform.localPosition, gameCamera.transform.localRotation, defoultTransform.localPosition, defoultTransform.localRotation));
    }

    private IEnumerator SmoothTransition(Vector3 startLocalPosition, Quaternion startLocalRotation, Vector3 endLocalPosition, Quaternion endLocalRotation)
    {
        float elapsedTime = 0f;

        while (elapsedTime < transitionDuration)
        {
            float t = Mathf.SmoothStep(0f, 1f, elapsedTime / transitionDuration); // Плавне пришвидшення і сповільнення

            gameCamera.transform.localPosition = Vector3.Lerp(startLocalPosition, endLocalPosition, t);
            gameCamera.transform.localRotation = Quaternion.Slerp(startLocalRotation, endLocalRotation, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Переконатися, що позиція та ротація досягли кінцевих значень
        gameCamera.transform.localPosition = endLocalPosition;
        gameCamera.transform.localRotation = endLocalRotation;
    }
}
