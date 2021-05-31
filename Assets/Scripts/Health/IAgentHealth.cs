

namespace SES.Core
{
    public interface IAgentHealth
    {
        //SpaceHealth CurrentSpace { get; set; }
        HealthCondition HealthCondition { get; set; }
        void ExposeAgent();
        float GetInfectionQuanta();
        float GetMaskFactor();
        void InfectAgent();
        bool IsInfected();
        void ResetShortRangeInfectionQuanta();
        void SetActivityType(ActivityType type);

        void SetMaskFactor(MaskFactor factor);
    }
}