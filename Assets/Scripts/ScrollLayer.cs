using UnityEngine;

public class ScrollLayer : MonoBehaviour
{
    [SerializeField] private Transform cam; // Cámara a seguir
    [SerializeField] private float parallaxMultiplier = 0.5f; //Velocidad del parallax

    private float spriteWidth; //Ancho del fondo
    private Vector3 lastCampos; // Última posición de la cámara
    
    private Transform[] backgrounds = new Transform[2];

    void Start()
    {
        if (transform.childCount != 2)
        {
            Debug.LogError($"ParallaxScrollLayer only works with 2 Bg Children, Fix '{name}' GameObject");
            return;
        }

        //Obtiene los fondos
        for (int i = 0; i<2; i++)
        {
            backgrounds[i] = transform.GetChild(i);
        }

    }

   
}
