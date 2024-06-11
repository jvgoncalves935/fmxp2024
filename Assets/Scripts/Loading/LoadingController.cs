using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingController : MonoBehaviour
{
    private bool primeiroUpdate;
    private AsyncOperation operacao;
    [SerializeField] private Image imageLoading;
    [SerializeField] private Sprite spritePetsfarm;
    [SerializeField] private Sprite spritePawsville;
    [SerializeField] private Sprite spriteMundanescape;
    [SerializeField] private Sprite spriteFuturescape;
    [SerializeField] private SceneLoader sceneLoader;

    private string proximaCena;
    private bool isLoadingFinished = false;
    private bool isNotLevelScene = false;

    void Start()
    {
        primeiroUpdate = true;
        VerificarSceneLoaderInstanciado();
        PararMusica();
        GetNextScene();
        CheckCurrentLoadingScreen();
        DelaySparkyRushLoading();
        //TravarCursor();
    }

    private void Update() {
        if(primeiroUpdate){
            primeiroUpdate = false;
            StartCoroutine(CarregarCena());
            FocarMouse();
        }
        LogoPulsando();
    }

    private IEnumerator CarregarCena(){
        

        int indiceProximaCena = SceneLoader.InstanciaSceneLoader.GetIndiceProximaCena(proximaCena);

        operacao = SceneManager.LoadSceneAsync(indiceProximaCena,LoadSceneMode.Single);
        //operacao.completed += (op) => PosLoadingCoroutine();
        
        operacao.allowSceneActivation = isNotLevelScene;

        //decimal progresso;

        while(!(operacao.progress >= 0.9f)){
            //progresso = (decimal) (Mathf.Clamp01(operacao.progress / 0.9f) * 100);
            //textProgresso.text =  progresso.ToString("#.##")+ "%";
            yield return null;
        }
    }

    private void PosLoading() {
        SceneLoader.InstanciaSceneLoader.SetProximaCena("");
        operacao.allowSceneActivation = true;
    }

    private void LogoPulsando(){
        float valor = Mathf.Abs(0.5f + Mathf.Sin(Time.time * 2.0f)/2.0f);
        //Debug.Log(valor);
        //imagemEH.color = new Color(1.0f, 1.0f, 1.0f, valor);
    }

    private void FocarMouse() {
    #if UNITY_EDITOR || UNITY_STANDALONE_WIN
        MouseOperations.FocarMouseMultiPlat();
    #endif
    }

    public void VerificarSceneLoaderInstanciado() {
        if(FindObjectOfType<SceneLoader>() == null) {
            Instantiate(sceneLoader);
            DontDestroyOnLoad(sceneLoader);
            
        }
    }

    private void PararMusica() {
        if(SceneLoader.InstanciaSceneLoader.ShouldStopMusicOnLoading()) {
            AudioManager.InstanciaAudioManager.StopMusicaAtual();
        }
    }

    private void CheckCurrentLoadingScreen() {
        switch(proximaCena) {
            case "Petsfarm": {
                imageLoading.sprite = spritePetsfarm;
                break;
            }
            case "Pawsville": {
                imageLoading.sprite = spritePawsville;
                break;
            }
            case "Mundanescape": {
                imageLoading.sprite = spriteMundanescape;
                break;
            }
            case "Futurescape": {
                imageLoading.sprite = spriteFuturescape;
                break;
            }
            default: {
                isNotLevelScene = true;
                break;
            }

        }
    }

    private void DelaySparkyRushLoading() {
        if(isNotLevelScene) {
            return;
        }
        StartCoroutine(DelaySparkyRushLoadingCoroutine());
    }

    private IEnumerator DelaySparkyRushLoadingCoroutine() {
        //Debug.Log("Fade In");
        ScreenUtils.Instance.FadeInImage(imageLoading, 1.0f);

        yield return new WaitForSeconds(4.0f);

        while(!(operacao.progress >= 0.9f)) {
            yield return new WaitForSeconds(0.5f);
        }

        //Debug.Log("Fade Out");
        ScreenUtils.Instance.FadeOutImage(imageLoading, 1.0f);
        yield return new WaitForSeconds(1.3f);

        PosLoading();
    }

    private void GetNextScene() {
        proximaCena = SceneLoader.InstanciaSceneLoader.GetProximaCena();
    }

    public static void TravarCursor() {
        Cursor.lockState = CursorLockMode.Locked;
        //Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;
    }

    public static void DestravarCursor() {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
