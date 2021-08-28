using System.Collections.Generic;

public class NullEvent
{
}

public class ReadSuccessEvent
{
    public List<Bread> list;
}

public class PerSecondEvent
{
    public double timeStamp;
}

public class BtnClickEvent
{
    public int btnIndex { get; set; }
}