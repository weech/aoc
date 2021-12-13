namespace AOC

open System

module Parsers =

    let DATA =
        System.IO.Path.Combine [| __SOURCE_DIRECTORY__
                                  ".."
                                  ".."
                                  "data" |]

    let day01 () =
        System.IO.Path.Combine [| DATA
                                  "day01.txt" |]
        |> System.IO.File.ReadLines
        |> Seq.map int
        |> Array.ofSeq

    let day02 () =
        System.IO.Path.Combine [| DATA
                                  "day02.txt" |]
        |> System.IO.File.ReadLines

    let day03 () =
        System.IO.Path.Combine [| DATA
                                  "day03.txt" |]
        |> System.IO.File.ReadLines
        |> Array.ofSeq

    let day04 () = 
        let file = 
            System.IO.Path.Combine [| DATA; "day04.txt" |]
            |> System.IO.File.ReadAllText
        let doubles = file.Split([|"\n\n"|], StringSplitOptions.RemoveEmptyEntries)
        let calls = (doubles[0]).Split(',') |> Seq.map int |> Array.ofSeq
        let boards = doubles[1..]
        (calls, boards)

    let day05 () =
        System.IO.Path.Combine [| DATA
                                  "day05.txt" |]
        |> System.IO.File.ReadLines

    let split (c: char) (str: string) = str.Split(c)

    let day06 () =
        System.IO.Path.Combine [| DATA
                                  "day06.txt" |]
        |> System.IO.File.ReadLines
        |> Seq.exactlyOne
        |> split ','
        |> Seq.map uint64
        |> List.ofSeq

    let day07 () =
        System.IO.Path.Combine [| DATA
                                  "day07.txt" |]
        |> System.IO.File.ReadAllText
        |> split ',' 
        |> Array.map int

    let day08 () =
        System.IO.Path.Combine [| DATA
                                  "day08.txt" |]
        |> System.IO.File.ReadLines

    let day09 () =
        System.IO.Path.Combine [| DATA
                                  "day09.txt" |]
        |> System.IO.File.ReadLines
        |> List.ofSeq

    let day10 () =
        System.IO.Path.Combine [| DATA
                                  "day10.txt" |]
        |> System.IO.File.ReadLines

    let day11 () =
        System.IO.Path.Combine [| DATA
                                  "day11.txt" |]
        |> System.IO.File.ReadLines
        |> List.ofSeq

    let day12 () =
        System.IO.Path.Combine [| DATA
                                  "day12.txt" |]
        |> System.IO.File.ReadLines

    let day13 () = 
        let file = 
            System.IO.Path.Combine [| DATA; "day13.txt" |]
            |> System.IO.File.ReadAllText
        let doubles = file.Split([|"\n\n"|], StringSplitOptions.RemoveEmptyEntries)
        (doubles.[0].Split('\n'), doubles.[1].Split('\n', StringSplitOptions.RemoveEmptyEntries))