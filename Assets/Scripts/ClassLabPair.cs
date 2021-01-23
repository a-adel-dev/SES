[System.Serializable]
public struct ClassLabPair
{
    public Classroom classroom;
    public Lab lab;

    public ClassLabPair(Classroom _classroom, Lab _lab)
    {
        classroom = _classroom;
        lab = _lab;
    }
}
