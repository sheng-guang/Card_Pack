using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public interface IRes
{
    string DirectoryName { get; }
    string PackName { get; }
    string KindName { get; }
    
}

public interface IResGetter
{
    object GetNewObject(ResArgs a);
}
public interface IResCreater<T> : IResGetter
{
    T GetNew(ResArgs a);
}




