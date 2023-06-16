using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private BaseCounter baseCounter;
    [SerializeField] private GameObject[] effectIfSelected;
    private void Start()
    {
        //PlayerController.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

    private void Player_OnSelectedCounterChanged(object sender, PlayerController.OnSelectedCounterChangedEventArgs e)
    {
        effectIfSelected.ToList().ForEach(u=>u.SetActive(e.selectedCounter == baseCounter));
    }
}
