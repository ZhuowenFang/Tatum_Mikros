using System.Collections;
using UnityEngine;
using System.IO;

public class Platform : MonoBehaviour
{
    private UI UIObject;
    public float movementSpeed = 1.4f;
    private Vector3 originalPosition;
    private float timer = 0f;
    private float move_timer = 0f;
    private Renderer objRenderer;
    private float ana_startTime = 0f;
    private float ana_endTime = 0f;
    private float ana_timeDifference;
    private Color originalColor;
    public GameObject beatsBar;
    private bool done = false;
    public int down;
    public int distance;
    private bool moving;
    public BallController ballController;
    private void Start()
    {
        UIObject = FindObjectOfType<UI>();
        originalPosition = transform.position;
/*        objRenderer = GetComponent<Renderer>();
        originalColor = objRenderer.material.color;*/
        HideBeatsBar();
    }

    void Update()
    {
        // Check if the player is on the elevator
        move_timer += Time.deltaTime;

        if (moving && UIObject.beat)
        {   
            // Move the elevator upward
            if (down == 0 && transform.position.y >= originalPosition.y + distance)
            {
                UIObject.beat = false;
                UIObject.count = 0;
                UIObject.pkey = 1;
                moving = false;
                done = true;
                HideBeatsBar();
                ana_endTime = Time.time;
                ana_timeDifference = ana_endTime - ana_startTime;
                string fileName = "analytics_puzzle_time_1.txt";
                string content = string.Format("Time : {0}\n", ana_timeDifference);
                File.AppendAllText(fileName, content);
            } else if (down == 1 && transform.position.y <= originalPosition.y - distance)
            {
                UIObject.beat = false;
                UIObject.count = 0;
                UIObject.pkey = 1;
                moving = false;
                done = true;
                HideBeatsBar();
                ana_endTime = Time.time;
                ana_timeDifference = ana_endTime - ana_startTime;
                string fileName = "analytics_puzzle_time_1.txt";
                string content = string.Format("Time : {0}\n", ana_timeDifference);
                File.AppendAllText(fileName, content);
            }
            else
            {
                if (timer > 0.6f)
                {
                    moving = false;
                    timer = 0;
                }
                if (down == 1)
                {
                    ballController.transform.Translate(Vector3.down * movementSpeed * Time.deltaTime);
                    transform.Translate(Vector3.down * movementSpeed * Time.deltaTime);
                    timer += Time.deltaTime;
                }
                else
                {
                    transform.Translate(Vector3.up * movementSpeed * Time.deltaTime);
                    timer += Time.deltaTime;
                }



            }
        }
        else if(transform.position.y > originalPosition.y && !done)
        {
                transform.Translate(Vector3.down * movementSpeed * Time.deltaTime);
        }
        else if (transform.position.y < originalPosition.y && !done)
        {
            transform.Translate(Vector3.up * movementSpeed * Time.deltaTime);
        }

        //if (UIObject.color)
        //{
        //    ChangeObjectColor(Color.red);
        //}
        //else
        //{
        //    ChangeObjectColor(originalColor);
        //}
    }


    public void collision_()
    {
        Debug.Log("siusiusisu");
        if (timer == 0f)
        {
            ana_startTime = Time.time;
            UIObject.beat = true;
            ShowBeatsBar();
        }
    }

        void ChangeObjectColor(Color newColor)
    {
        // objRenderer.material.color = newColor;
        objRenderer.material = new Material(objRenderer.material);
        objRenderer.material.color = newColor;
    }

    void ChangeColorBack()
    {
        // Use the originalColor variable to revert the color
        ChangeObjectColor(originalColor);
    }

    //private void OnTriggerExit2D(Collider2D collider2D)
    //{
    //    if (collider2D.CompareTag("Player"))
    //    {

    //        //UIObject.gameObject.SetActive(true);
    //        UIObject.beat = false;
    //        UIObject.count = 0;
    //        UIObject.pkey = true;
    //        UIObject.moving = false;
    //        Debug.Log("not");
    //    }
    //}
    public void ShowBeatsBar()
    {
        if (beatsBar != null)
        {
            // Try to find the renderer in the children
            Renderer[] renderers = beatsBar.GetComponentsInChildren<Renderer>(true);

            if (renderers.Length > 0)
            {
                foreach (Renderer renderer in renderers)
                {
                    renderer.enabled = true;
                }
            }
            else
            {
                Debug.LogError("Renderer component not found in the children of beatsBar GameObject.");
            }
        }
        else
        {
            Debug.LogError("beatsBar GameObject not assigned.");
        }
    }

    // Method to make the GameObject invisible
    public void HideBeatsBar()
    {   
        ballController.EnableMovement();
        if (beatsBar != null)
        {
            // Try to find the renderer in the children
            Renderer[] renderers = beatsBar.GetComponentsInChildren<Renderer>(true);

            if (renderers.Length > 0)
            {
                foreach (Renderer renderer in renderers)
                {
                    renderer.enabled = false;
                }
            }
            else
            {
                Debug.LogError("Renderer component not found in the children of beatsBar GameObject.");
            }
        }
        else
        {
            Debug.LogError("beatsBar GameObject not assigned.");
        }
    }

    public void allowMove(float t)
    {
        moving = true;
        timer -= t;

    }
/*    public void stopMove()
    {
        if (move_timer > 0.05f)
        {

            moving = false;
        }
        
    }*/

}