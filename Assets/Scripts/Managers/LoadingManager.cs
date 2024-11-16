using System.Collections;
//using Facebook.Unity;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace Managers
{
    public class LoadingManager : MonoBehaviour
    {
        
        void Awake ()
        {
            Application.targetFrameRate = 60;
            //InitFacebook();
            StartCoroutine(LoadingRoutine());
        }

        private IEnumerator LoadingRoutine()
        {
            
            var index = 2;
            if (PlayerPrefs.HasKey("LastScene"))
            {
                index = PlayerPrefs.GetInt("LastScene");
            }
            yield return new WaitForSeconds(2f);
            SceneManager.LoadScene(index);
        }
        
    }
    
}
