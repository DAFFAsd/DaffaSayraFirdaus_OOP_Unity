using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class InvincibilityComponent: MonoBehaviour
    {
        #region Datamembers

        #region Editor Settings
        [SerializeField] private int blinkingCount = 7;
        [SerializeField] private float blinkInterval = 0.1f;
        [SerializeField] private Material blinkMaterial;
        #endregion

        #region Private Fields
        private SpriteRenderer spriteRenderer;
        private Material originalMaterial;
        private Coroutine flashRoutine;
        private Coroutine blinkRoutine;
        public bool isInvincible = false;
        private bool isPlayer = false;
        #endregion
        #endregion

        #region Methods
        #region Unity Callbacks
        void Start()
        {
        isPlayer = gameObject.CompareTag("Player");

        if (isPlayer)
        {
            // If this is a player, look for child named "Ship"
            Transform shipTransform = transform.Find("Ship");
            if (shipTransform != null)
            {
                spriteRenderer = shipTransform.GetComponent<SpriteRenderer>();
                if (spriteRenderer == null)
                {
                    Debug.LogError("Ship child object found but doesn't have a SpriteRenderer component!");
                }
            }
            else
            {
                Debug.LogError("Player object doesn't have a child named 'Ship'!");
            }
        }
        else
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        if (spriteRenderer != null)
        {
            originalMaterial = spriteRenderer.material;
        }
    }
        #endregion
        private IEnumerator BlinkingRoutine()
        {
            isInvincible = true;

            for (int i = 0; i < blinkingCount; i++)
            {
                spriteRenderer.material = blinkMaterial;
                yield return new WaitForSeconds(blinkInterval);
                spriteRenderer.material = originalMaterial;
                yield return new WaitForSeconds(blinkInterval);
            }

            isInvincible = false;
            blinkRoutine = null;
        }
        public void StartBlinking()
        {
            if (!isInvincible)
            {
                if (blinkRoutine != null)
                {
                    StopCoroutine(blinkRoutine);
                }
                blinkRoutine = StartCoroutine(BlinkingRoutine());
            }
        }
        #endregion
    }