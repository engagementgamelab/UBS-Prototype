using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ExampleClass : MonoBehaviour
{
    // Creates a line renderer that follows a Sin() function
    // and animates it.

    public Color c1 = Color.yellow;
    public Color c2 = Color.red;
    public GameObject target;

    public int lengthOfLineRenderer = 20;

    public float startWidth = 1.0f;
    public float endWidth = 1.0f;
    public float threshold = 0.001f;
    public float percentsPerSecond = 0.5f; // %2 of the path moved per second
    float currentPathPercent = 0.0f; //min 0, max 1


    int lineCount = 0; 
    Vector3 lastPos = Vector3.one * float.MaxValue;
    List<Vector3> linePoints = new List<Vector3>();
    LineRenderer lineRenderer;

    bool animate = false;
    
    private Vector3 screenPoint;
    private Vector3 offset;
        

    void Start()
    {
    
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
        lineRenderer.widthMultiplier = 0.2f;
        // lineRenderer.positionCount = lengthOfLineRenderer;

        // A simple 2 color gradient with a fixed alpha of 1.0f.
        float alpha = 1.0f;
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(c1, 0.0f), new GradientColorKey(c2, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(0.5f, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
            );
        lineRenderer.colorGradient = gradient;
    
    }

    void OnMouseDown(){
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }

    void OnMouseUp(){
        // if(linePoints != null)
        //     linePoints.Clear();
        animate = true;
    }
        
    void OnMouseDrag(){

        Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorPoint) + offset;
        transform.position = cursorPosition;
        
        if(linePoints == null)
            linePoints = new List<Vector3>();
        
        linePoints.Add(cursorPosition);

    }

    void Update()
    {

        if(!animate)
            return;

        if(linePoints != null && linePoints.Count > 0) {
            currentPathPercent += percentsPerSecond * Time.deltaTime;

            if(currentPathPercent < 1.0f)
                iTween.PutOnPath(target, linePoints.ToArray(), currentPathPercent);
            else {
                animate = false;
                currentPathPercent = 0f;
                linePoints.Clear();
            }
        }

        // Vector3 mousePos = Input.mousePosition;
        // mousePos.z = Camera.main.nearClipPlane;

        // Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(mousePos);

        // float dist = Vector3.Distance(lastPos, mouseWorld);
        // if(dist <= threshold)
        //     return;

        // lastPos = mouseWorld;

        // UpdateLine();
    }

    void UpdateLine()
    {
        lineRenderer.SetWidth(startWidth, endWidth);
        lineRenderer.SetVertexCount(linePoints.Count);

        for(int i = lineCount; i < linePoints.Count; i++)
        {
             lineRenderer.SetPosition(i, linePoints[i]);
        }
        lineCount = linePoints.Count;
    }
}