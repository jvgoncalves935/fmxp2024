using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using StarterAssets;

public class Player3D : MonoBehaviour
{
    [Header("Physics Settings")]
    [SerializeField] private InputActionReference attack;
    [SerializeField] private bool isAttacking = false;
    [SerializeField] private bool grounded;
    [SerializeField] private float attackInitDuration;
    [SerializeField] private float attackDelayDuration;

    [Header("Health Settings")]
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private int coins = 0;
    [SerializeField] private int playerCurrentHealth = 100;
    [SerializeField] private float invencibilityTime = 1.0f;
   
    private bool isPlayerHit = false;
    private bool playerKilled = false;
    private int playerFullHealth;
    private bool gamePaused;

    private PlayerAttackCollider attackCollider;
    private ThirdPersonController controller;
    private Animator animator;
    private MeshRenderer meshRenderer;

    public static GameObject instance;
    private static Player3D _instance;
    public static Player3D Instance {
        get {
            if(_instance == null) {
                _instance = instance.GetComponent<Player3D>();
            }
            return _instance;
        }
    }
    void Awake() {
        instance = FindObjectOfType<Player3D>().gameObject;
    }
    void Start()
    {
        InitPlayerObjects();
        ToggleAttackCollider(false);
    }

    private void OnEnable() {
        attack.action.started += PlayerAttack;
    }

    private void OnDisable() {
        attack.action.started -= PlayerAttack;
    }

    // Update is called once per frame
    void Update()
    {
        GroundCheck();
        GetAnimations();
    }

    private void GroundCheck() {
        grounded = controller.Grounded;
    }

    public void SetPlayerPositionCurrentCheckpoint() {
        Checkpoint3D currentCheckpoint = Checkpoint3DManager.Instance.GetCurrentCheckpoint();

        float x = currentCheckpoint.transform.position.x;
        float y = currentCheckpoint.transform.position.y;
        float z = currentCheckpoint.transform.position.z;

        Vector3 newPlayerPosition = new Vector3(x, y, z);

        ChangePlayerPosition(newPlayerPosition);
    }

    private void ChangePlayerPosition(Vector3 newPosition) {
        gameObject.transform.position = newPosition;
    }

    private void PlayerAttack(InputAction.CallbackContext callback) {
        if(!isAttacking && grounded) {
            StartCoroutine(PlayerAttackCoroutine());
        }
    }

    private void InitPlayerObjects() {
        attackCollider = transform.Find("AttackCollider").gameObject.GetComponent<PlayerAttackCollider>();
        controller = GetComponent<ThirdPersonController>();
        animator = GetComponent<Animator>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private IEnumerator PlayerAttackCoroutine() {
        //Ataque do Player
        isAttacking = true;
        ToggleAttackCollider(isAttacking);
        TogglePlayerMovement(!isAttacking);

        yield return new WaitForSeconds(attackInitDuration);
        
        //Collider do ataque desativado
        ToggleAttackCollider(!isAttacking);
        yield return new WaitForSeconds(attackDelayDuration);

        //Fim anima��o ataque
        isAttacking = false;
        TogglePlayerMovement(!isAttacking);

    }

    private void ToggleAttackCollider(bool toggle) {
        attackCollider.gameObject.SetActive(toggle);
    }

    private void TogglePlayerMovement(bool toggle) {
        controller.enabled = toggle;
    }
    public void PlayerDamage(int damage) {
        StartCoroutine(PlayerDamageCoroutine(damage));
    }
    private IEnumerator PlayerDamageCoroutine(int damage) {
        playerCurrentHealth -= damage;
        NormalizePlayerDamage();

        healthBar.SetHealth(playerCurrentHealth);

        if(!IsPlayerKilled()) {
            isPlayerHit = true;
            PlayerDamagedAnimation();
            yield return new WaitForSeconds(invencibilityTime);
            isPlayerHit = false;
        } else {
            KillPlayer();
            yield return new WaitForSeconds(3.0f);
            ResetScene.InstanciaResetScene.Reset();
        }

    }

    private void NormalizePlayerDamage() {
        if(playerCurrentHealth <= 0) {
            playerCurrentHealth = 0;
        }
    }

    private bool IsPlayerKilled() {
        return playerCurrentHealth <= 0;
    }

    private void GetAnimations() {

    }

    private void PlayerDamagedAnimation() {
        StartCoroutine(PlayerDamagedCoroutine());
    }

    private IEnumerator PlayerDamagedCoroutine() {
        bool isEnabled = true;
        while(isPlayerHit && !playerKilled) {
            isEnabled = !isEnabled;
            meshRenderer.enabled = isEnabled;
            yield return new WaitForSeconds(0.02f);
        }
        meshRenderer.enabled = true;

    }

    private void KillPlayer() {
        playerKilled = true;
        isPlayerHit = true;
        playerCurrentHealth = 0;
    }
}