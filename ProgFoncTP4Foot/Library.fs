namespace ProgFoncTP4Foot

open System

module TirAuBut =
    let shoot (seed : int) =
        let cos = Math.Cos seed
        cos > 0
    
    let getScoreFirstTeam (shotsList : (bool * bool) list) =
        shotsList |> List.map fst
                  |> List.map (fun b -> if b then 1 else 0 )
                  |> List.sum
        
    let getScoreSecondTeam (shotsList : (bool * bool) list) =
        shotsList |> List.map snd
                  |> List.map (fun b -> if b then 1 else 0 )
                  |> List.sum
        
    let isThereAWinner (shotsList : (bool*bool) list) =
        let score1 = getScoreFirstTeam shotsList
        let score2 = getScoreSecondTeam shotsList
        score1 <> score2
        
    let getWinnerScore (shotsList : (bool*bool) list) =
        let score1 = getScoreFirstTeam shotsList
        let score2 = getScoreSecondTeam shotsList
        if score1 = score2 then
            None
        else
            Some(max score1 score2)
    
    let getWinner (shotsList : (bool*bool) list) =
        if isThereAWinner shotsList then
            let winnerScore = getWinnerScore shotsList
            if winnerScore.IsSome then
                if getScoreFirstTeam shotsList = winnerScore.Value then
                    Some("Team 1")
                else
                    Some("Team 2")
            else
                None
        else
            None
            
    let playRound (seed : int) =
        (shoot (seed / 3), shoot (seed * 2 / 3))
    let play5RoundMatch (seed : int) =
        let roundNumbers = List.init 5 (fun i -> i + 1)
        roundNumbers |> List.map (fun n -> playRound (seed * n / 5))
    