using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningManager : MonoBehaviour
{

    public GameObject lightningPrefab;

    private Vector3 coordinates;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
      if(Input.GetMouseButtonDown(0))
      {
        CreateLightning();
      }
    }

    void CreateLightning()
    {
      float distance;

      Plane plane = new Plane(Vector3.up, 0);
      Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

      if(plane.Raycast(ray, out distance))
      {
        GameObject lightning = (GameObject)Instantiate(lightningPrefab);
        lightning.transform.position = ray.GetPoint(distance);
        lightning.transform.position = new Vector3(lightning.transform.position.x, lightning.transform.position.y +15.0f, lightning.transform.position.z);
        Destroy(lightning, 1.5f);
      }
    }
}
