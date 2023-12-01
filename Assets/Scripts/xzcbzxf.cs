using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.InputSystem;

public class xzcbzxf : MonoBehaviour
{

    CharacterController theCharacter;
    public PlayerActionsSettings playerControls;
    Vector3 moveDirection;


    private void Awake()
    {
        playerControls = new PlayerActionsSettings();
    }
    private void OnEnable()
    {
        
        playerControls.Enable();
        playerControls.Player.Fire.performed += Explode;
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        theCharacter = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = playerControls.Player.Move.ReadValue<Vector2>();
        theCharacter.Move(moveDirection);
    }

    public void Explode(InputAction.CallbackContext context)
    {
        Debug.Log("HOlyShit");
    }
}
