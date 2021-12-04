module AOC.Day04

open System

[<Literal>]
let WIDTH = 5

type Board = {Data: int[]; Wins: int[]}

let getRowCol idx nx = (idx / nx, idx % nx)

let makeBoard (str: string) = 
    {Data = str.Split('\n')
        |> Seq.map (fun row -> row.Split(' ', StringSplitOptions.RemoveEmptyEntries) |> Array.map int)
        |> Array.concat; 
    Wins = Array.create (WIDTH*2) 0 }

let checkBoard call board =
    match board.Data |> Array.tryFindIndex ((=) call) with 
    | Some idx -> 
        let (row, col) = getRowCol idx WIDTH             
        {board with Wins = Array.init (WIDTH*2)
                (fun idx -> 
                    if idx = row then board.Wins[idx] + 1 
                    elif idx = col + WIDTH  then board.Wins[idx] + 1
                    else board.Wins[idx])}
    | None -> board
  
let calculateScore board priorCalls = 
    board.Data
    |> Seq.filter (fun ele -> priorCalls |> Array.exists (((=) ele)) |> not)
    |> Seq.sum 
    |> (*) priorCalls[(Array.length priorCalls) - 1]

let checkWins board = Seq.exists ((=) WIDTH) board.Wins

let rec advanceGame turn (calls: int[]) boards = 
    match Array.tryFindIndex checkWins boards with
    | Some idx -> calculateScore boards[idx] calls[0..(turn-1)]
    | None -> advanceGame (turn + 1) calls (Array.map (checkBoard calls[turn]) boards)

let part1 calls boards = 
    advanceGame 0 calls (boards |> Seq.map makeBoard |> Array.ofSeq)

let rec losingGame turn (calls: int[]) boards = 
    match Array.length (Array.filter checkWins boards) with
    | 0 -> losingGame (turn + 1) calls (Array.map (checkBoard calls[turn]) boards)
    | x when Array.length boards = 1 -> calculateScore boards[0] calls[0..(turn-1)]
    | _ -> losingGame (turn + 1) calls (boards |> Array.filter (checkWins >> not) |> Array.map (checkBoard calls[turn]))

let part2 calls boards = 
    losingGame 0 calls (boards |> Seq.map makeBoard |> Array.ofSeq)
