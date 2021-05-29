
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SES.Core
{
    public interface IAI
    {
        GameObject GetGameObject();
        void IdleAgent();
        void PauseAgent();
        void ResumeAgent();
        void NavigateTo(Vector3 location);
        void GoToAnotherLevel(Vector3 location);
        Spot currentDesk { get; set; }
        IBathroom bathroomToVisit { get; set; }

    }
}
