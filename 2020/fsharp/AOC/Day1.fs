module AOC.Day1
    
let checkEntry(vals:list<int>) = 
    if List.sum vals = 2020 then 
        List.fold ( * ) 1 vals
    else
        0

let cartesianProduct lists = 
    Seq.foldBack (fun n g -> [for nel in n do for gel in g do yield nel::gel])
            lists 
            [[]]

let part1 data = 
    cartesianProduct [data; data]
    |> List.map checkEntry
    |> List.filter (fun x -> x > 0)
    |> List.head
    
let part2 data = 
    cartesianProduct [data; data; data]
    |> List.map checkEntry
    |> List.filter (fun x -> x > 0)
    |> List.head  