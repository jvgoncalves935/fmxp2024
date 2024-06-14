using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MundanescapeChangeLevels : MonoBehaviour
{
    [SerializeField] private GameObject part01;
    [SerializeField] private GameObject part02;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if(PlayerUtils.IsPlayerBody(other)) {
            ToggleObjects();
        }
    }

    private void ToggleObjects() {
        StartCoroutine(ToggleObjectsCoroutine());
    }

    private IEnumerator ToggleObjectsCoroutine() {
        part01.SetActive(false);
        yield return new WaitForSeconds(0.03f);
        part02.SetActive(true);
    }


}
