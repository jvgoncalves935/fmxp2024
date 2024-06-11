using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TelaInicialController : MonoBehaviour{
    [SerializeField] private Image imagemKeyboardMouse;
    [SerializeField] private Image imagemPolybius;
    [SerializeField] private Image imagemPolybiusSeriesX;
    [SerializeField] private Image imagemCM;
    [SerializeField] private Image imagemDisclaimer;
    [SerializeField] private ScenesData scenesData;
    [SerializeField] private InputNames inputNames;
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private TMP_Text textTelaInicial;
    public Dictionary<string, string> stringsTelaInicial;
    private string mensagem;
    private string bufferMensagem;
    private int tamanhoMensagem;
    private int tamanhoBufferMensagem;

    private void Awake() {
        
    }

    void Start()
    {
        VerificarScenesDataInstanciado();
        //FocarMouse();
        //VerificarVideoPreload();
        StartCoroutine(IniciarCutscene());
        textTelaInicial.gameObject.SetActive(false);
        imagemKeyboardMouse.gameObject.SetActive(false);
    }

    private IEnumerator IniciarCutscene(){
        yield return new WaitForSeconds(1.3f);
        KeyboardMouse();
        yield return new WaitForSeconds(1.0f);

        PlayMusic("Polybius Series X1");
        StartCoroutine(FadeIn(imagemPolybius, 0.4f));
        yield return new WaitForSeconds(3.5f);
        StartCoroutine(FadeOut(imagemPolybius, 0.3f));
        StartCoroutine(FadeIn(imagemPolybiusSeriesX, 0.3f));
        yield return new WaitForSeconds(4.8f);

        StopMusic("Polybius Series X1");
        imagemPolybiusSeriesX.color = new Color(1, 1, 1, 0);
        yield return new WaitForSeconds(0.1f);
        VaporSnakeLog();
        yield return new WaitForSeconds(2.5f);

        
        yield return new WaitForSeconds(0.8f);
        PlayMusic("R.E. (full)");
        StartCoroutine(FadeIn(imagemCM, 1.2f));
        yield return new WaitForSeconds(1.2f);
        yield return new WaitForSeconds(3.0f);
        StartCoroutine(FadeOut(imagemCM, 0.8f));
        yield return new WaitForSeconds(1.5f);

        imagemDisclaimer.color = new Color(1, 1, 1, 1);
        yield return new WaitForSeconds(4.0f);
        

        SceneLoader.InstanciaSceneLoader.SetProximaCena("MenuPrincipal");
        SceneLoader.InstanciaSceneLoader.SetStopMusicOnLoading(true);
        GerenciadorCena.CarregarCena("Loading");
    }

    private void KeyboardMouse() {
        StartCoroutine(KeyboardMouseCoroutine());
    }

    private IEnumerator KeyboardMouseCoroutine() {
        imagemKeyboardMouse.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.8f);
        imagemKeyboardMouse.gameObject.SetActive(false);
    }

    private void VaporSnakeLog() {
        StartCoroutine(VaporSnakeLogCoroutine());
    }

    private IEnumerator VaporSnakeLogCoroutine() {
        textTelaInicial.gameObject.SetActive(true);
        yield return new WaitForSeconds(2.2f);
        textTelaInicial.gameObject.SetActive(false);

    }

    private IEnumerator FadeIn(Image imagem, float tempoFinal){
        float tempo;
        for(tempo = 0.0f;tempo <= tempoFinal;tempo += Time.deltaTime) {
            imagem.color = Color.Lerp(new Color(1, 1, 1, 0), new Color(1, 1, 1, 1), tempo / tempoFinal);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        imagem.color = new Color(1, 1, 1, 1);
    }

    private IEnumerator FadeOut(Image imagem, float tempoFinal) {
        float tempo;
        for(tempo = tempoFinal;tempo >= 0.0f;tempo -= Time.deltaTime) {
            imagem.color = Color.Lerp(new Color(1, 1, 1, 0), new Color(1, 1, 1, 1), tempo / tempoFinal);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        imagem.color = new Color(1, 1, 1, 0);
    }

    public void VerificarScenesDataInstanciado() {
        if(FindObjectOfType<ScenesData>() == null) {
            Instantiate(scenesData);
            scenesData = ScenesData.InstanciaScenesData;

            Instantiate(inputNames);
            inputNames = InputNames.InstanciaInputNames;

            Instantiate(sceneLoader);
            sceneLoader = SceneLoader.InstanciaSceneLoader;

            Instantiate(audioManager);
        }
    }

    private void FocarMouse() {
    #if UNITY_EDITOR || UNITY_STANDALONE_WIN
            MouseOperations.FocarMouseMultiPlat();
    #endif
    }

    private void VerificarVideoPreload() {
        if(!ConquistaController.GetVideoPreloadAtivado()) {
            StartCoroutine(IniciarCutscene());
        } else {
            StartCoroutine(IniciarVideoPreload());
        }
    }

    private IEnumerator IniciarVideoPreload() {
        IniciarMensagem();
        while(tamanhoBufferMensagem < tamanhoMensagem) {
            AvancarMensagem();
            yield return new WaitForSeconds(Random.Range(0.05f,0.15f));
        }
        yield return new WaitForSeconds(3f);

        ConquistaController.DesativarVideoPreload();
        FecharJogo();
    }

    private void IniciarMensagem() {
        stringsTelaInicial = LocalizationSystem.GetDicionarioStringsCena("TelaInicial");
        mensagem = stringsTelaInicial["TELA_INICIAL_MENSAGEM"];
        tamanhoMensagem = mensagem.Length;
        bufferMensagem = "";
        tamanhoBufferMensagem = 0;
    }

    private void AvancarMensagem() {
        bufferMensagem += mensagem[tamanhoBufferMensagem];
        tamanhoBufferMensagem += 1;
        textTelaInicial.text = bufferMensagem;
    }

    private void FecharJogo() {
    #if UNITY_EDITOR || UNITY_STANDALONE_WIN
            FecharJogoWIN();
    #endif
    }

    #if UNITY_EDITOR || UNITY_STANDALONE_WIN
    private void FecharJogoWIN() {
        Application.Quit();
    }
    #endif

    public void VerificarSceneLoaderInstanciado() {
        if(FindObjectOfType<SceneLoader>() == null) {
            Instantiate(sceneLoader);
            Instantiate(audioManager);
            //DontDestroyOnLoad(sceneLoader);
            //Debug.Log("SceneData criado em EventHorizon");
        } else {
            //Debug.Log("SceneData anteriormente criado");
        }
    }

    public void PlayMusic(string music) {
        AudioManager.InstanciaAudioManager.Play(music);
    }

    private void StopMusic(string music) {
        AudioManager.InstanciaAudioManager.Stop(music);
    }

}
