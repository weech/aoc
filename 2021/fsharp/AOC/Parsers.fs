namespace AOC

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
        |> Seq.map (fun x -> int x)
        |> Array.ofSeq

    let day02 () =
        System.IO.Path.Combine [| DATA
                                  "day02.txt" |]
        |> System.IO.File.ReadLines