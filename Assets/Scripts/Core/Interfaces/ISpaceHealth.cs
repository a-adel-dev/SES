namespace SES.Core
{
    public interface ISpaceHealth
    {
        void DissipateConcentration();
        float Concentration { get; set; }
    }
}