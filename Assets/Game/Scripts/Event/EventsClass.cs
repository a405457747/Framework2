using System.Collections.Generic;
using System.Reflection.Emit;

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
    public string content { get; set; }
    public BreadUI breadUI { get; set; }
}

public class ErrorEvent
{
    public string ErrorMsg;
}