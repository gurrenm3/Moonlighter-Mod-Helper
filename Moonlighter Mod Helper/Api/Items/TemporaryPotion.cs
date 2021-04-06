using Moonlighter_Mod_Helper.Extensions;
using System;
using System.Collections;
using UnityEngine;

namespace Moonlighter_Mod_Helper.Api.Items
{
    public abstract class TemporaryPotion : Potion, IRevertable
    {
        public abstract int TimeToRevert { get; set; }

        public TemporaryPotion()
        {

        }

        public abstract void Revert();


        public void StartRevertCoroutine()
        {
            StartCoroutine(RevertTimer());
        }

        public virtual IEnumerator RevertTimer()
        {
            yield return new WaitForSecondsRealtime(TimeToRevert);
            Revert();
        }
    }
}