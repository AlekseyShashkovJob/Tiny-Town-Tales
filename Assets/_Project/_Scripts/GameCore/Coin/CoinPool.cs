using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameCore.Coin
{
    public class CoinPool : MonoBehaviour
    {
        [SerializeField] private int _poolSize = 5;
        [SerializeField] private GameObject _coinPrefab;

        private Queue<GameObject> _coinPool;
        private Color _coinColor = Color.white;

        private void Awake()
        {
            _coinPool = new Queue<GameObject>();

            for (int i = 0; i < _poolSize; ++i)
            {
                GameObject coin = Instantiate(_coinPrefab);
                coin.SetActive(false);
                _coinPool.Enqueue(coin);
            }
        }

        public void SetCoinColor(Color color)
        {
            _coinColor = color;
        }

        public GameObject GetCoin()
        {
            GameObject coin = _coinPool.Count > 0
                ? _coinPool.Dequeue()
                : Instantiate(_coinPrefab);

            coin.SetActive(true);

            Image coinImage = coin.GetComponent<Image>();
            if (coinImage != null)
                coinImage.color = _coinColor;

            return coin;
        }

        public void ReturnCoin(GameObject coin)
        {
            coin.SetActive(false);
            _coinPool.Enqueue(coin);
        }
    }
}