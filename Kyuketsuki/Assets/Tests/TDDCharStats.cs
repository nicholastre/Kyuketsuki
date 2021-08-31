using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TDDCharStats
{
    
    [Test]
    public void TestInstanceNotNull()
    {
        StatsTDD instance = new StatsTDD();

        Assert.NotNull(instance);
    }
}
