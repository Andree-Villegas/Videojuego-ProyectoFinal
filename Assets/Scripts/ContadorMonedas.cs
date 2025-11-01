using UnityEngine;
using TMPro;

public class ContadorMonedas : MonoBehaviour
{
    public static ContadorMonedas Instancia;

    private void Awake()
    {
        if (Instancia == null)
        {
            Instancia = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private int coins = 0;
    public TMP_Text textCoins;

    public void AñadirMoneda(int cantidad = 1)
    {
        coins += cantidad;

        if (textCoins != null)
        {
            textCoins.text = coins.ToString();
        }

        Debug.Log($"Monedas totales: {coins}");
    }

}