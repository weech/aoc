module AOC.Day5

open System

let getSeatID item = 
    item 
    |> Seq.map (fun l -> 
        match l with 
        | 'L' -> '0'
        | 'R' -> '1'
        | 'B' -> '1'
        | 'F' -> '0'
        | _ -> l)
    |> Seq.toArray
    |> System.String
    |> (+) "0b"
    |> int

// 874.601 us
let part1 data = 
    data |> Seq.map getSeatID |> Seq.max

// 892.714 us
let part2 data =
    let sorted = data |> Seq.map getSeatID |> Array.ofSeq |> Array.sort
    Array.map2 (-) sorted.[1..] sorted.[..((Array.length sorted) - 2)]
    |> Array.findIndex ((=) 2)
    |> (+) sorted.[0]
    |> (+) 1 // 0 based indexing offset