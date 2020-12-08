module AOC.Day2

open System
open System.Text.RegularExpressions

let splitInFour (str: string)  = 
    let m = Regex.Match (str, "^(\d+)-(\d+) ([a-z]): (.+)$")
    let cap = Array.tail [| for g in m.Groups -> g.Value |]
    (int cap.[0], int cap.[1], cap.[2].[0], cap.[3])


// 4.399 ms
let part1 data = 
    let isValid min max letter word =
        let getCount lett pword = 
            pword 
            |> Seq.filter (fun x -> x = lett)
            |> Seq.length
        min <= getCount letter word && max >= getCount letter word
    data 
    |> Seq.filter (splitInFour >> 
                    (fun (min, max, letter, word) -> isValid min max letter (Seq.toList word)))
    |> Seq.length

// 2.649 ms
let part2 data =
    let isValid first last letter (word: string) =
        (word.[first-1] = letter) <> (word.[last-1] = letter)
    data 
    |> Seq.filter (splitInFour >> (fun (min, max, letter, word) -> isValid min max letter word))
    |> Seq.length
