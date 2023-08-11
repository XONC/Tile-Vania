using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
  [SerializeField] AudioClip coinPickUpClip;
  [SerializeField] int baseSource = 100;

  bool getCoin = false; // 防止捡起硬币两次

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.gameObject.tag == "Player" && !getCoin)
    {
      getCoin = true;
      AudioSource.PlayClipAtPoint(coinPickUpClip, Camera.main.transform.position);
      FindObjectOfType<GameSession>().SetCoinCount(baseSource);

      Destroy(gameObject);
    }
  }

}
