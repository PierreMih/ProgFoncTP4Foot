namespace TP4Tests

open System
open System.Linq
open ProgFoncTP4Foot
open TirAuBut
open Microsoft.VisualStudio.TestTools.UnitTesting
open FSharp.Collections

[<TestClass>]
type TestClass () =
    let numberOfTries = 10000000
    
    [<TestMethod>]
    member this.Shoot () =
        let rand = Random()
        let tries = Array.init numberOfTries (fun _ -> shoot (rand.Next()))
        Assert.IsTrue(tries.Length > 0)
        
    [<TestMethod>]
    member this.ShootAsync () =
        let rand = Random.Shared
        let tries = Array.Parallel.init numberOfTries (fun _ -> shoot (rand.Next()))
        Assert.IsTrue(tries.Length > 0)
        
    [<TestMethod>]
    member this.ShootCanReturnBothValue () =
        let rand = Random()
        let tries = List.init numberOfTries (fun _ -> shoot (rand.Next()))
        let successList = tries |> List.filter (fun b -> b = true) 
        let failureList = tries |> List.filter (fun b -> b = false)
        let areThereSuccess = not successList.IsEmpty
        let areThereFailures = not failureList.IsEmpty
        Assert.IsTrue(areThereSuccess && areThereFailures)

    [<TestMethod>]
    member this.ShootCanReturnBothValueParallel () =
        let rand = Random.Shared
        let tries = Array.Parallel.init numberOfTries (fun _ -> shoot (rand.Next()))
        let successList = tries |> Array.Parallel.filter (fun b -> b = true) 
        let failureList = tries |> Array.Parallel.filter (fun b -> b = false) 
        let areThereSuccess = successList.Any()
        let areThereFailures = failureList.Any()
        Assert.IsTrue(areThereSuccess && areThereFailures)

    [<TestMethod>]
    member this.ShootCanReturnBothValueParallelLinq () =
        let rand = Random.Shared
        let tries = Array.Parallel.init numberOfTries (fun _ -> shoot (rand.Next()))
        let successList = tries.AsParallel().Where(fun b -> b = true)
        let failureList = tries.AsParallel().Where(fun b -> b = false)
        let areThereSuccess = successList.Any()
        let areThereFailures = failureList.Any()
        Assert.IsTrue(areThereSuccess && areThereFailures)
