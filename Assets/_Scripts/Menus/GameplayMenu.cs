using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayMenu : MonoBehaviour {

    private List<Image> pooledHearts;
    [SerializeField] private Sprite fullHeart, emptyHeart;
    
    [SerializeField] private RectTransform heart;
    private RectTransform heartPanel;

    private void Awake() {
        pooledHearts = new List<Image>();
        heartPanel = (RectTransform)transform.Find("HeartPanel");
    }

    private void OnEnable(){
        PlayerHealth.OnLivesChanged += OnLivesChanged;
    }

    private void OnDisable() {
        PlayerHealth.OnLivesChanged -= OnLivesChanged;
    }

    private void OnLivesChanged(int lives) {
        if (pooledHearts.Count == 0) {
            for (int i = 0; i < lives; i++) {
                RectTransform h = Instantiate(heart);

                h.SetParent(heartPanel, false);
                h.localPosition = new Vector3(h.rect.width * i, h.localPosition.y, h.localPosition.z);

                Image heartImage = h.GetComponent<Image>();
                heartImage.sprite = emptyHeart;

                pooledHearts.Add(heartImage);
            }
        }

        for (int i = 0; i < pooledHearts.Count; i++) {
            if (i < lives) pooledHearts[i].sprite = fullHeart;
            else pooledHearts[i].sprite = emptyHeart;
        }
    }
}
