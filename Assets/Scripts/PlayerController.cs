using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float dashSpeed = 4f;
    [SerializeField] private TrailRenderer myTrailRenderer;
    public Animator animator;

    private PlayerControls playerControls;
    private Vector2 movement;
    private Rigidbody2D rb;
    private float directionX;
    private float directionY;
    private float lastInputTime;

    private bool isDashing = false;

    private void Awake() {
        lastInputTime = Time.time;
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Start() {
        playerControls.Dash.Dash.performed += _ => Dash();
    }

    private void OnEnable(){
        playerControls.Enable();
    }

    private void Update(){

        if (Time.time > lastInputTime + .075f) {
            PlayerInput();
            lastInputTime = Time.time;
        }

    }
    private void FixedUpdate(){
        Move();
    }

    private void PlayerInput(){
        movement = playerControls.Movement.Move.ReadValue<Vector2>();
        
        if (movement.x != 0 || movement.y != 0)
        {
            directionX = movement.x;
            directionY = movement.y;
        }
        animator.SetFloat("DirectionX", directionX);
        animator.SetFloat("DirectionY", directionY);
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    private void Move(){
        rb.MovePosition(rb.position + movement.normalized * (moveSpeed * Time.fixedDeltaTime));
    }

    private void Dash() { 
        if (!isDashing) {
            isDashing = true;
            moveSpeed *= dashSpeed;
            myTrailRenderer.emitting = true;
            StartCoroutine(EndDashRoutine());
        }
    }

    private IEnumerator EndDashRoutine() {
        float dashTime = .2f;
        float dashCD = .25f;
        yield return new WaitForSeconds(dashTime);
        moveSpeed /= dashSpeed;
        myTrailRenderer.emitting = false;
        yield return new WaitForSeconds(dashCD);
        isDashing = false;
    }
}
