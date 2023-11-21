using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GameJolt.UI;
using GameJolt.API;

public class Button : MonoBehaviour
{
    public void CambiarEscena(string nombreDeEscena)
    {
        SceneManager.LoadScene(nombreDeEscena);
    }

    public void MostrarTrofeos()
    {
        GameJoltUI.Instance.ShowTrophies();
    }
}
