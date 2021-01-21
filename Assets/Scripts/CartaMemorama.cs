using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CartaMemorama : MonoBehaviour, IPointerClickHandler
{

    public Sprite contraCarta;
    public Sprite frenteCarta;

    public bool activada = false;

    public MemoramaManager manager;
    public float tiempoAnimacion = 1f;


    public void OnPointerClick(PointerEventData eventData)
    {

        if (activada)
            return;

        if (LeanTween.isTweening(gameObject))
            return;

        if (manager.sePuedeVoltear)
        {
            //voltear Carta
            manager.AgregarCartaVolteada(this);
            rotar(tiempoAnimacion);
        }
    }


    public void SetCartaFrente(Sprite sprite)

    {
        frenteCarta = sprite;
    }

    void rotar(float time)
    {
        LeanTween.rotateY(gameObject, gameObject.transform.eulerAngles.y + 180, time).setOnUpdate((float r) =>
        {
            float y = transform.rotation.eulerAngles.y;
            if (y == -180f || y == 180f || y == 0)
            {
                if (transform.GetComponent<Image>().sprite == frenteCarta)
                {
                    transform.GetComponent<Image>().sprite = contraCarta;
                }
                else
                    transform.GetComponent<Image>().sprite = frenteCarta;
            }

        });
    }

    public void RegresarCarta()
    {
        rotar(0.5f);
    }
    public void VoltearCarta()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Image>().sprite = contraCarta;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
