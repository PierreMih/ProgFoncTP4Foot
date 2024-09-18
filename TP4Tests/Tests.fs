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
    member this.ShootCanReturnBothValues () =
        let rand = Random()
        let tries = List.init numberOfTries (fun _ -> shoot (rand.Next()))
        let successList = tries |> List.filter (fun b -> b = true) 
        let failureList = tries |> List.filter (fun b -> b = false)
        let areThereSuccess = not successList.IsEmpty
        let areThereFailures = not failureList.IsEmpty
        Assert.IsTrue(areThereSuccess && areThereFailures)

    [<TestMethod>]
    member this.ShootCanReturnBothValuesParallel () =
        let rand = Random.Shared
        let tries = Array.Parallel.init numberOfTries (fun _ -> shoot (rand.Next()))
        let successList = tries |> Array.Parallel.filter (fun b -> b = true) 
        let failureList = tries |> Array.Parallel.filter (fun b -> b = false) 
        let areThereSuccess = successList.Any()
        let areThereFailures = failureList.Any()
        Assert.IsTrue(areThereSuccess && areThereFailures)

    [<TestMethod>]
    member this.ShootCanReturnBothValuesParallelLinq () =
        let rand = Random.Shared
        let tries = Array.Parallel.init numberOfTries (fun _ -> shoot (rand.Next()))
        let successList = tries.AsParallel().Where(fun b -> b = true)
        let failureList = tries.AsParallel().Where(fun b -> b = false)
        let areThereSuccess = successList.Any()
        let areThereFailures = failureList.Any()
        Assert.IsTrue(areThereSuccess && areThereFailures)

    [<TestMethod>]
    member this.CanGetScoreFirstTeam () =
        let rounds = [(true, true); (false, true);(false, true);(false, true);(false, true);(false, true)]
        let scoreFirstTeam = getScoreFirstTeam rounds
        Assert.AreEqual(1, scoreFirstTeam)

    [<TestMethod>]
    member this.CanGetScoreSecondTeam () =
        let rounds = [(true, true); (false, true);(false, true);(false, true);(false, true);(false, true)]
        let scoreSecondTeam = getScoreSecondTeam rounds
        Assert.AreEqual(6, scoreSecondTeam)
        
    [<TestMethod>]
    member this.IsThereAWinner_ReturnsTrue () =
        let rounds = [(true, true); (false, true);(false, true);(false, true);(false, true);(false, true)]
        let isThereWinner = isThereAWinner rounds
        Assert.IsTrue(isThereWinner)
        
    [<TestMethod>]
    member this.IsThereAWinner_ReturnsFalse () =
        let rounds = [(true, true); (true, true);(true, true);(true, true);(true, true);(true, true)]
        let isThereWinner = isThereAWinner rounds
        Assert.IsFalse(isThereWinner)
        
    [<TestMethod>]
    member this.GetWinnerScore_ReturnsScore () =
        let rounds = [(true, true); (false, true);(false, true);(false, true);(false, true);(false, true)]
        let winnerScore = getWinnerScore rounds
        Assert.IsTrue(winnerScore.IsSome && winnerScore.Value = 6)
        
    [<TestMethod>]
    member this.GetWinnerScore_ReturnsNone () =
        let rounds = [(true, true); (true, true);(true, true);(true, true);(true, true);(true, true)]
        let winnerScore = getWinnerScore rounds
        Assert.IsTrue(winnerScore.IsNone)
        
    [<TestMethod>]
    member this.GetWinner_ReturnsTeam1 () =
        let rounds = [(true, false); (true, true);(true, true);(true, true);(true, true);(true, true)]
        let winner = getWinner rounds
        Assert.IsTrue(winner.IsSome && winner.Value = "Team 1")
        
    [<TestMethod>]
    member this.GetWinner_ReturnsTeam2 () =
        let rounds = [(false, true); (true, true);(true, true);(true, true);(true, true);(true, true)]
        let winner = getWinner rounds
        Assert.IsTrue(winner.IsSome && winner.Value = "Team 2")
        
    [<TestMethod>]
    member this.GetWinner_ReturnsNone () =
        let rounds = [(true, true); (true, true);(true, true);(true, true);(true, true);(true, true)]
        let winner = getWinner rounds
        Assert.IsTrue(winner.IsNone)
        
    [<TestMethod>]
    member this.PlayRoundCanReturnBothValues () =
        let rand = Random.Shared
        let rounds = Array.Parallel.init numberOfTries (fun _ -> playRound (rand.Next()))
        let successList = tries.AsParallel().Where(fun b -> b = true)
        let failureList = tries.AsParallel().Where(fun b -> b = false)
        let areThereSuccess = successList.Any()
        let areThereFailures = failureList.Any()
        Assert.IsTrue(areThereSuccess && areThereFailures)