using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;
    public PlayerMovement playerMovement;
    public Animator animator;

    void Awake()
    {
         // Referensi : https://gamedevbeginner.com/singletons-in-unity-the-right-way/
        if (Instance != null && Instance != this){
            Destroy(gameObject);
        }
        else{
            Instance = this;
        }
    }

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        GameObject engineEffect = GameObject.Find("EngineEffect");
        if (engineEffect != null){
            animator = engineEffect.GetComponent<Animator>();
        }
    }

    void FixedUpdate()
    {
        playerMovement.Move();
    }

    void LateUpdate()
    {
        if (animator != null){
            animator.SetBool("IsMoving", playerMovement.IsMoving());
        }
    }
}
