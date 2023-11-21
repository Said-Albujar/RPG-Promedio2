using GameJolt.UI;
using GameJolt.API;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameJoltUI.Instance.ShowSignIn((signInSuccess) =>
        {
            if (signInSuccess)
            {
                GetFirstTrophy();
                Debug.Log("Se logueó con éxito");
            }
            else
            {
                Debug.Log("No se logueó");
            }
        });
    }

    private void GetFirstTrophy()
    {
        Trophies.TryUnlock(215932);
    }
}
