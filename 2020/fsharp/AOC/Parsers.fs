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

    let day10() = 
        System.IO.Path.Combine [| DATA; "day10.txt" |]
        |> System.IO.File.ReadLines 
        |> Seq.map int
        |> Array.ofSeq    

    let day11() = 
        System.IO.Path.Combine [| DATA; "day11.txt" |]
        |> System.IO.File.ReadLines
        |> array2D

    let day12() = 
        System.IO.Path.Combine [| DATA; "day12.txt" |]
        |> System.IO.File.ReadLines    

    let day13() = 
        let file = System.IO.Path.Combine [| DATA; "day13.txt"|]
                    |> System.IO.File.ReadAllText
        let lines = file.Split([|"\n"|], StringSplitOptions.RemoveEmptyEntries)
        (int lines.[0], lines.[1])

    let day14() = 
        System.IO.Path.Combine [| DATA; "day14.txt" |]
        |> System.IO.File.ReadLines 

    let day16() = 
        let file = System.IO.Path.Combine [| DATA; "day16.txt" |]
                    |> System.IO.File.ReadAllText 
        file.Split([|"\n\n"|], StringSplitOptions.RemoveEmptyEntries)

    let day19General path = 
        let file = System.IO.Path.Combine [| DATA; path |]
                    |> System.IO.File.ReadAllText 
        let splat = file.Split([|"\n\n"|], StringSplitOptions.RemoveEmptyEntries)
        let rules = splat.[0].Split([|'\n'|], StringSplitOptions.RemoveEmptyEntries)
        let messages = splat.[1].Split([|'\n'|], StringSplitOptions.RemoveEmptyEntries)
        rules, messages


    let day19() = 
        day19General "day19.txt"
    
    let day19Test() = 
        day19General "day19_test.txt"
