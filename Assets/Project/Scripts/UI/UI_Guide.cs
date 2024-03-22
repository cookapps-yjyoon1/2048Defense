
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public class UI_Guide : MonoBehaviour
    {
        private const int maxIndex = 5;
        private int curIndex = 0;
        
        public Action EndAction;
        
        private List<GameObject> _goGuides;
        
        
        public void Open(Action endAcion)
        {
            EndAction = endAcion;
            gameObject.SetActive(true);
            _goGuides[0].SetActive(true);
        }

        public void OnClickButton()
        {
            curIndex++;

            for (int i = 0; i < _goGuides.Count; i++)
            {
                _goGuides[i].SetActive(i == curIndex);
            }
            
            if (curIndex >= maxIndex)
            {
                EndAction?.Invoke();
                Close();
            }
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }
    }
