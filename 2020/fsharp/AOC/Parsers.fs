namespace AOC

open System

module Parsers = 
    let DATA = System.IO.Path.Combine [| __SOURCE_DIRECTORY__; ".."; ".."; "data" |]

    let day1 () =
        System.IO.Path.Combine [| DATA; "day1.txt" |]
        |> System.IO.File.ReadLines 
        |> Seq.map (fun x -> int(x))
        |> List.ofSeq

    let day2 () =
        System.IO.Path.Combine [| DATA; "day2.txt" |]
        |> System.IO.File.ReadLines 

    let day3() = 
        System.IO.Path.Combine [| DATA; "day3.txt" |]
        |> System.IO.File.ReadLines
        |> Seq.map (fun line -> Seq.map (fun x -> if x = '#' then 1 else 0) line)
        |> array2D

    let day4() = 
        let file = System.IO.Path.Combine [| DATA; "day4.txt" |]
                |> System.IO.File.ReadAllText 
        file.Split([|"\n\n"|], StringSplitOptions.RemoveEmptyEntries)

    let day5() =
        System.IO.Path.Combine [| DATA; "day5.txt" |]
        |> System.IO.File.ReadLines 

    let day6() = 
        let file = System.IO.Path.Combine [| DATA; "day6.txt" |]
                |> System.IO.File.ReadAllText
        file.Split([|"\n\n"|], StringSplitOptions.RemoveEmptyEntries)
        |> Seq.map (fun group -> group.Split([|'\n'|]))

    let day7() = 
        System.IO.Path.Combine [| DATA; "day7.txt" |]
        |> System.IO.File.ReadLines         

    let day8() = 
        System.IO.Path.Combine [| DATA; "day8.txt" |]
        |> System.IO.File.ReadLines  

    let day9() = 
        System.IO.Path.Combine [| DATA; "day9.txt" |]
        |> System.IO.File.ReadLines 
        |> Seq.map int64
        |> Array.ofSeq