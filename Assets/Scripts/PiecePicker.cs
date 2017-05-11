using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GGL;

public class PiecePicker : MonoBehaviour
{

    public float pieceHeight = 5f;
    public float rayDistance = 1000f;
    public LayerMask selectionIgnoreLayer;

    private Piece selectedPiece;
    private CheckerBoard board;

    // Use this for initialization
    void Start()
    {
        // Find the checkerboard in the scene
        board = FindObjectOfType<CheckerBoard>();
        // Check errors
        if(board == null)
        {
            Debug.LogError("There is no checkerboard in the scene!");
        }
    }
    // Check if we are selecting a piece
    void CheckSelection()
    {
        // Create a ray from camera mouse position to world
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        GizmosGL.color = Color.red;
        GizmosGL.AddLine(ray.origin, ray.origin + ray.direction * rayDistance, 0.15f, 0.15f);
        RaycastHit hit;
        // Check if the player hits the mouse button
        if (Input.GetMouseButtonDown(0))
        {
            // Cast a ray to detect piece
            if(Physics.Raycast(ray, out hit, rayDistance))
            {
                // Set the selected piece to be the hit object
                selectedPiece = hit.collider.GetComponent<Piece>();
                // If the user did not hit a piece
                if(selectedPiece == null)
                {
                    Debug.Log("Cannot pick up object:" + hit.collider.name);
                }
            }
        }
    }
    // Move the slected piece if one is selected
    void MoveSelection()
    {
        // chekc if a piece has been selected
        if(selectedPiece != null)
        {
            // Create a new ray from the camera
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            GizmosGL.color = Color.yellow;
            GizmosGL.AddLine(ray.origin, ray.origin + ray.direction * rayDistance, 0.1f, 0.1f);
            RaycastHit hit;
            // Raycast to only hit objects that aren't pieces
            if(Physics.Raycast(ray, out hit, rayDistance, ~selectionIgnoreLayer))
            {
                // Obtain the hit point
                GizmosGL.color = Color.blue;
                GizmosGL.AddSphere(hit.point, 0.5f);

            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckSelection();
        MoveSelection();
    }
}
