using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayMenu : Menu {

    private List<Image> pooledHearts;
    [SerializeField] private Sprite fullHeart, emptyHeart;
    [SerializeField] private RectTransform heart;
    private RectTransform heartPanel;

    [SerializeField] private PlayerHealth playerHealth;

    protected override void Awake() {
        base.Awake();

        pooledHearts = new List<Image>();
        heartPanel = (RectTransform)transform.Find("HeartPanel");

        for (int i = 0; i < playerHealth.MaxLives; i++) {
                RectTransform h = Instantiate(heart);

                h.SetParent(heartPanel, false);
                h.localPosition = new Vector3(h.rect.width * i, h.localPosition.y, h.localPosition.z);

                Image heartImage = h.GetComponent<Image>();
                heartImage.sprite = fullHeart;

                pooledHearts.Add(heartImage);
            }
    }

    private void OnEnable() {
        playerHealth.OnLivesChanged += OnLivesChanged;
    }

    private void OnDisable() {
        playerHealth.OnLivesChanged -= OnLivesChanged;
    }

    private void OnLivesChanged(int lives) {
        for (int i = 0; i < pooledHearts.Count; i++) {
            if (i < lives) pooledHearts[i].sprite = fullHeart;
            else pooledHearts[i].sprite = emptyHeart;
        }
    }
}
