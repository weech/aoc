module AOC.Day6

open System 

let setifyString (str: string) = 
    str |> Seq.toList |> Set.ofList

let createSet accum group  =
    group
    |> Seq.map setifyString
    |> accum

let part1 data =
    data
    |> Seq.sumBy ((createSet Set.unionMany) >> Set.count)

let part2 data = 
    data
    |> Seq.sumBy ((createSet Set.intersectMany) >> Set.count)
