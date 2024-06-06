using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CutsceneInicialController : MonoBehaviour
{
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private TMP_Text textoUI;
    private int indiceTextoAtual;
    [SerializeField] private Image[] imagensCutscene;

    private Dictionary<string, string> stringsCutscene;
    private Dictionary<string, string> stringsPersonagensCutsceneInicial;

    private static int NUM_IMAGENS = 6;

    private string[] textosCutscenes;

    [SerializeField] public static GameObject instanciaCutsceneInicialController;
    private static CutsceneInicialController _instanciaCutsceneInicialController;
    public static CutsceneInicialController InstanciaCutsceneInicialController {
        get {
            if(_instanciaCutsceneInicialController == null) {
                _instanciaCutsceneInicialController = instanciaCutsceneInicialController.GetComponent<CutsceneInicialController>();
            }
            return _instanciaCutsceneInicialController;
        }
    }
    void Awake() {
        instanciaCutsceneInicialController = FindObjectOfType<CutsceneInicialController>().gameObject;
    }
    // Start is called before the first frame update
    void Start()
    {
        indiceTextoAtual = 0;
        StartCoroutine(CutsceneInicial());
        VerificarSceneLoaderInstanciado();
        MusicaInicio();
        IniciarCoresImagens();
        CarregarStrings();
        AplicarStrings();
    }

    // Update is called once per frame
    void Update()
    {
        CheckSkipCutscene();
    }
    private IEnumerator CutsceneInicial() {
        yield return new WaitForSeconds(1f);

        //Imagem 1
        StartCoroutine(FadeIn(imagensCutscene[0], 0.5f));
        yield return new WaitForSeconds(0.5f);
        
        SetText(textosCutscenes[0]);
        yield return StartCoroutine(SkippableCutscenes.InstanciaSkippableCutscenes.WaitForSecondsCancelavel(4f));

        StartCoroutine(FadeOut(imagensCutscene[0], 0.5f));
        yield return new WaitForSeconds(0.5f);
        SetText("");
        yield return new WaitForSeconds(0.5f);


        //Imagem 2
        StartCoroutine(FadeIn(imagensCutscene[1], 0.5f));
        yield return new WaitForSeconds(0.5f);

        SetText(textosCutscenes[1]);
        yield return StartCoroutine(SkippableCutscenes.InstanciaSkippableCutscenes.WaitForSecondsCancelavel(4f));

        StartCoroutine(FadeOut(imagensCutscene[1], 0.5f));
        yield return new WaitForSeconds(0.5f);
        SetText("");
        yield return new WaitForSeconds(0.5f);


        //Imagem 3
        StartCoroutine(FadeIn(imagensCutscene[2], 0.5f));
        yield return new WaitForSeconds(0.5f);

        SetText(textosCutscenes[2]);
        yield return StartCoroutine(SkippableCutscenes.InstanciaSkippableCutscenes.WaitForSecondsCancelavel(4f));

        StartCoroutine(FadeOut(imagensCutscene[2], 0.5f));
        yield return new WaitForSeconds(0.5f);
        SetText("");
        yield return new WaitForSeconds(0.5f);


        //Imagem 4
        StartCoroutine(FadeIn(imagensCutscene[3], 0.5f));
        yield return new WaitForSeconds(0.5f);

        SetText(textosCutscenes[3]);
        yield return StartCoroutine(SkippableCutscenes.InstanciaSkippableCutscenes.WaitForSecondsCancelavel(4f));

        StartCoroutine(FadeOut(imagensCutscene[3], 0.5f));
        yield return new WaitForSeconds(0.5f);
        SetText("");
        yield return new WaitForSeconds(0.5f);


        //Imagem 5
        StartCoroutine(FadeIn(imagensCutscene[4], 0.5f));
        yield return new WaitForSeconds(0.5f);

        if(LocalizationSystem.GetLinguagem() == "en-US") {
            yield return StartCoroutine(SkippableCutscenes.InstanciaSkippableCutscenes.WaitForSecondsCancelavel(11f));
        } else {
            SetText(textosCutscenes[4]);
            yield return StartCoroutine(SkippableCutscenes.InstanciaSkippableCutscenes.WaitForSecondsCancelavel(2.5f));

            SetText(textosCutscenes[5]);
            yield return StartCoroutine(SkippableCutscenes.InstanciaSkippableCutscenes.WaitForSecondsCancelavel(5f));

            SetText(textosCutscenes[6]);
            yield return StartCoroutine(SkippableCutscenes.InstanciaSkippableCutscenes.WaitForSecondsCancelavel(4f));

            SetText(textosCutscenes[7]);
            yield return StartCoroutine(SkippableCutscenes.InstanciaSkippableCutscenes.WaitForSecondsCancelavel(3.5f));

            SetText(textosCutscenes[8]);
            yield return StartCoroutine(SkippableCutscenes.InstanciaSkippableCutscenes.WaitForSecondsCancelavel(3.5f));
        }
        

        StartCoroutine(FadeOut(imagensCutscene[4], 0.5f));
        yield return new WaitForSeconds(0.5f);
        SetText("");
        yield return new WaitForSeconds(0.5f);


        //Imagem 6
        StartCoroutine(FadeIn(imagensCutscene[5], 0.5f));
        yield return new WaitForSeconds(0.5f);

        SetText(textosCutscenes[9]);
        yield return StartCoroutine(SkippableCutscenes.InstanciaSkippableCutscenes.WaitForSecondsCancelavel(4.5f));

        StartCoroutine(FadeOut(imagensCutscene[5], 0.5f));
        yield return new WaitForSeconds(0.5f);
        SetText("");
        yield return new WaitForSeconds(0.5f);



        yield return new WaitForSeconds(1f);
        IniciarPrimeiraFase();
    }
    private void IniciarPrimeiraFase() {
        SceneLoader.InstanciaSceneLoader.SetProximaCena("mara mara");
        //Debug.Log(SceneLoader.InstanciaSceneLoader.GetProximaCena());
        GerenciadorCena.CarregarCena("Loading");
    }

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

    public void MusicaInicio() {
        AudioManager.InstanciaAudioManager.Play("Piano Sad");
    }

    public void SetText(string texto) {
        textoUI.text = texto;
    }

    private IEnumerator FadeIn(Image imagem, float tempoFinal) {
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

    private void IniciarCoresImagens() {
        for(int i = 0;i < NUM_IMAGENS;i++) {
            imagensCutscene[i].color = new Color(1, 1, 1, 0);
        }
    }

    private void CheckSkipCutscene() {
        if(Input.GetButtonDown("Escape")) {
            IniciarPrimeiraFase();
        }
    }

    private void CarregarStrings() {
        LocalizationSystem.GetDicionarioStringsFullCena(GerenciadorCena.NomeCenaAtual(), out stringsCutscene, out stringsPersonagensCutsceneInicial);
    }

    private void AplicarStrings() {
        textosCutscenes = new string[stringsCutscene.Count];
        for(int i = 0;i < stringsCutscene.Count;i++) {
            if(stringsPersonagensCutsceneInicial["INICIAL_" + i] != "") {
                textosCutscenes[i] = "[" + stringsPersonagensCutsceneInicial["INICIAL_" + i] + "]\n" + stringsCutscene["INICIAL_" + i];
            } else {
                textosCutscenes[i] = stringsCutscene["INICIAL_" + i];
            }
        }
    }
}
