using System.Collections;
using AssetKits.ParticleImage;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class GoldVisual : MonoBehaviour
    {
        [SerializeField] private bool isOnEndScreen;
        private TextMeshProUGUI coinText;
        private int currentCoin;
        private int targetCoin;
        
        void Start()
        {
            coinText = transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
            currentCoin = ResourceManager.Instance.GetCurrentCoin();
            coinText.text = currentCoin.ToString();
            EventManager.Instance.OnSetCurrentCoin += OnSetCurrentCoin;
        }

    

        private void OnSetCurrentCoin(int amount,bool isIncrement)
        {
            if (isIncrement)
            {
                targetCoin = currentCoin + amount;
                
            }
            else
            {
                targetCoin = currentCoin - amount;
            }
            
            StartCoroutine(SetGoldText());
        }

        IEnumerator SetGoldText()
        {
            var timer = 0f;
            while (timer < 0.15f)
            {
                timer += Time.deltaTime;
                coinText.text = Mathf.RoundToInt(Mathf.Lerp(currentCoin, targetCoin, timer)).ToString();
                yield return null;
            }
            currentCoin = targetCoin;
            coinText.text = currentCoin.ToString();
        }

    }
}
