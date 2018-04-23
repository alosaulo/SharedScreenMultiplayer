using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    Vector3 desiredPos;

    private Vector3 moveVelocity;
    private float zoomSpeed;

    public float dampTime = 0.2f;                 // Approximate time for the camera to refocus.
    public float screenEdgeBuffer = 4f;           // Space between the top/bottom most target and the screen edge.
    public float minSize = 6.5f;                  // The smallest orthographic size the camera can be.

    Camera mainCamera;

    // Use this for initialization
    void Start () {
        mainCamera = Camera.main;
    }
	
	// Update is called once per frame
	void Update () {
        CameraMove();
        Zoom();
    }

    public void CameraMove() {
        desiredPos = FindAvaragePosition();
        transform.position = Vector3.SmoothDamp(transform.position, 
            desiredPos, 
            ref moveVelocity, 
            dampTime);
    }

    public void Zoom() {
        float requiredSize = CameraSize();
        mainCamera.orthographicSize = Mathf.SmoothDamp(mainCamera.orthographicSize,
                                                        requiredSize,
                                                        ref zoomSpeed,
                                                        dampTime);
    }

    private Vector3 FindAvaragePosition() {
        Vector3 averagePos = transform.position;

        int activePlayers = 0;

        foreach (GameObject player in PlayerManager._instance.Players)
        {
            if (player.activeSelf) {
                activePlayers = activePlayers + 1;
                averagePos = player.transform.position;
            }
        }

        if (activePlayers > 0) {
            averagePos = averagePos / activePlayers;
        }

        averagePos.z = transform.position.z;

        return averagePos;
    }

    private float CameraSize() {
        Vector3 desiredLocalPos = transform.InverseTransformPoint(desiredPos);

        float size = 0;

        /*Obtem a distância da posição da câmera e, em seguida, percorra todos os alvos 
            * que eles devem mostrar, obtendo a distância máxima desse local (x ou y). 
            * Em seguida, use essa distância máxima para alterar o tamanho da câmera para mostrar 
            * todos os elementos.*/
        foreach (GameObject player in PlayerManager._instance.Players)
        {
            if (player.activeSelf) {
                Vector3 targetLocalPos = transform.InverseTransformPoint(player.transform.position);
                Vector3 desiredPosToTarget = targetLocalPos - desiredLocalPos;
                size = Mathf.Max(size, Mathf.Abs(desiredPosToTarget.y));
                size = Mathf.Max(size, Mathf.Abs(desiredPosToTarget.x) / mainCamera.aspect);
            }
        }

        size += screenEdgeBuffer;

        size = Mathf.Max(size, minSize);

        return size;
    }

}
