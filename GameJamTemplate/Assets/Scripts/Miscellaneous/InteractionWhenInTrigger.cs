using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InteractionWhenInTrigger : MonoBehaviour
{

    public GameObject interactionPopup;
    public GameObject DialogPopup;
    private bool _playerInRange;

    // Start is called before the first frame update
    void Start()
    {

        if (interactionPopup != null)
        {
            interactionPopup.SetActive(false);
        }

        if (DialogPopup != null)
        {
            DialogPopup.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (_playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            DialogPopup.SetActive(true);
            interactionPopup.SetActive(false);
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactionPopup.SetActive(true);
            _playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DialogPopup.SetActive(false);
            interactionPopup.SetActive(false);
            _playerInRange = false;
        }
    }
}
