using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tips.Part_2_Result
{
    public interface IAgentJumpInput
    {
        public event Action OnJumpInput;
    }
}