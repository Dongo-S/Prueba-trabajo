using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class MemoramaManager : MonoBehaviour
{


    public bool sePuedeVoltear = true;
    public List<GameObject> cartas;
    public List<Sprite> spritesFrentes;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI tiempoText;
    public float tiempoRestante;
    int score = 0;

    [SerializeField, Range(2, 34)]
    int dificultad = 14;

    CartaMemorama carta1;
    CartaMemorama carta2;
    float deltaTime;
    [SerializeField]
    List<GameObject> cartasAsignadas;

    public void AgregarCartaVolteada(CartaMemorama carta)
    {
        if (sePuedeVoltear)
        {
            if (carta == carta1 || carta == carta2)
                return;

            if (carta1 == null)
                carta1 = carta;
            else if (carta2 == null)
            {
                carta2 = carta;
                sePuedeVoltear = false; //porque ya está asignado los dos
                Invoke("VerificarCartas", 1f);
            }

        }
       
    }


    void VerificarCartas()
    {
        if (carta1.frenteCarta == carta2.frenteCarta)
        {
            carta1.activada = true;
            carta1.GetComponent<Image>().sprite = carta1.frenteCarta;

            carta2.activada = true;
            carta2.GetComponent<Image>().sprite = carta2.frenteCarta;

            score++;
            scoreText.text = "Score: " + score;
            
        }
        else
        {
            carta1.RegresarCarta();
            carta2.RegresarCarta();
        }

        carta1 = null;
        carta2 = null;
        Invoke("sePuedeRotar",0.5f);
    }

    void sePuedeRotar()

    {
        sePuedeVoltear = true;
    }




    // Start is called before the first frame update
    void Start()
    {
        Random.InitState((int)System.DateTime.Now.Ticks);
        if (dificultad % 2 == 1)
        {
            dificultad++;
        }
        cartasAsignadas = new List<GameObject>();

        for (int i = 0; i < dificultad; i++)
        {
            cartas[i].SetActive(true);
            cartasAsignadas.Add(cartas[i]);
        }

        InicializarJuego();
    }



    void InicializarJuego()
    {
        List<Sprite> spritesTemp = new List<Sprite>(spritesFrentes);

        while (cartasAsignadas.Count > 0)
        {

            int r1 = Random.Range(0, cartasAsignadas.Count);
            int r2 = Random.Range(0, cartasAsignadas.Count);
            int r3 = Random.Range(0, spritesTemp.Count);

            while (r1 == r2)
            {
                r1 = Random.Range(0, cartasAsignadas.Count);
            }

            cartasAsignadas[r1].GetComponent<CartaMemorama>().SetCartaFrente(spritesTemp[r3]);
            cartasAsignadas[r2].GetComponent<CartaMemorama>().SetCartaFrente(spritesTemp[r3]);

            if (r1 > r2)
            {
                cartasAsignadas.RemoveAt(r1);
                cartasAsignadas.RemoveAt(r2);
            }
            else
            {
                cartasAsignadas.RemoveAt(r2);
                cartasAsignadas.RemoveAt(r1);              
            }
           
            spritesTemp.RemoveAt(r3);
         
        }
    }

    // Update is called once per frame
    void Update()
    {
        deltaTime += Time.deltaTime;

        if (deltaTime >= 0.2f)
        {
            tiempoRestante -= deltaTime;

            if (tiempoRestante <= 0)
            {
                //mensaje Perdiste

            }
            System.TimeSpan timeSpan = System.TimeSpan.FromSeconds(tiempoRestante);
               
            tiempoText.text = string.Format("Tiempo restante: {0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);
            deltaTime = 0f;
        }
    }
}
