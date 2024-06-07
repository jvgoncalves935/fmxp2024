using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerExitLevel : MonoBehaviour
{
    [SerializeField] private string nextLevelName;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<MeshRenderer>().enabled = false;
        CheckEmptyLevelName();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool IsPlayerBody(Collider collider) {
        if(collider.tag == "Player") {
            return true;
        }
        return false;
    }

    private void ExitLevel() {
        SceneLoader.InstanciaSceneLoader.SetProximaCena(nextLevelName);
        GerenciadorCena.CarregarCena("Loading");
    }

    private void OnTriggerEnter(Collider collider) {
        if(IsPlayerBody(collider)) {
            ExitLevel();
        }
    }

    private void CheckEmptyLevelName() {
        if(nextLevelName == null) {
            Debug.LogError("Next level name not defined!");
        }
    }
}
