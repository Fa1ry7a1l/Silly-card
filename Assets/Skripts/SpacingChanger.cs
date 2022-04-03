using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpacingChanger : MonoBehaviour
{
    [SerializeField] private GameManagerSrc _gameManager;
    [SerializeField] private HorizontalLayoutGroup _enemy;
    [SerializeField] private HorizontalLayoutGroup _player;
    private Action<int, Transform, bool> _added;

    private void Awake()
    {
        _added += OnAdded;
        _gameManager.Init(_added);
    }

    private void OnAdded(int count, Transform hand, bool type)
    {
        
        float hand_width = hand.GetComponent<RectTransform>().rect.width;
        float card_width = hand.GetChild(0).GetComponent<RectTransform>().rect.width;
        float new_spacing = -(card_width - (hand_width - card_width) / count);
        if (type)
        {

            _enemy.spacing = new_spacing;

        }
        else _player.spacing = new_spacing;
    }
}
