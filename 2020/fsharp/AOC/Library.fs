namespace AOC

open System
open System.Text.RegularExpressions

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

    let splitInFour (str: string)  = 
        let m = Regex.Match (str, "^(\d+)-(\d+) ([a-z]): (.+)$")
        let cap = Array.tail [| for g in m.Groups -> g.Value |]
        (int cap.[0], int cap.[1], cap.[2].[0], cap.[3])

    let countTrees (slopeX: int, slopeY: int) (data: int [,]) = 
        let xs = Seq.toArray (seq {for n in 0 .. slopeX .. (Array2D.length1 data - 1) do n})
        let ys = seq {for n in 0 .. (Array.length xs - 1) do (n * slopeY) % Array2D.length2 data}
        Seq.zip xs ys
        |> Seq.sumBy (fun (x, y) -> data.[x, y])

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

    let day2Part1 data = 
        let isValid min max letter word =
            let getCount lett pword = 
                pword 
                |> Seq.filter (fun x -> x = lett)
                |> Seq.length
            min <= getCount letter word && max >= getCount letter word
        data 
        |> Seq.filter (Helpers.splitInFour >> 
                        (fun (min, max, letter, word) -> isValid min max letter (Seq.toList word)))
        |> Seq.length
     
    let day2Part2 data =
        let isValid first last letter (word: string) =
            (word.[first-1] = letter) <> (word.[last-1] = letter)
        data 
        |> Seq.filter (Helpers.splitInFour >> (fun (min, max, letter, word) -> isValid min max letter word))
        |> Seq.length

    let day3Part1 data = 
        int64 (Helpers.countTrees (1, 3) data)

    let day3Part2 data = 
        Seq.fold (fun accum slope -> accum * int64 (Helpers.countTrees slope data)) 
            1L
            [ (1, 1); (1, 3); (1, 5); (1, 7); (2, 1) ]