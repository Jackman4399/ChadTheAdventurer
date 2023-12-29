using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StrawberryBarrier : MonoBehaviour {

    private void OnEnable() {
        PlayerDataManager.Instance.OnStrawberriesEnough += OnStrawberriesEnough;
    }

    private void OnDisable() {
        PlayerDataManager.Instance.OnStrawberriesEnough -= OnStrawberriesEnough;
    }

    private void OnStrawberriesEnough() => Destroy(gameObject);

}
