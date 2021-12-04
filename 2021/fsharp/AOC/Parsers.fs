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