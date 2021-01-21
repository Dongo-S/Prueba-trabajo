using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RequestManager : MonoBehaviour
{

    string usuario;
    string contraseña;
    public string url;
    public GameObject mensajeError;
    public GameObject loginPanel;
    public GameObject botonesPanel;

    public void SetUsuario(string user)
    {
        usuario = user;
    }

    public void SetContrasena(string password)
    {
        contraseña = password;
    }



    public void SolicitarLogin()
    {
        StartCoroutine(Login());
    }



    private IEnumerator Login()
    {
        UnityWebRequest req = UnityWebRequest.Get(url);

        yield return req.SendWebRequest();


        if (req.isNetworkError || req.isHttpError)
        {
            

            Debug.Log(req.error);
        }
        else
        {

            DownloadHandler handler = req.downloadHandler;
            string text = "{\n \"user\": \""+ usuario+ "\",\n \"password\": \"" + contraseña + "\"\n }\n";
            if (text == handler.text)
            {
                //pasar a la siguiente escena
                loginPanel.SetActive(false);
                botonesPanel.SetActive(true);
            }
            else
            {
                mensajeError.SetActive(true);
                Debug.Log("Usuario o Contraseña incorrecto");
            }
        }
    }
    public void Salir()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
