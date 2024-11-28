using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTutorial : MonoBehaviour
{
    [SerializeField] private GameObject tutorialPanel;
    private void Start()
    {
        tutorialPanel.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            tutorialPanel.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            tutorialPanel.SetActive(false);
        }
    }

}
