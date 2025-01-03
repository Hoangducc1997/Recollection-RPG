using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionZone : MonoBehaviour
{
    public string tagTarget = "Player";

    public List<Collider2D> detectedOjs = new List<Collider2D>();
    public Animator boxTrapAnimator; // Gắn Animator của Box Trap vào đây


    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == tagTarget)
        {
            detectedOjs.Add(collider);
            if (boxTrapAnimator != null)
            {
                boxTrapAnimator.SetBool("IsOpenBoxTrap", true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == tagTarget)
        {
            detectedOjs.Remove(collider);
            if (boxTrapAnimator != null)
            {
                boxTrapAnimator.SetBool("IsOpenBoxTrap", false);
            }
        }
    }
}
