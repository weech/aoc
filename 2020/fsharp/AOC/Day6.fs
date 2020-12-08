module AOC.Day6

open System 

let setifyString (str: string) = 
    str |> Seq.toList |> Set.ofList

let createSet accum group  =
    group
    |> Seq.map setifyString
    |> accum

// 4.279 ms
let part1 data =
    data
    |> Seq.sumBy ((createSet Set.unionMany) >> Set.count)

// 3.894 ms
let part2 data = 
    data
    |> Seq.sumBy ((createSet Set.intersectMany) >> Set.count)
