using UnityEngine;

public class HitboxControllerScript : MonoBehaviour
{
    public GameObject targetObject;

    private float appearTimer = .1f;
    private float currTimer = 0f;

    private void Start()
    {
        targetObject.SetActive(false);
    }

    void Update()
    {
        AppeareOnClick();

        if (currTimer > 0f)
        {
            currTimer -= Time.deltaTime;
        }
        else
            targetObject.SetActive(false);
    }

    private void AppeareOnClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            targetObject.SetActive(true);
            currTimer = appearTimer;
        }
    }
}
