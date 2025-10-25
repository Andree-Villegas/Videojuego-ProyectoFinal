using Unity.VisualScripting;
using UnityEditor;
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

        if (cam != null) cam = Camera.main.transform;
        lastCampos = cam.position;

        //Obtiene el ancho del sprite automaticamente
        spriteWidth = backgrounds[0].GetComponent<SpriteRenderer>().bounds.size.x;

    }

    private void Update()
    {
        //Mueve el fondo con la cámara
        Vector3 deltaMovement = cam.position - lastCampos;
        transform.position += new Vector3(deltaMovement.x * parallaxMultiplier, 0, 0);
        lastCampos = cam.position;

        //Reposiciona los fondos si la cámara avanza demasiado 
        foreach (var bg in backgrounds)
        {
            float camDistance = cam.position.x - bg.position.x;
            if (Mathf.Abs(camDistance) >= spriteWidth)
            {
                float offest = (camDistance > 0) ? spriteWidth * 2f : -spriteWidth * 2f;
                bg.position += new Vector3(offest, 0, 0);
            }
        }
    }
}
