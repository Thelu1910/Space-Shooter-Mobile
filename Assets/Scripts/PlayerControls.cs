using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

public class PlayerControls : MonoBehaviour
{
    private Camera mainCam;
    private Vector3 offset;

    private float maxLeft;
    private float maxRight;
    private float maxDown;
    private float maxUp;

    private void Start()
    {
        mainCam = Camera.main;

        StartCoroutine(SetBoundaries());
    }

    private void Update()
    {
        if (Touch.activeTouches.Count > 0)
        {
            if (Touch.activeTouches[0].finger.index == 0)
            {
                Touch myTouch = Touch.activeTouches[0];
                Vector3 touchPos = myTouch.screenPosition;

#if UNITY_EDITOR
                if (touchPos.x == Mathf.Infinity)             // in case test on simulator, press outside screen and unity report error
                    return;
#endif

                touchPos = mainCam.ScreenToWorldPoint(touchPos);

                if (Touch.activeTouches[0].phase == TouchPhase.Began)
                    offset = touchPos - transform.position;

                if (Touch.activeTouches[0].phase == TouchPhase.Moved)
                    transform.position = new Vector3(touchPos.x - offset.x, touchPos.y - offset.y, 0);

                if (Touch.activeTouches[0].phase == TouchPhase.Stationary)
                    transform.position = new Vector3(touchPos.x - offset.x, touchPos.y - offset.y, 0);
            } 

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, maxLeft, maxRight), Mathf.Clamp(transform.position.y, maxDown, maxUp), 0);
        }
    }

    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
    }

    private void OnDisable()
    {
        EnhancedTouchSupport.Disable();
    }

    private IEnumerator SetBoundaries ()
    {
        // give time for cinemachine to adjust camera
        yield return new WaitForEndOfFrame();

        // yield return new WaitForSeconds(0.4f);         use this for old phone

        maxLeft = mainCam.ViewportToWorldPoint(new Vector2(0.15f, 0)).x;
        maxRight = mainCam.ViewportToWorldPoint(new Vector2(0.85f, 0)).x;
        maxDown = mainCam.ViewportToWorldPoint(new Vector2(0, 0.15f)).y;
        maxUp = mainCam.ViewportToWorldPoint(new Vector2(0, 0.6f)).y;
    }
}
