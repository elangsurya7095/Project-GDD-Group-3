using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Referensi ke Transform pemain
    public float smoothSpeed = 0.125f; // Kecepatan pergerakan kamera

    void FixedUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position; // Dapatkan posisi target pemain
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed); // Interpolasi posisi kamera
            transform.position = smoothedPosition; // Atur posisi kamera

            // Batasi posisi kamera agar tetap berada di dalam batas tilemap collider
            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, -52f, 253.9f),
                Mathf.Clamp(transform.position.y, -34.01f, 20f),
                -10f
            );
        }
    }
}
