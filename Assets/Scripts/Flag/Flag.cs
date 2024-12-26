using Bots;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flags
{
    public class Flag : MonoBehaviour, IBotBilder
    {
        [SerializeField] private BuildingPreview _buildingPreview;

        private Base _base;
        public Action<Bot> BotChangedBase;

        public void Build(Bot bot)
        {
            if (_base != null)
            {
                bot.SetBase(_base);
                BotChangedBase.Invoke(bot);

                _base.gameObject.SetActive(true);
                _base.SetBot(bot);
                _base.transform.position = transform.position;
            }
        }

        public Transform GetTransform()
        {
            return transform;
        }

        internal void SetBase(Base building)
        {
            _base = building;
            _base.gameObject.SetActive(false);
        }
    }
}
