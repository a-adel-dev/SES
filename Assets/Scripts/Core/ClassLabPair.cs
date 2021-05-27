namespace SES.Core
{
    [System.Serializable]
    public struct ClassLabPair 
    {
        public IClassroom classroom;
        public ILab lab;

        public ClassLabPair(IClassroom _classroom, ILab _lab)
        {
            classroom = _classroom;
            lab = _lab;
        }
    }
}
