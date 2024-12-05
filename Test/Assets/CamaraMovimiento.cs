using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraMovimiento : MonoBehaviour
{
    private GameObject player;
    
    private float offsetDistanceY = 2f;
    private float offsetDistanceZ = -1f;
    private float rotationSpeed = 5f;
    private float pitch = 0f; // Ángulo vertical de la cámara
    private float yaw = 0f; // Ángulo horizontal de la cámara
    private float mouseSensitivity = 100f; // Sensibilidad del ratón
    private Quaternion initialRotation;
    public Vector3 offset = new Vector3(0, 2f, -1f); // Offset de la posición de la cámara respecto al jugador
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Cursor.lockState = CursorLockMode.Locked;
    }   

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        yaw += mouseX; // Rotación horizontal
        pitch -= mouseY; // Rotación vertical
        pitch = Mathf.Clamp(pitch, -45f, 45f);
        transform.rotation = Quaternion.Euler(pitch, yaw, 0f);
        transform.position = player.transform.position + new Vector3(0, offsetDistanceY, offsetDistanceZ);
        transform.position = player.transform.position + transform.rotation * offset;
        initialRotation = transform.rotation;
    }
    
    void LateUpdate()
    {
        // Actualiza la posición de la cámara según el jugador
        transform.position = player.transform.position + new Vector3(0, offsetDistanceY, offsetDistanceZ);

        Quaternion targetRotation = Quaternion.LookRotation(player.transform.forward, Vector3.up);

        // Combina la rotación inicial con la orientación del jugador para suavizar
        transform.rotation = Quaternion.Lerp(initialRotation, targetRotation, Time.deltaTime * rotationSpeed);  
    }
}
