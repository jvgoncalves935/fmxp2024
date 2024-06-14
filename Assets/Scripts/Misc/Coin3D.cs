using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin3D : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 150 * Time.deltaTime, 0);   
    }

    private void OnTriggerEnter(Collider collider) {
        if(PlayerUtils.IsPlayerBody(collider)) {
            AddCoin();
        }
    }

    private void AddCoin() {
        Player3D.Instance.AddCoin();
        gameObject.SetActive(false);
    }
}
