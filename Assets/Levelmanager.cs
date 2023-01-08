using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class Levelmanager : MonoBehaviour
{
    public int level;
    public TextMeshProUGUI text;
    bool advanced;
    // Start is called before the first frame update
    void Start()
    {
       // DontDestroyOnLoad(this.gameObject);
    }
    private IEnumerator WaitForSceneLoad()
    {
        yield return new WaitForSeconds(2);
            SceneManager.LoadScene("lv" + level.ToString());

    }
    // Update is called once per frame
    void Update()
    {
        if(GameObject.FindGameObjectWithTag("Enemy") == null &&!gameObject.GetComponent<PlaceCard>().holding && !advanced)
        {
            advanced = true;
            level++;
            // PlayerPrefs.SetInt("Level",level);
            text.text =" Level "+(level-1).ToString()+ " completed. " + " Advancing to level "+ level.ToString() +".";

           StartCoroutine(WaitForSceneLoad());
           // SceneManager.LoadScene("lv" + level.ToString());
        }
    }
    public void Reload()
    {
        SceneManager.LoadScene("lv" + level.ToString());
    }
}
