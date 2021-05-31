namespace SES.Core
{
    public enum ClassroomsOcsillatingState
    {
        inSession,
        onBreak
    }

    public enum ActivityType
    {
        Breathing,
        Talking,
        LoudTalking,
        Paused
    }

    public enum MaskFactor
    {
        none,
        cloth,
        surgical,
        N95
    }

    public enum StudentState
    {
        inClass,
        active,
        inLab,
        onBreak,
        inTransit
    }

    public enum HealthCondition
    {
        healthy,
        infected,
        contagious
    }

    public enum TeacherMovementStyle
    {
        classroom,
        restricted
    }

    public enum AgentControlState
    {
        dependent,
        autonomous
    }

    public enum SchoolDayState
    {
        classesInSession,
        breakTime,
        egressTime, 
        offTime,
        simOver
    }
}