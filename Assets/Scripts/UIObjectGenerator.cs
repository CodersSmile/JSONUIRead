using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AdaptivePerformance;
using UnityEngine.UI;
using static UnityEngine.UIElements.VisualElement;

public class UIObjectGenerator : MonoBehaviour
{
    public TextAsset uiObjectsJSON;
    public GameObject canvas;
    public Font customFont;
    public Sprite customImage;
    public Sprite FilledStarSprite;
    public Sprite UnfilledStarSprite;
    public Sprite ButtonSprite;
  


    private void Start()
    {
        UIObjectData uiData = JsonUtility.FromJson<UIObjectData>(uiObjectsJSON.text);






        foreach (UIObject obj in uiData.objects)
        {
            if (obj.name == "BG")
            {
                RawImage bgImage = new GameObject("BG").AddComponent<RawImage>();
                bgImage.transform.SetParent(canvas.transform, false);

                foreach (UIComponent component in obj.components)
                {
                    if (component.type == "RawImage")
                    {
                        bgImage.color = new Color(
                            component.properties.color.r,
                            component.properties.color.g,
                            component.properties.color.b,
                            component.properties.color.a
                        );

                        if (component.properties.cornerRadius > 0)
                        {
                            RectTransform rectTransform = bgImage.GetComponent<RectTransform>();
                            rectTransform.GetComponent<Image>().maskable = true;
                            rectTransform.GetComponent<Image>().type = Image.Type.Sliced;
                            rectTransform.GetComponent<Image>().fillCenter = true;
                            rectTransform.GetComponent<Image>().sprite = null;
                            rectTransform.GetComponent<Image>().raycastTarget = false;
                            rectTransform.GetComponent<Image>().pixelsPerUnitMultiplier = 1.0f;
                            rectTransform.GetComponent<Image>().fillMethod = Image.FillMethod.Vertical;
                            rectTransform.GetComponent<Image>().fillOrigin = (int)Image.OriginVertical.Top;
                            rectTransform.GetComponent<Image>().fillAmount = 1;
                            rectTransform.GetComponent<Image>().overrideSprite = null;

                            rectTransform.GetComponent<Image>().SetAllDirty();
                            rectTransform.GetComponent<Image>().material = new Material(Shader.Find("UI/Default"));
                            rectTransform.GetComponent<Image>().material.SetFloat("_CornerRadius", component.properties.cornerRadius);
                        }

                        RectTransform bgRectTransform = bgImage.GetComponent<RectTransform>();
                        bgRectTransform.sizeDelta = new Vector2(1500.0f, 800.0f);


                       
                    }
                }
            }
            else if (obj.name == "Placeholder")
            {
                RawImage placeholderImage = new GameObject("Placeholder").AddComponent<RawImage>();
                placeholderImage.transform.SetParent(canvas.transform, false);

                foreach (UIComponent component in obj.components)
                {
                    if (component.type == "RawImage")
                    {
                        placeholderImage.color = Color.white;
                        RectTransform placeholderRectTransform = placeholderImage.GetComponent<RectTransform>();
                        placeholderRectTransform.sizeDelta = new Vector2(1460.0f, 760.0f);

                        if (component.properties.cornerRadius > 0)
                        {
                            placeholderImage.material = new Material(Shader.Find("UI/Default"));
                            placeholderImage.material.SetFloat("_CornerRadius", component.properties.cornerRadius);
                        }
                    }
                }
            }
      
            else if (obj.name == "SampleFormat")
            {
                GameObject sampleFormatObject = new GameObject("SampleFormat");
                sampleFormatObject.transform.SetParent(canvas.transform, false);
              
                foreach (UIComponent component in obj.components)
                {
                    if (component.type == "GameObject" && component.properties.name == "Holder")
                    {
                        GameObject holderObject = new GameObject("Holder");
                        holderObject.transform.SetParent(sampleFormatObject.transform, false);
                        RectTransform holderectTransform = holderObject.AddComponent<RectTransform>();
                        holderectTransform.sizeDelta = new Vector2(1450.0f, 750.0f);

                        
                        GameObject buttonObject = new GameObject("adTag");
                        buttonObject.transform.SetParent(holderObject.transform, false);
                        Button buttonComponent = buttonObject.AddComponent<Button>();
                        Image buttonImage = buttonObject.AddComponent<Image>();

                        RectTransform buttonRectTransform = buttonObject.GetComponent<RectTransform>();
                        buttonRectTransform.sizeDelta = new Vector2(80.0f, 60.0f);

                        buttonImage.color = new Color(1.0f, 0.647f, 0.0f, 1.0f);

                        buttonRectTransform.anchorMin = new Vector2(0, 1); 
                        buttonRectTransform.anchorMax = new Vector2(0, 1);
                        buttonRectTransform.pivot = new Vector2(0, 1);    
                        buttonRectTransform.anchoredPosition = Vector2.zero;
                        GameObject adTagObject = new GameObject("adTagText");
                        adTagObject.transform.SetParent(buttonObject.transform, false);

                        Text adTagText = adTagObject.AddComponent<Text>();
                        if (customFont != null)
                        {
                            adTagText.font = customFont;
                        }
                        else
                        {
                            Debug.LogError("Custom font or Text component not assigned.");
                        }
                        adTagText.text = "A D";
                        adTagText.color = Color.black;
                        adTagText.fontStyle = FontStyle.Bold;
                        adTagText.fontSize = 24;
                        adTagText.alignment = TextAnchor.MiddleCenter;


                        RawImage adChoicesIconObject = new GameObject("RawImage_adchoicesIcon").AddComponent<RawImage>(); ;
                        adChoicesIconObject.transform.SetParent(holderObject.transform, false);
                        RectTransform adChoicesIconRectTransform = adChoicesIconObject.GetComponent<RectTransform>();
                        adChoicesIconRectTransform.anchoredPosition = new Vector2(70.0f, -100.0f);
                        adChoicesIconRectTransform.anchorMin = new Vector2(0, 1);
                        adChoicesIconRectTransform.anchorMax = new Vector2(0, 1);
                        adChoicesIconRectTransform.pivot = new Vector2(0, 1);
                        adChoicesIconRectTransform.sizeDelta = new Vector2(200.0f, 200.0f);


                        /* GameObject adChoicesIconObject = new GameObject("RawImage_adchoicesIcon");
                         adChoicesIconObject.transform.SetParent(adChoicesIconObject.transform, false);
                         Image testAdImage = adChoicesIconObject.AddComponent<Image>();
                         testAdImage.sprite = customImage; 
                         RectTransform adChoicesIconRectTransform = adChoicesIconObject.GetComponent<RectTransform>();
                         adChoicesIconRectTransform.anchoredPosition = new Vector2(70.0f, -100.0f);
                         adChoicesIconRectTransform.anchorMin = new Vector2(0, 1);
                         adChoicesIconRectTransform.anchorMax = new Vector2(0, 1);
                         adChoicesIconRectTransform.pivot = new Vector2(0, 1);
                         adChoicesIconRectTransform.sizeDelta = new Vector2(200.0f, 200.0f);*/




                        GameObject paddingObject = new GameObject("Padding");
                        paddingObject.transform.SetParent(holderObject.transform, false);
                        RectTransform paddingRectTransform = paddingObject.AddComponent<RectTransform>();
                        paddingRectTransform.anchoredPosition = new Vector2(10.0f, -100.0f);
                        paddingRectTransform.anchorMin = new Vector2(0, 1);
                        paddingRectTransform.anchorMax = new Vector2(0, 1);
                        paddingRectTransform.pivot = new Vector2(0, 1); 
                        paddingRectTransform.sizeDelta = new Vector2(1200.0f, 100.0f); 

                        GameObject appDetailsObject = new GameObject("Appdetails");
                        appDetailsObject.transform.SetParent(paddingObject.transform, false);
                        RectTransform appDetailsRectTransform = appDetailsObject.AddComponent<RectTransform>();
                        appDetailsRectTransform.anchoredPosition = new Vector2(300.0f, -10.0f);
                        appDetailsRectTransform.anchorMin = Vector2.zero;
                        appDetailsRectTransform.anchorMax = Vector2.zero;
                        appDetailsRectTransform.pivot = Vector2.zero;
                        Button appDetailsButton = appDetailsObject.AddComponent<Button>();
                        appDetailsButton.GetComponent<RectTransform>().sizeDelta = new Vector2(1000, 80);

                        RawImage rawImageIconObject = new GameObject("RawImage_adIcon").AddComponent<RawImage>();
                        rawImageIconObject.transform.SetParent(appDetailsObject.transform, false);
                        RectTransform rawRectTransform = rawImageIconObject.GetComponent<RectTransform>();
                        rawRectTransform.anchoredPosition = new Vector2(70.0f, -100.0f);
                        rawRectTransform.anchorMin = new Vector2(0, 1);
                        rawRectTransform.anchorMax = new Vector2(0, 1);
                        rawRectTransform.pivot = new Vector2(0, 1);
                        rawRectTransform.sizeDelta = new Vector2(200.0f, 200.0f);
                        if (customImage != null)
                        {
                            adChoicesIconObject.texture = customImage.texture;
                        }
                        else
                        {
                            Debug.LogError("Custom image not assigned.");
                        }


                        GameObject appInfoObject = new GameObject("AppInfo");
                        appInfoObject.transform.SetParent(appDetailsObject.transform, false);
                        RectTransform appInfoRectTransform = appInfoObject.AddComponent<RectTransform>();
                        appInfoRectTransform.anchoredPosition = new Vector2(0.0f, 0.0f);
                        appInfoRectTransform.anchorMin = new Vector2(0, 0); 
                        appInfoRectTransform.anchorMax = new Vector2(0, 0); 
                        appInfoRectTransform.pivot = new Vector2(0, 0);    
                        Button appInfoButton = appInfoObject.AddComponent<Button>();
                        appInfoButton.GetComponent<RectTransform>().sizeDelta = new Vector2(1000, 200); 
                        Text buttonText = new GameObject("Text_adHeadline").AddComponent<Text>();
                        buttonText.transform.SetParent(appInfoButton.transform, false);
                        buttonText.text = "Test Ad: Flood It!";
                        buttonText.font = customFont; 
                        buttonText.fontSize = 50;
                        buttonText.color = Color.black; 
                        buttonText.alignment = TextAnchor.MiddleLeft;
                        buttonText.GetComponent<RectTransform>().sizeDelta = new Vector2(1000, 80);


                        GameObject starRatingObject = new GameObject("StarRating");
                        starRatingObject.transform.SetParent(appInfoButton.transform, false);
                        RectTransform starRatingRectTransform = starRatingObject.AddComponent<RectTransform>();
                        starRatingRectTransform.anchoredPosition = new Vector2(0.0f, -20.0f);
                        starRatingRectTransform.anchorMin = new Vector2(0, 0);
                        starRatingRectTransform.anchorMax = new Vector2(0, 0);
                        starRatingRectTransform.pivot = new Vector2(0, 0);
                        Button starRateInfoButton = starRatingObject.AddComponent<Button>();
                        starRateInfoButton.GetComponent<RectTransform>().sizeDelta = new Vector2(300, 60);

                       
                        GameObject filledStarObject = new GameObject("FilledStar");
                        filledStarObject.transform.SetParent(starRatingObject.transform, false);
                        Image filledStarImage = filledStarObject.AddComponent<Image>();
                        filledStarImage.sprite = FilledStarSprite; 
                        filledStarImage.color = Color.yellow; 
                        RectTransform filledStarRectTransform = filledStarObject.GetComponent<RectTransform>();
                        filledStarRectTransform.anchoredPosition = Vector2.zero;
                        filledStarRectTransform.sizeDelta = new Vector2(300, 60);

                       
                        GameObject unfilledStarObject = new GameObject("UnfilledStar");
                        unfilledStarObject.transform.SetParent(starRatingObject.transform, false);
                        Image unfilledStarImage = unfilledStarObject.AddComponent<Image>();
                        unfilledStarImage.sprite = UnfilledStarSprite; 
                        unfilledStarImage.color = Color.gray; 
                        RectTransform unfilledStarRectTransform = unfilledStarObject.GetComponent<RectTransform>();
                        unfilledStarRectTransform.anchoredPosition = Vector2.zero;
                        unfilledStarRectTransform.sizeDelta = new Vector2(300, 60);


                        Text storeText = new GameObject("Text_store").AddComponent<Text>();
                        storeText.transform.SetParent(appInfoButton.transform, false);
                        RectTransform textStoreRect = storeText.GetComponent<RectTransform>();
                        textStoreRect.anchoredPosition = new Vector2(0.0f, -160.0f);
                        textStoreRect.sizeDelta = new Vector2(1000, 40);


                        Text priceText = new GameObject("priceText").AddComponent<Text>();
                        priceText.transform.SetParent(appInfoButton.transform, false);
                        priceText.text = "FREE";
                        priceText.font = customFont;
                        priceText.fontSize = 30;
                        priceText.color = Color.gray;
                        priceText.alignment = TextAnchor.MiddleLeft;
                        priceText.GetComponent<RectTransform>().sizeDelta = new Vector2(1000, 50);
                        RectTransform priceTextRect = priceText.GetComponent<RectTransform>();
                        priceTextRect.anchoredPosition = new Vector2(0.0f, -160.0f);
                        priceTextRect.sizeDelta = new Vector2(1000, 50);


                        Text installText = new GameObject("Text_body").AddComponent<Text>();
                        installText.transform.SetParent(paddingObject.transform, false);
                        installText.text = "Install Flood It app for free! Free popular Casual Game";
                        installText.font = customFont;
                        installText.fontSize = 70;
                        installText.color = Color.gray;
                        installText.alignment = TextAnchor.LowerLeft;
                        installText.GetComponent<RectTransform>().sizeDelta = new Vector2(1300, 200);
                        RectTransform installTextRect = installText.GetComponent<RectTransform>();
                        installTextRect.anchoredPosition = new Vector2(70, -250);
                        installTextRect.sizeDelta = new Vector2(1300, 200);



                        Button buttonCtaObject = new GameObject("Button_Cta").AddComponent<Button>();
                        buttonCtaObject.transform.SetParent(paddingObject.transform, false);
                        Button buttonCta = buttonCtaObject.AddComponent<Button>();
                        Image buttonCtaImage = buttonCtaObject.AddComponent<Image>();
                        RectTransform buttonCtaRectTransform = buttonCtaObject.GetComponent<RectTransform>();
                        buttonCtaRectTransform.anchoredPosition = new Vector2(70, -450);
                        buttonCtaRectTransform.sizeDelta = new Vector2(1200, 150);
                        buttonCtaImage.color = new Color(0, 1, 1, 1);
                        Text buttonCtaText = new GameObject("Text_CallToAction").AddComponent<Text>();
                        buttonCtaText.transform.SetParent(buttonCtaObject.transform, false);
                        buttonCtaText.text = "INSTALL"; 
                        buttonCtaText.font = customFont;
                        buttonCtaText.fontSize = 80;
                        buttonCtaText.color = Color.white;
                        buttonCtaText.alignment = TextAnchor.MiddleCenter;
                        RectTransform buttonTextRect = buttonCtaText.GetComponent<RectTransform>();
                        buttonTextRect.anchoredPosition = new Vector2(0, 0); // Adjust the position as needed
                        buttonTextRect.sizeDelta = new Vector2(1200, 150);



                        RawImage adChoicesIconRawImage = adChoicesIconObject.AddComponent<RawImage>();
                        Sprite adChoicesIconSprite = Resources.Load<Sprite>("testAd");

                        adChoicesIconRawImage.texture = adChoicesIconSprite.texture;


                      

                    }
                }
                RectTransform sampleFormatRectTransform = sampleFormatObject.AddComponent<RectTransform>();
                sampleFormatRectTransform.sizeDelta = new Vector2(1450.0f, 750.0f); // Set the size to your desired values

             

            }
        }
    }

  
}