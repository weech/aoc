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

    let day4() = 
        let file = System.IO.Path.Combine [| DATA; "day4.txt" |]
                |> System.IO.File.ReadAllText 
        file.Split([|"\n\n"|], StringSplitOptions.RemoveEmptyEntries)

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

    let groupEntry item = 
        Regex.Matches(item, "(\w+):(\S+)")
        |> Seq.cast<Match>
        |> Seq.map (fun mat -> (let cap = Array.tail [| for g in mat.Groups -> g.Value |]
                                (cap.[0], cap.[1])))
        |> Map 

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

    let day4Inner validator data = 
        data
        |> Seq.filter (Helpers.groupEntry >> validator)
        |> Seq.length

    let day4Part1 data = 
        let validatePassport (passport: Map<string, string>) = 
            let mandatoryKeys = ["byr"; "iyr"; "eyr"; "hgt"; "hcl"; "ecl"; "pid"]
            Seq.forall (fun key -> passport.ContainsKey(key)) mandatoryKeys
        day4Inner validatePassport data

    let day4Part2 data = 
        let validatePassport (passport: Map<string, string>) = 
            passport.ContainsKey("byr") && Regex.IsMatch (passport.["byr"], "^19[2-9][0-9]$|^200[0-2]$") &&
            passport.ContainsKey("iyr") && Regex.IsMatch (passport.["iyr"], "^201[0-9]$|^2020$") && 
            passport.ContainsKey("eyr") && Regex.IsMatch (passport.["eyr"], "^202[0-9]$|^2030$") && 
            passport.ContainsKey("hgt") && Regex.IsMatch (passport.["hgt"], "^1[5-8][0-9]cm$|^19[0-3]cm$|^59in$|^6[0-9]in$|^7[0-6]in$") &&
            passport.ContainsKey("hcl") && Regex.IsMatch (passport.["hcl"], "^#[0-9a-f]{6}$") &&
            passport.ContainsKey("ecl") && Regex.IsMatch (passport.["ecl"], "^amb$|^blu$|^brn$|^gry$|^grn$|^hzl$|^oth$") &&
            passport.ContainsKey("pid") && Regex.IsMatch (passport.["pid"], "^\d{9}$")
        day4Inner validatePassport data