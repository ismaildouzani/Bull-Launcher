// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.InputSystem;
// using UnityEngine.InputSystem.EnhancedTouch;
// public class BallHandler : MonoBehaviour
// {
//     [SerializeField] private GameObject ballPrefab;
//     [SerializeField] private Rigidbody2D pivot;
//     [SerializeField] private float detachDelay; 
//     [SerializeField] private float respawnDelay;

//      private Rigidbody2D currentBallRigidbody;
//      private SpringJoint2D currentBallSprintJoint;

//     private Camera mainCamera;
//     private bool isDragging;
    
//     // Start is called before the first frame update
//     void Start()
//     {
//         mainCamera = Camera.main;
//         SpawnNweBall();
//     }
//     void OnEnable()
    
//     {
//         EnhancedTouchSupport.Enable();
//     }

//     void OnDisable() 
//     {
//          EnhancedTouchSupport.Disable();

//     }

//     // Update is called once per frame
//     void Update()
//     {
//         if (currentBallRigidbody == null) {return;}
//         if (Touch.activeTouches.Count == 0)
//         {
//             if(isDragging) 
//             {
//                 LaunchBall();

//             }

//             isDragging =  false;
//             return;
//         }

//         isDragging = true;
//         currentBallRigidbody.isKinematic = true;
//         Vector2 touchPosition = new Vector2();

//         foreach(Touch touch in Touch.activeTouches)
//         {
//             touchPosition += touch.screenPosition;
//         }

//         touchPosition /= Touch.activeTouches.Count;

//         Vector3 worldPosition = mainCamera.ScreenToWorldPoint(touchPosition);
//        currentBallRigidbody.position = worldPosition;


//     }
//     private void SpawnNweBall() 

//     {
//         GameObject ballInstance = Instantiate(ballPrefab, pivot.position, Quaternion.identity);

//         currentBallRigidbody = ballInstance.GetComponent<Rigidbody2D>();
//         currentBallSprintJoint = ballInstance.GetComponent<SpringJoint2D>();

//         currentBallSprintJoint.connectedBody = pivot;
//     }
//     private void LaunchBall() 
//     {
//         currentBallRigidbody.isKinematic = false;
//         currentBallRigidbody = null;

       
//         Invoke(nameof(DetachaBall), detachDelay);

//     }
//     private void DetachaBall()
//     {
//          currentBallSprintJoint.enabled = false;

//         currentBallSprintJoint = null;
//         Invoke(nameof(SpawnNweBall), respawnDelay);
//     }
// }


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;

public class BallHandler : MonoBehaviour
{
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private Rigidbody2D pivot;
    [SerializeField] private float detachDelay;
    [SerializeField] private float respawnDelay;

    private Rigidbody2D currentBallRigidbody;
    private SpringJoint2D currentBallSpringJoint;

    private Camera mainCamera;
    private bool isDragging;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        SpawnNewBall(); // Corrected method name
    }

    void OnEnable()
    {
        EnhancedTouchSupport.Enable(); // Enable Enhanced Touch support
    }

    void OnDisable()
    {
        EnhancedTouchSupport.Disable(); // Disable Enhanced Touch support
    }

    // Update is called once per frame
    void Update()
    {
        if (currentBallRigidbody == null) return;

        // Use the correct namespace for EnhancedTouch's Touch class
        if (UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches.Count == 0)
        {
            if (isDragging)
            {
                LaunchBall();
            }

            isDragging = false;
            return;
        }

        isDragging = true;
        currentBallRigidbody.isKinematic = true;

        Vector2 touchPosition = Vector2.zero; // Initialize touchPosition

        // Accumulate screen positions of all active touches
        foreach (var touch in UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches)
        {
            touchPosition += touch.screenPosition;
        }

        // Get the average touch position
        touchPosition /= UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches.Count;

        // Convert screen position (Vector2) to world position (Vector3) with z = 0f
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(new Vector3(touchPosition.x, touchPosition.y, mainCamera.nearClipPlane));
        currentBallRigidbody.position = new Vector2(worldPosition.x, worldPosition.y); // Assign only x, y for 2D
    }

    private void SpawnNewBall() // Corrected method name
    {
        GameObject ballInstance = Instantiate(ballPrefab, pivot.position, Quaternion.identity);

        currentBallRigidbody = ballInstance.GetComponent<Rigidbody2D>();
        currentBallSpringJoint = ballInstance.GetComponent<SpringJoint2D>();

        currentBallSpringJoint.connectedBody = pivot;
    }

    private void LaunchBall()
    {
        currentBallRigidbody.isKinematic = false;
        currentBallRigidbody = null;

        Invoke(nameof(DetachBall), detachDelay); // Corrected method name
    }

    private void DetachBall() // Corrected method name
    {
        currentBallSpringJoint.enabled = false;
        currentBallSpringJoint = null;
        Invoke(nameof(SpawnNewBall), respawnDelay);
    }
}
