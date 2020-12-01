namespace AOC

open System

module Parsers = 
    let DATA = System.IO.Path.Combine [| __SOURCE_DIRECTORY__; ".."; ".."; "data" |]

    let day1 () =
        System.IO.Path.Combine [| DATA; "day1.txt" |]
            |> System.IO.File.ReadLines 
            |> Seq.map (fun x -> int(x))
            |> List.ofSeq

module Helpers = 
    let checkEntry(vals:list<int>) = 
        if List.sum vals = 2020 then 
            List.fold ( * ) 1 vals
        else
            0

    let cartesianProduct lists = 
        Seq.foldBack (fun n g -> [for nel in n do for gel in g do yield nel::gel])
             lists 
             [[]]

module Scripts = 
    let day1Part1 data = 
        Helpers.cartesianProduct [data; data]
            |> List.map Helpers.checkEntry
            |> List.filter (fun x -> x > 0)
            |> List.head
        
    let day1Part2 data = 
        Helpers.cartesianProduct [data; data; data]
            |> List.map Helpers.checkEntry
            |> List.filter (fun x -> x > 0)
            |> List.head      
