using System;
using System.Collections;

namespace Moonlighter_Mod_Helper.Api
{
    public interface IRevertable
    {
        int TimeToRevert { get; set; }

        IEnumerator RevertTimer();
        void Revert();
    }
}