namespace TP4Tests

open System
open ProgFoncTP4Foot
open Microsoft.VisualStudio.TestTools.UnitTesting

[<TestClass>]
type TestClass () =

    [<TestMethod>]
    member this.TestMethodPassing () =
        
        Assert.IsTrue(true);