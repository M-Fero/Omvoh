using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using CodeMonkey.Utils;
using Leap.Controllers;

public class SolarPanel : MonoBehaviour
{


    [SerializeField] private Texture2D dirtMaskTextureBase;
    [SerializeField] private Texture2D dirtBrush;
    [SerializeField] private Material material;
    //[SerializeField] private TextMeshProUGUI uiText;
    [SerializeField] private int dirtBrushWidth;
    [SerializeField] private int dirtBrushHeight;
    [SerializeField] private Controller controller;
    //[SerializeField] private string dirtMask;

    public Canvas canvas;
    public Image theVImage;
    public RectTransform uiRectTransformV;

    private Texture2D dirtMaskTexture;
    private float dirtAmountTotal;
    private float dirtAmount;
    private Vector2Int lastPaintPixelPosition;

    private void Awake()
    {
        dirtMaskTexture = new Texture2D(dirtMaskTextureBase.width, dirtMaskTextureBase.height);
        dirtMaskTexture.SetPixels(dirtMaskTextureBase.GetPixels());
        dirtMaskTexture.Apply();
        material.SetTexture("_DirtMask", dirtMaskTexture);


        dirtAmountTotal = 0f;
        for (int x = 0; x < dirtMaskTextureBase.width; x++)
        {
            for (int y = 0; y < dirtMaskTextureBase.height; y++)
            {
                dirtAmountTotal += dirtMaskTextureBase.GetPixel(x, y).g;
            }
        }
        dirtAmount = dirtAmountTotal;
    }
    float Map(float value, float inMin, float inMax, float outMin, float outMax)
    {
        return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }

    private void Update()
    {
        if (controller.hand == null) return;


        Vector3 screenPoint = Camera.main.WorldToViewportPoint(controller.hand.position);
        Debug.Log("Screen Point Before: " + screenPoint);
        //float xx = Map(screenPoint.x, 795, 1235, 0, 1800);
        if (controller.hand == null) return;

        // Map the viewport coordinates to screen pixel coordinates
        // For X: Map from 0-1 (viewport) to 795-1235 (target area x bounds)
        float mappedX = Map(screenPoint.x, 0.4f, 1f, 500, -500);

        // For Y: Map from 0-1 (viewport) to 40-561 (target area y bounds)
        // Swapped the min/max values to correct the mapping
        float mappedY = Map(screenPoint.y, -6.0f, -5.0f, -680, 180);

        // Create new screen point with mapped coordinates
        screenPoint = new Vector3(-mappedX, mappedY, screenPoint.z);

        // Get the hand position in world space
        Vector3 handWorldPosition = controller.hand.position;

        // Convert the world position to screen space
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(handWorldPosition);

        // Convert the screen position to Canvas local position
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.GetComponent<RectTransform>(),
            screenPosition,
            canvas.worldCamera,
            out Vector2 canvasLocalPosition
        );

        // Update the position of theVImage
        uiRectTransformV.anchoredPosition = canvasLocalPosition;
        theVImage.transform.localPosition = screenPoint;

        //Debug.Log("Hand Position: " + handWorldPosition + " | Canvas Position: " + canvasLocalPosition);

        // Perform a raycast from the image's position
        Ray ray = Camera.main.ScreenPointToRay(theVImage.transform.position);
        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            Debug.Log("Raycast hit: " + raycastHit.collider.name);
            // Store the hit information or perform additional logic here


            Vector2 textureCoord = raycastHit.textureCoord;
            int pixelX = (int)(textureCoord.x * dirtMaskTexture.width);
            int pixelY = (int)(textureCoord.y * dirtMaskTexture.height);

            Vector2Int paintPixelPosition = new Vector2Int(pixelX, pixelY);
            //Debug.Log("UV: " + textureCoord + "; Pixels: " + paintPixelPosition);

            int paintPixelDistance = Mathf.Abs(paintPixelPosition.x - lastPaintPixelPosition.x) + Mathf.Abs(paintPixelPosition.y - lastPaintPixelPosition.y);
            int maxPaintDistance = 7;
            if (paintPixelDistance < maxPaintDistance)
            {
                // Painting too close to last position
                return;
            }
            lastPaintPixelPosition = paintPixelPosition;
            int pixelXOffset = pixelX - (dirtBrush.width / 2);
            int pixelYOffset = pixelY - (dirtBrush.height / 2);

            for (int x = 0; x < dirtBrush.width; x++)
            {
                for (int y = 0; y < dirtBrush.height; y++)
                {
                    // Calculate normalized coordinates for smoother sampling of the brush
                    float u = (x + 0.5f) / dirtBrush.width;
                    float v = (y + 0.5f) / dirtBrush.height;
                    Color pixelDirt = dirtBrush.GetPixelBilinear(u, v);
                    Color pixelDirtMask = dirtMaskTexture.GetPixel(pixelXOffset + x, pixelYOffset + y);

                    float removedAmount = pixelDirtMask.g - (pixelDirtMask.g * pixelDirt.g);
                    dirtAmount -= removedAmount;

                    dirtMaskTexture.SetPixel(
                        pixelXOffset + x,
                        pixelYOffset + y,
                        new Color(0f, pixelDirtMask.g * pixelDirt.g, 0f)
                    );
                }
            }
            dirtMaskTexture.Apply();
        }
    }

    private float GetDirtAmount()
    {
        return this.dirtAmount / dirtAmountTotal;
    }
}
