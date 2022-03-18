using UnityEngine;
using UnityEngine.UI;

public class HealthBarBase : MonoBehaviour
{
    public Image healthBarImage;

    private PlayerHealth player;

    void Start() {
        player = GetComponent<PlayerHealth>();
    }

    public void UpdateHealthBar( ){
        
        healthBarImage.fillAmount = Mathf.Clamp( player.curHealth / player.maxHealth, 0, 1f );
    }
}