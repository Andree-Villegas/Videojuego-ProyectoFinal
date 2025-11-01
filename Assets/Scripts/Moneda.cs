using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            if (ContadorMonedas.Instancia != null)
            {
                ContadorMonedas.Instancia.A�adirMoneda(1);
            }

            Destroy(gameObject);
        }
    }

}
