using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Controller : MonoBehaviour
{
    public Transform hand;       // Reference to the hand object in world space
    public Image uiImage;         // Reference to the UI image you want to move
    public Camera mainCamera;     // Reference to the camera rendering the Canvas
    public Canvas canvas;
    public TextMeshProUGUI detectedText;
    public bool OneTime;
    public GameObject hands, DetectCanvas , gameCanvas;
    public List<GameObject> handsList;

    void Start()
    {
       OneTime = true;
    }
    float Map(float value, float inMin, float inMax, float outMin, float outMax)
    {
        return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }

    #region Test With Mouse Reference it from Inspector
    public void ClickableAgent()
    {
        
            Debug.Log("open");
            DetectCanvas.SetActive(false);
            hands.SetActive(false);
            gameCanvas.SetActive(true);
            OneTime = false;
        
    }
    #endregion

    void Update()
    {

        foreach (GameObject handObject in handsList)
        {
            if (handObject.active)
            {
                hand = handObject.transform; 
                
                break;
            }
        }

        

        if (hand == null) return;








        // Landscape
        Vector3 screenPos = mainCamera.WorldToScreenPoint(hand.position);
        Debug.Log(screenPos);

        float xx = Map(screenPos.x, 400, 1100, 0, 1800);
        screenPos.x = xx;

        // Get Canvas dimensions
        RectTransform canvasRect = canvas.transform as RectTransform;
        float canvasWidth = canvasRect.rect.width;
        float canvasHeight = canvasRect.rect.height;

        // Convert screen position to Canvas local position
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRect, screenPos, mainCamera, out Vector2 localPos);

        // Clamp X position within the Canvas bounds
        localPos.x = Mathf.Clamp(localPos.x, -canvasWidth / 2, canvasWidth / 2);

        // Fix Y position near the bottom of the screen
        localPos.y = -canvasHeight / 2 + 50; // Adjust this value as needed

        // Set UIImage position
        uiImage.rectTransform.localPosition = localPos;




        //////////Portrait 
        //Vector3 screenPos = mainCamera.WorldToScreenPoint(hand.position);


        //RectTransform canvasRect = canvas.transform as RectTransform;
        //float canvasWidth = canvasRect.rect.width;


        //RectTransformUtility.ScreenPointToLocalPointInRectangle(
        //    canvasRect, screenPos, mainCamera, out Vector2 localPos);


        //localPos.x = Mathf.Clamp(localPos.x, -canvasWidth / 2, canvasWidth / 2);


        //Vector2 currentPos = uiImage.rectTransform.localPosition;
        //uiImage.rectTransform.localPosition = new Vector2(localPos.x, currentPos.y);

        if (OneTime == true)
        {
            DetectTe();
        }

    }
    
    public void DetectTe()
    {
        
        if(detectedText.text == "Open Palm Down")
        {
            Debug.Log("open");
            DetectCanvas.SetActive(false);
            hands.SetActive(false);
            gameCanvas.SetActive(true);
            OneTime = false;
        }
    }
    
}
// two hands detection 


//// Get screen positions for both hands
//Vector3 leftHandScreenPos = mainCamera.WorldToScreenPoint(leftHand.position);
//Vector3 rightHandScreenPos = mainCamera.WorldToScreenPoint(rightHand.position);

//// Determine which hand is closer to the screen's center to follow
//float screenCenterX = Screen.width / 2;
//Vector3 chosenHandScreenPos = Mathf.Abs(leftHandScreenPos.x - screenCenterX) < Mathf.Abs(rightHandScreenPos.x - screenCenterX)
//    ? leftHandScreenPos
//    : rightHandScreenPos;

//// Map the chosen hand's X position to the Canvas range
//float mappedX = Map(chosenHandScreenPos.x, 400, 1100, 0, 1800);
//chosenHandScreenPos.x = mappedX;

//// Get Canvas dimensions
//RectTransform canvasRect = canvas.transform as RectTransform;
//float canvasWidth = canvasRect.rect.width;
//float canvasHeight = canvasRect.rect.height;

//// Convert screen position to Canvas local position
//RectTransformUtility.ScreenPointToLocalPointInRectangle(
//    canvasRect, chosenHandScreenPos, mainCamera, out Vector2 localPos);

//// Clamp X position within the Canvas bounds
//localPos.x = Mathf.Clamp(localPos.x, -canvasWidth / 2, canvasWidth / 2);
//localPos.y = -canvasHeight / 2 + 50; // Adjust this value for the desired height

//// Set UIImage position
//uiImage.rectTransform.localPosition = localPos;