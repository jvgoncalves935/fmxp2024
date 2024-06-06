using UnityEditor;
using UnityEngine;

public class PreloadController : MonoBehaviour
{
    void Start(){
        if(GerenciadorCena.NomeCenaAtual() != "_preload"){
            Destroy(gameObject);
            return;
        }

        ScenesData.InstanciaScenesData.AddScenesData("language", "en-US");

        SceneLoader.InstanciaSceneLoader.SetProximaCena("TelaInicial");
        GerenciadorCena.CarregarCena("TelaInicial");
    }
}
