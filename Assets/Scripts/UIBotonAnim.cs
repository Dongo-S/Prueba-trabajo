using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIBotonAnim : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    RectTransform rect;

    public bool fadeOnClick;
    public bool activarOnEnable;

    int id_ScaleZ;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (fadeOnClick)
        {
            LeanTween.scale(rect, Vector2.one * 1.5f, 0.5f);
            LeanTween.alpha(rect, 0f, 0.5f).setOnComplete(() =>
           {
               Color c = GetComponent<Image>().material.color;
               c.a = 1f;
               rect.GetComponent<Image>().material.color = c;
           });
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        gameObject.transform.localScale = Vector2.one;
        id_ScaleZ = LeanTween.scale(rect, Vector2.one * 1.1f, 0.5f).setEase(LeanTweenType.easeInOutSine).setLoopPingPong().id;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(LeanTween.isTweening(id_ScaleZ))
        {
            LeanTween.cancel(id_ScaleZ);
        }
        LeanTween.scale(rect, Vector2.one, 0.2f);
    }

    void OnEnable()
    {

        rect.localScale= Vector2.zero;
        LeanTween.scale(rect, Vector2.one, 0.2f);
        LeanTween.alpha(rect, 1f, 0.2f);
        
    }

    void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
