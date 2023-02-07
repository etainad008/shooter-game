using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    private bool isEnter = false;
    private bool isShopMenuOpen = false;

    readonly KeyCode openShopButton = KeyCode.E;

    public GameEvent OnPlayerInShopRange;
    public GameEvent OnShopInteract;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isEnter = true;
            OnPlayerInShopRange.Raise(isEnter);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isEnter = false;
            OnPlayerInShopRange.Raise(isEnter);
        }
    }

    private void Update()
    {
        if (isEnter)
        {
            if(Input.GetKeyDown(openShopButton))
            {
                HandleShopMenu();
            }
        }
    }

    public void HandleShopMenu()
    {
        Time.timeScale = isShopMenuOpen ? 1f : 0;
        OnPlayerInShopRange.Raise(isShopMenuOpen);
        isShopMenuOpen = !isShopMenuOpen;
        Cursor.lockState = isShopMenuOpen ? CursorLockMode.Confined : CursorLockMode.Locked;
        Cursor.visible = isShopMenuOpen;
        OnShopInteract.Raise(isShopMenuOpen);
    }
}
