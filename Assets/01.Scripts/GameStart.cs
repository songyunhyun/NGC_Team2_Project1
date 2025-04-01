using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(mousePos, new Vector3(0, 0, 0), 0);
            if (hit.collider != null)
            {

                if (hit.collider.tag == "Start")
                {
                    SceneManager.LoadScene("SYH");
                }
                else if (hit.collider.tag == "Option")
                {
                    //SceneManager.LoadScene("LevelSelect");¹Ì±¸Çö
                }
                else if (hit.collider.tag == "Exit")
                {
                    Application.Quit();
                }
            }
        }
    }
}
