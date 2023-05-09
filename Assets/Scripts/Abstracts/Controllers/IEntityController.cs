using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kitchen.Abstract.Controller
{
    public interface IEntityController
    {
        Transform transform { get; }
    }
}